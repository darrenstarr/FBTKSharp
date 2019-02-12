

namespace Program
{
    using System;
    using System.Linq;
    using System.Runtime.ConstrainedExecution;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FrameBufferBitField
    {
        public UInt32 Offset;
        public UInt32 Length;
        public UInt32 MsbRight;
    }

    [StructLayout(LayoutKind.Sequential, Size=160)]
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
            
            var fb0Handle = UnsafeNativeMethods.Open("/dev/fb0", OPEN_READ_WRITE);
            if(fb0Handle.IsInvalid) {
                var errno = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                Console.WriteLine($"Failed to open /dev/fb0 for read and write {fb0Handle}");

                Console.WriteLine("Error code is {0}", errno);
            } else {
                var frameBufferInfo = new FrameBufferVarScreenInfo();
                var buffer = new byte[160];
                Console.WriteLine("Getting framebuffer info");
                var result = UnsafeNativeMethods.Ioctl(fb0Handle, FBIOGET_VSCREENINFO, ref frameBufferInfo);
                if(result < 0) {
                    throw new UnixIOException();
                } else {
                    Console.WriteLine($"Frame buffer resolution: {frameBufferInfo.XResolution}x{frameBufferInfo.YResolution}");
                }

                Console.WriteLine("Closing device /dev/fb0");
                fb0Handle.Close();
            }
        }

        public void Dispose()
        {
        }
    }
}