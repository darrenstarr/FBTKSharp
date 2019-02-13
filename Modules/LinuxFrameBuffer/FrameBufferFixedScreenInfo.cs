namespace LinuxFrameBuffer
{
    using System.Runtime.InteropServices;
    using System.Text;

    [StructLayout(LayoutKind.Sequential, Size=80)]
    internal struct FrameBufferFixedScreenInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string Id;
        public long SMemStart;
        public uint SMemLength;
        public uint Type;
        public uint TypeAux;
        public uint Visual;
        public ushort XPanStep;
        public ushort YPanStep;
        public ushort YWrapStep;
        public uint LineLength;
        public uint MMIOStart;
        public uint MMIOLength;
        public uint Accel;
        public ushort Capabilities;
        public ushort Reserved1;
        public ushort Reserved2;

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"ID: {Id}");
            sb.AppendLine($"SMemStart: {SMemStart}");
            sb.AppendLine($"SMemLength: {SMemLength}");
            sb.AppendLine($"Type: {Type}");
            sb.AppendLine($"TypeAux: {TypeAux}");
            sb.AppendLine($"Visual: {Visual}");
            sb.AppendLine($"XPanStep: {XPanStep}");
            sb.AppendLine($"YPanStep: {YPanStep}");
            sb.AppendLine($"YWrapStep: {YWrapStep}");
            sb.AppendLine($"Line length: {LineLength}");
            sb.AppendLine($"MMIO Start: {MMIOStart}");
            sb.AppendLine($"MMIO Length: {MMIOLength}");
            sb.AppendLine($"Accel: {Accel}");
            sb.AppendLine($"Capabilities: {Capabilities}");

            return sb.ToString();
        }
    }
}