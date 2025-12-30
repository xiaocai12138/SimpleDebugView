# SimpleDebugView

一个简单的Windows调试信息查看工具，用于捕获和显示使用`OutputDebugString`函数输出的调试信息。

## 功能特点

- 🎯 **实时监控**：捕获所有调用`OutputDebugString`的进程输出
- 📝 **详细信息**：显示调试信息的时间戳（毫秒级）、进程ID和消息内容
- ⏯️ **控制功能**：支持开始、停止、暂停和继续监控
- 🔍 **过滤功能**：按进程ID和关键字过滤调试信息
- 🧹 **日志管理**：支持清除当前显示的日志
- 📱 **简洁界面**：直观的Windows Forms界面，易于使用

## 安装和运行

### 前提条件

- Windows操作系统
- .NET Framework 4.6 或更高版本

### 运行方式

1. 直接运行编译好的可执行文件 `SimpleDebugView.exe`
2. 或从源代码编译运行：
   - 克隆或下载项目代码
   - 使用Visual Studio打开 `SimpleDebugView.sln`
   - 编译并运行项目

## 使用方法

1. **启动监控**：点击"开始"按钮开始捕获调试信息
2. **停止监控**：点击"停止"按钮停止捕获调试信息
3. **暂停/继续**：点击"暂停"按钮暂停显示新的调试信息（不会停止捕获），再次点击"继续"恢复显示
4. **清除日志**：点击"清除"按钮清空当前显示的所有日志
5. **PID过滤**：在PID输入框中输入进程ID，只显示该进程的调试信息
6. **关键字过滤**：在关键字输入框中输入文本，只显示包含该文本的调试信息

## 项目结构

```
SimpleDebugView/
├── SimpleDebugView/
│   ├── Core/                 # 核心功能实现
│   │   ├── DbWinMonitor.cs   # DBWIN监控器，核心功能实现
│   │   └── DebugMessage.cs   # 调试消息数据结构
│   ├── Properties/           # 项目属性文件
│   ├── App.config            # 应用程序配置
│   ├── MainForm.cs           # 主界面实现
│   ├── MainForm.Designer.cs  # 主界面设计器
│   ├── MainForm.resx         # 主界面资源
│   ├── Program.cs            # 应用程序入口
│   └── SimpleDebugView.csproj # 项目文件
└── SimpleDebugView.sln       # 解决方案文件
```

## 技术说明

### 工作原理

SimpleDebugView 使用 Windows 系统的 DBWIN（Debug Window）机制来捕获调试输出。该机制通过以下组件实现：

1. **共享内存**：使用命名共享内存 `DBWIN_BUFFER` 存储调试信息
2. **事件通知**：使用两个命名事件 `DBWIN_BUFFER_READY` 和 `DBWIN_DATA_READY` 进行进程间通信
3. **内存映射**：通过内存映射文件访问共享内存内容

### 核心类说明

#### DbWinMonitor
- 实现了对DBWIN机制的封装
- 提供Start/Stop/暂停/继续等控制方法
- 解析共享内存中的调试信息并触发MessageReceived事件

#### DebugMessage
- 调试消息的数据结构
- 包含时间戳、进程ID和消息内容

#### MainForm
- 应用程序主界面
- 处理用户交互和UI更新
- 实现日志显示和过滤功能

## 注意事项

1. 同一时间只能有一个调试视图工具运行（因为DBWIN机制是全局共享的）
2. 暂停监控时，系统仍会继续捕获调试信息，但不会显示在界面上
3. 如果监控器未运行，调试信息可能会丢失（取决于系统实现）

## 许可证

本项目采用 MIT 许可证，可自由使用和修改。

## 贡献

欢迎提交 Issue 和 Pull Request 来改进这个项目。

---

**作者**：-  
**版本**：1.0
**更新日期**：2025-12-30
