using System;
using System.Globalization;
using System.Windows.Forms;
using SimpleDebugView.Core;

namespace SimpleDebugView
{
    public partial class MainForm : Form
    {
        private readonly DbWinMonitor _monitor = new DbWinMonitor();

        public MainForm()
        {
            InitializeComponent();

            _monitor.MessageReceived += OnMessageReceived;

            UpdateButtons(false);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                _monitor.Start();
                UpdateButtons(true);
                AppendUiLine("=== Monitor started ===");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Start failed: " + ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _monitor.Stop();
            UpdateButtons(false);
            AppendUiLine("=== Monitor stopped ===");
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            _monitor.IsPaused = !_monitor.IsPaused;
            btnPause.Text = _monitor.IsPaused ? "继续" : "暂停";
            AppendUiLine(_monitor.IsPaused ? "=== Paused ===" : "=== Resumed ===");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listBoxLogs.Items.Clear();
        }

        private void OnMessageReceived(DebugMessage msg)
        {
            // 后台线程回调 -> 切回 UI 线程
            if (IsDisposed) return;

            BeginInvoke(new Action(() =>
            {
                if (!PassFilter(msg)) return;

                string line = string.Format(
                    CultureInfo.InvariantCulture,
                    "{0:HH:mm:ss.fff}  [{1}]  {2}",
                    msg.Time, msg.Pid, msg.Text);

                listBoxLogs.Items.Add(line);

                // 自动滚动到最后（ListBox）
                listBoxLogs.TopIndex = Math.Max(0, listBoxLogs.Items.Count - 1);
            }));
        }

        private bool PassFilter(DebugMessage msg)
        {
            // PID 过滤
            string pidText = (txtPidFilter.Text ?? string.Empty).Trim();
            if (pidText.Length > 0)
            {
                int pid;
                if (!int.TryParse(pidText, NumberStyles.Integer, CultureInfo.InvariantCulture, out pid))
                    return false; // PID 输入非法时：简单处理为不显示
                if (msg.Pid != pid) return false;
            }

            // 关键字过滤（包含）
            string keyword = (txtKeyword.Text ?? string.Empty).Trim();
            if (keyword.Length > 0)
            {
                if (msg.Text == null) return false;
                if (msg.Text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) < 0)
                    return false;
            }

            return true;
        }

        private void AppendUiLine(string text)
        {
            listBoxLogs.Items.Add(text);
            listBoxLogs.TopIndex = Math.Max(0, listBoxLogs.Items.Count - 1);
        }

        private void UpdateButtons(bool running)
        {
            btnStart.Enabled = !running;
            btnStop.Enabled = running;
            btnPause.Enabled = running;
            btnClear.Enabled = true;

            btnPause.Text = "暂停";
            _monitor.IsPaused = false;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _monitor.Dispose();
            base.OnFormClosing(e);
        }
    }
}
