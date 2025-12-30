using System;

namespace SimpleDebugView.Core
{
    public sealed class DebugMessage
    {
        public DateTime Time { get; set; }
        public int Pid { get; set; }
        public string Text { get; set; }
    }
}
