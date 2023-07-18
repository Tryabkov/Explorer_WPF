using System;

namespace Explorer_WPF.MVVM.Models.AbstractClasses
{
    internal class FolderItem
    {
        public string Name { get; protected set; }
        public string Path { get; protected set; }
        public string Extention { get; protected set; }
        public virtual DateTime? CreationTime { get; protected set; }
        public virtual DateTime? LastAccessTime { get; protected set; }
        public virtual DateTime? LastWriteTime { get; protected set; }

        public virtual bool IsFile { get; protected set; }
    }
}
