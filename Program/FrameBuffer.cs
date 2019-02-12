

namespace Program
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FrameBufferBitField
    {
        public UInt32 Offset;
        public UInt32 Length;
        public UInt32 MsbRight;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FrameBufferVarScreenInfo
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

    public class FrameBuffer : IDisposable
    {
        [DllImport("libc.so.6", EntryPoint = "open", SetLastError = true )]
        public static extern int Open(string fileName, int mode);
        
        [DllImport("libc.so.6", EntryPoint = "close", SetLastError = true)]
        public static extern int Close(int fd);

        [DllImport("libc.so.6", EntryPoint = "ioctl", SetLastError = true)]
        private extern static int FrameBufferIoctl(int fd, int request, ref FrameBufferVarScreenInfo screenInfo);
        
        [DllImport("libc.so.6", EntryPoint = "read", SetLastError = true)]
        internal static extern int Read(int handle, byte[] data, int length);

        internal const int OPEN_READ_WRITE = 2; // constant, even for different devices

        const int FBIOGET_VSCREENINFO = 0x4600;
        const int FBIOPUT_VSCREENINFO = 0x4601;
        const int FBIOGET_FSCREENINFO = 0x4602;
        const int  FBIOGETCMAP = 0x4604;
        const int  FBIOPUTCMAP = 0x4605;
        const int  FBIOPAN_DISPLAY = 0x4606;
        //const int _IOWR('F', 0x08, struct fb_cursor) FBIO_CURSOR;
        /* 0x4607-0x460B are defined below */
        const int FBIOGET_CON2FBMAP = 0x460F;
        const int FBIOPUT_CON2FBMAP = 0x4610;
        const int FBIOBLANK = 0x4611;		/* arg: 0 or vesa level + 1 */
        //const int _IOR('F', 0x12, struct fb_vblank) FBIOGET_VBLANK;
        const int FBIO_ALLOC = 0x4613;
        const int FBIO_FREE = 0x4614;
        const int FBIOGET_GLYPH = 0x4615;
        const int FBIOGET_HWCINFO = 0x4616;
        const int FBIOPUT_MODEINFO = 0x4617;
        const int FBIOGET_DISPINFO = 0x4618;
        //const int _IOW('F', 0x20, __u32) FBIO_WAITFORVSYNC;

        const int FB_TYPE_PACKED_PIXELS = 0;	/* Packed Pixels	*/
        const int FB_TYPE_PLANES = 1;	/* Non interleaved planes */
        const int FB_TYPE_INTERLEAVED_PLANES = 2;	/* Interleaved planes	*/
        const int FB_TYPE_TEXT = 3;	/* Text/attributes	*/
        const int FB_TYPE_VGA_PLANES = 4;	/* EGA/VGA planes	*/
        const int FB_TYPE_FOURCC = 5;	/* Type identified by a V4L2 FOURCC */

        public FrameBuffer()
        {
            var fb0Handle = Open("/dev/i2c-1", OPEN_READ_WRITE);
            if(fb0Handle == -1) {
                Console.WriteLine("Failed to open /dev/fb0 for read and write");
            } else {
                var frameBufferInfo = new FrameBufferVarScreenInfo();
                Console.WriteLine("Getting framebuffer info");
                var error = FrameBufferIoctl(fb0Handle, FBIOPUT_VSCREENINFO, ref frameBufferInfo);
                if(error < 0) {
                    Console.WriteLine("Failed to get frame buffer info");
                } else {
                    Console.WriteLine($"Frame buffer resolution: {frameBufferInfo.XResolution}x{frameBufferInfo.YResolution}");
                }

                Console.WriteLine("Closing device /dev/fb0");
                Close(fb0Handle);
            }
        }

        public void Dispose()
        {
        }
    }
}