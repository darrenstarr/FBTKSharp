
using System;
using LinuxFrameBuffer;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            FrameBuffer fb = new FrameBuffer();
            fb.FillRectangle(10,100,100,100,255,0,0);
            fb.FillRectangle(110,100,100,100,0,255,0);
            fb.FillRectangle(210,100,100,100,0,0,255);
        }
    }
}
