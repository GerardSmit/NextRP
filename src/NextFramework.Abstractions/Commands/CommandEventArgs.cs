using System;

namespace NextFramework.Commands
{
    public class CommandEventArgs : EventArgs
    {
        public bool Cancelled { get; set; }
    }
}
