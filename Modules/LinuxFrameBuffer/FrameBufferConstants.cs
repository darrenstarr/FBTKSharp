namespace LinuxFrameBuffer
{
    internal class FrameBufferConstants
    {
        public const int FBIOGET_VSCREENINFO = 0x4600;
        public const int FBIOPUT_VSCREENINFO = 0x4601;
        public const int FBIOGET_FSCREENINFO = 0x4602;
        public const int  FBIOGETCMAP = 0x4604;
        public const int  FBIOPUTCMAP = 0x4605;
        public const int  FBIOPAN_DISPLAY = 0x4606;
        //const int _IOWR('F', 0x08, struct fb_cursor) FBIO_CURSOR;
        /* 0x4607-0x460B are defined below */
        public const int FBIOGET_CON2FBMAP = 0x460F;
        public const int FBIOPUT_CON2FBMAP = 0x4610;
        public const int FBIOBLANK = 0x4611;		/* arg: 0 or vesa level + 1 */
        //const int _IOR('F', 0x12, struct fb_vblank) FBIOGET_VBLANK;
        public const int FBIO_ALLOC = 0x4613;
        public const int FBIO_FREE = 0x4614;
        public const int FBIOGET_GLYPH = 0x4615;
        public const int FBIOGET_HWCINFO = 0x4616;
        public const int FBIOPUT_MODEINFO = 0x4617;
        public const int FBIOGET_DISPINFO = 0x4618;
        //const int _IOW('F', 0x20, __u32) FBIO_WAITFORVSYNC;

        public const int FB_TYPE_PACKED_PIXELS = 0;	/* Packed Pixels	*/
        public const int FB_TYPE_PLANES = 1;	/* Non interleaved planes */
        public const int FB_TYPE_INTERLEAVED_PLANES = 2;	/* Interleaved planes	*/
        public const int FB_TYPE_TEXT = 3;	/* Text/attributes	*/
        public const int FB_TYPE_VGA_PLANES = 4;	/* EGA/VGA planes	*/
        public const int FB_TYPE_FOURCC = 5;	/* Type identified by a V4L2 FOURCC */
    }
}