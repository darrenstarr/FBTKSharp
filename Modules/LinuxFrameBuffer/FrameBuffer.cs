namespace LinuxFrameBuffer
{
    using System;
    using System.IO;
    using System.IO.MemoryMappedFiles;
    using System.Linq;
    using System.Runtime.ConstrainedExecution;
    using System.Runtime.InteropServices;

    public class FrameBuffer : IDisposable
    {
        public string DeviceName { get; private set; }

        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }

        internal const int OPEN_READ_WRITE = 2; // constant, even for different devices
        FrameBufferVarScreenInfo FrameBufferInfo = new FrameBufferVarScreenInfo();
        FrameBufferFixedScreenInfo FrameBufferFixedInfo = new FrameBufferFixedScreenInfo();
        private SafeUnixHandle fb0Handle;
        private IntPtr MMapPointer;
        private ulong ScreenSize;
        private uint PixelWidth;
        private uint Stride;

        public FrameBuffer(string deviceName="/dev/fb0")
        {
            DeviceName = deviceName;

            System.Diagnostics.Debug.WriteLine($"Attempting to open {DeviceName}");
            fb0Handle = UnsafeNativeMethods.Open(DeviceName, OPEN_READ_WRITE);
            if(fb0Handle.IsInvalid) {
                throw new UnixIOException();
            } 

            System.Diagnostics.Debug.WriteLine("Getting framebuffer info");
            var result = UnsafeNativeMethods.FrameBufferVarScreenInfoIoctl(fb0Handle, FrameBufferConstants.FBIOGET_VSCREENINFO, ref FrameBufferInfo);
            if(result < 0) {
                throw new UnixIOException();
            } 

            ScreenWidth = FrameBufferInfo.XResolution;            
            ScreenHeight = FrameBufferInfo.YResolution;            
            System.Diagnostics.Debug.WriteLine($"Frame buffer resolution: {FrameBufferInfo.XResolution}x{FrameBufferInfo.YResolution}");

            System.Diagnostics.Debug.WriteLine("Getting fixed framebuffer info");
            result = UnsafeNativeMethods.FrameBufferFixedScreenInfoIoctl(fb0Handle, FrameBufferConstants.FBIOGET_FSCREENINFO, ref FrameBufferFixedInfo);
            if(result < 0) {
                throw new UnixIOException();
            } 

            System.Diagnostics.Debug.WriteLine($"Fixed frame buffer");
            System.Diagnostics.Debug.WriteLine(FrameBufferFixedInfo.ToString());

            ScreenSize = FrameBufferInfo.XResolution * FrameBufferInfo.YResolution * FrameBufferInfo.BitsPerPixel / 8;
            System.Diagnostics.Debug.WriteLine($"Screen size {ScreenSize}");

            System.Diagnostics.Debug.WriteLine("MMaping device");
            MMapPointer = UnsafeNativeMethods.MMap(IntPtr.Zero, ScreenSize, MmapConstants.PROT_READ | MmapConstants.PROT_WRITE, MmapConstants.MAP_SHARED, fb0Handle, (ulong)0);
            if(MMapPointer.ToInt64() == -1) {
                throw new UnixIOException();
            } 

            System.Diagnostics.Debug.WriteLine($"Mapped buffer {MMapPointer.ToInt64()}");

            PixelWidth = FrameBufferInfo.BitsPerPixel / 8;
            Stride = FrameBufferFixedInfo.LineLength;
        }
        
        public void PutPixel(int x, int y, int r, int g, int b)
        {
            var location =
                (x + FrameBufferInfo.XOffset) * PixelWidth +
                (y + FrameBufferInfo.YOffset) * Stride;

            int value = r<<16 | g << 8 | b << 0 | 0xFF << 24;
            Marshal.WriteInt32(MMapPointer, Convert.ToInt32(location), value);
        }

        public void DrawHorizontalLine(int x, int y, int width, int r, int g, int b)
        {
            var location =
                (x + FrameBufferInfo.XOffset) * PixelWidth +
                (y + FrameBufferInfo.YOffset) * Stride;

            int value = r<<16 | g << 8 | b << 0 | 0xFF << 24;
            for(var i=0; i<width; i++,location+=PixelWidth)
                Marshal.WriteInt32(MMapPointer, Convert.ToInt32(location), value);
        }

        public void FillRectangle(int x, int y, int width, int height, int r, int g, int b)
        {
            for(var i=0; i<height; i++)
                DrawHorizontalLine(x, y+i, width, r, g, b);
        }

        public void Dispose()
        {
            System.Diagnostics.Debug.WriteLine("Unmapping device");
            UnsafeNativeMethods.MUnmap(MMapPointer, ScreenSize);
            System.Diagnostics.Debug.WriteLine($"Closing device {DeviceName}");
            fb0Handle.Close();
        }
    }
}