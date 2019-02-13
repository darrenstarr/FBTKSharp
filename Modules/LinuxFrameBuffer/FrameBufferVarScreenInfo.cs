namespace LinuxFrameBuffer
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Size=160)]
    internal struct FrameBufferVarScreenInfo
    {
        public uint XResolution;
        public uint YResolution;
        public uint XResolutionVirtual;
        public uint YResolutionVirtual;
        public uint XOffset;
        public uint YOffset;
        public uint BitsPerPixel;
        public uint GrayScale;
        public FrameBufferBitField Red;
        public FrameBufferBitField Green;
        public FrameBufferBitField Blue;
        public FrameBufferBitField Transparent;
        public uint NonStandard;
        public uint Activate;
        public uint Height;
        public uint Width;
        public uint AccelerationFlags;
        public uint PixelClock;
        public uint LeftMargin;
        public uint RightMargin;
        public uint UpperMargin;
        public uint LowerMargin;
        public uint HorizontalSyncLength;
        public uint VerticalSyncLength;
        public uint Sync;
        public uint VMode;
        public uint Rotate;
        public uint Colorspace;
        public uint Reserved0;
        public uint Reserved1;
        public uint Reserved2;
        public uint Reserved3;
    };
}