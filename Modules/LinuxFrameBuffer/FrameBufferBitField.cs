namespace LinuxFrameBuffer
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct FrameBufferBitField
    {
        public UInt32 Offset;
        public UInt32 Length;
        public UInt32 MsbRight;
    }
}