using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace SimpleDebugView.Core
{
    /// <summary>
    /// 监听 Win32 Debug Output（DBWIN 机制），类似 DebugView。
    /// 捕获所有调用 OutputDebugString 的进程输出（不需要改目标程序）。
    /// </summary>
    public sealed class DbWinMonitor : IDisposable
    {
        // ---- DBWIN objects ----
        private const string DBWIN_BUFFER = "DBWIN_BUFFER";
        private const string DBWIN_BUFFER_READY = "DBWIN_BUFFER_READY";
        private const string DBWIN_DATA_READY = "DBWIN_DATA_READY";

        private const int DBWIN_BUFFER_SIZE = 4096;
        private const int FILE_MAP_READ = 0x0004;

        private readonly object _gate = new object();
        private Thread _thread;
        private volatile bool _running;
        private volatile bool _paused;

        private IntPtr _hMap = IntPtr.Zero;
        private IntPtr _pView = IntPtr.Zero;
        private IntPtr _hBufferReady = IntPtr.Zero;
        private IntPtr _hDataReady = IntPtr.Zero;

        public bool IsRunning { get { return _running; } }
        public bool IsPaused { get { return _paused; } set { _paused = value; } }

        public event Action<DebugMessage> MessageReceived;

        // ---- Win32 APIs ----
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateFileMapping(
            IntPtr hFile,
            IntPtr lpAttributes,
            uint flProtect,
            uint dwMaximumSizeHigh,
            uint dwMaximumSizeLow,
            string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr MapViewOfFile(
            IntPtr hFileMappingObject,
            uint dwDesiredAccess,
            uint dwFileOffsetHigh,
            uint dwFileOffsetLow,
            UIntPtr dwNumberOfBytesToMap);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateEvent(
            IntPtr lpEventAttributes,
            bool bManualReset,
            bool bInitialState,
            string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetEvent(IntPtr hEvent);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint WaitForSingleObject(IntPtr hHandle, int dwMilliseconds);

        public void Start()
        {
            lock (_gate)
            {
                if (_running) return;

                // PAGE_READWRITE = 0x04，映射 pagefile-backed shared memory
                _hMap = CreateFileMapping(new IntPtr(-1), IntPtr.Zero, 0x04, 0, DBWIN_BUFFER_SIZE, DBWIN_BUFFER);
                if (_hMap == IntPtr.Zero) throw new InvalidOperationException("CreateFileMapping failed: " + Marshal.GetLastWin32Error());

                _pView = MapViewOfFile(_hMap, FILE_MAP_READ, 0, 0, (UIntPtr)DBWIN_BUFFER_SIZE);
                if (_pView == IntPtr.Zero) throw new InvalidOperationException("MapViewOfFile failed: " + Marshal.GetLastWin32Error());

                _hBufferReady = CreateEvent(IntPtr.Zero, false, false, DBWIN_BUFFER_READY);
                if (_hBufferReady == IntPtr.Zero) throw new InvalidOperationException("CreateEvent(DBWIN_BUFFER_READY) failed: " + Marshal.GetLastWin32Error());

                _hDataReady = CreateEvent(IntPtr.Zero, false, false, DBWIN_DATA_READY);
                if (_hDataReady == IntPtr.Zero) throw new InvalidOperationException("CreateEvent(DBWIN_DATA_READY) failed: " + Marshal.GetLastWin32Error());

                _running = true;
                _thread = new Thread(Loop) { IsBackground = true, Name = "DBWIN Monitor" };
                _thread.Start();
            }
        }

        public void Stop()
        {
            lock (_gate)
            {
                if (!_running) return;
                _running = false;
            }

            // 尽量唤醒等待，避免 Join 卡住
            if (_hBufferReady != IntPtr.Zero) SetEvent(_hBufferReady);
            if (_hDataReady != IntPtr.Zero) SetEvent(_hDataReady);

            if (_thread != null && _thread.IsAlive) _thread.Join(800);
            Cleanup();
        }

        private void Loop()
        {
            // 告诉系统：消费者准备好
            SetEvent(_hBufferReady);

            while (_running)
            {
                // 等待生产者写数据；短超时便于 Stop 生效
                uint wait = WaitForSingleObject(_hDataReady, 200);
                if (!_running) break;
                if (wait != 0) continue; // WAIT_OBJECT_0 == 0

                try
                {
                    // 即使暂停，也必须继续 set buffer_ready，否则会阻塞全局 debug 输出
                    if (_paused)
                        continue;

                    // 解析：前 4 字节 PID，后续为 ANSI 字符串
                    int pid = Marshal.ReadInt32(_pView, 0);

                    int max = DBWIN_BUFFER_SIZE - 4;
                    byte[] bytes = new byte[max];
                    Marshal.Copy(IntPtr.Add(_pView, 4), bytes, 0, max);

                    int len = Array.IndexOf(bytes, (byte)0);
                    if (len < 0) len = max;

                    string text = Encoding.Default.GetString(bytes, 0, len);

                    var msg = new DebugMessage
                    {
                        Time = DateTime.Now,
                        Pid = pid,
                        Text = text
                    };

                    var handler = MessageReceived;
                    if (handler != null) handler(msg);
                }
                catch
                {
                    // 单次异常忽略，避免线程退出
                }
                finally
                {
                    // 告诉系统：可以写下一条了
                    SetEvent(_hBufferReady);
                }
            }
        }

        private void Cleanup()
        {
            if (_pView != IntPtr.Zero) { UnmapViewOfFile(_pView); _pView = IntPtr.Zero; }
            if (_hMap != IntPtr.Zero) { CloseHandle(_hMap); _hMap = IntPtr.Zero; }
            if (_hBufferReady != IntPtr.Zero) { CloseHandle(_hBufferReady); _hBufferReady = IntPtr.Zero; }
            if (_hDataReady != IntPtr.Zero) { CloseHandle(_hDataReady); _hDataReady = IntPtr.Zero; }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
