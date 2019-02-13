# LinuxFrameBuffer

## Description

This is an experimental library to use the Linux Frame Buffer directly from with C# by using as few PInvoke calls as possible.

Long story short, it works, but I failed.

I'm not a P/Invoke expert and have never really been overly comfortable using it, but I was laying in bed sick with nothing else to do, so I gave it a go. 

There are issues I'm sure that others probably could help with.

1) I used an [https://stackoverflow.com/questions/10387603/p-invoke-ioctl-system-call](article) from 2012 on Stack Overflow to get through most of my marshalling issues. This solution is old and crusty and there must be more evolved solutions that are made for .NET Core instead of Mono. That said, it worked..  well mostly.

2) I should have made an MmapSafeHandle class to protect the memory mapped region

3) All drawing operations currently require a call to Marshal.WriteInt32() for every pixel. This is REALLY REALLY a bad idea. I suppose I could switch to using unsafe... but generally I feel unsafe doing so.

4) There is a MemoryMappedFile class, but I couldn't for the life of me get it to work the the frame buffer. Besides, it's using shared memory sys calls instead of MMAP. In addition, it set the share mode to share read which won't work for me.

As I have a sick obsession with pixel pushing going back to the 1980's I'll probably spend some time playing with this. There's even a chance I'll force myself to implement a windowing toolkit based on a 3D canvas just for personal entertainment. Please don't expect anything there to be worth using as other than reference.

