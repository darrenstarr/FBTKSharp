# LinuxFrameBuffer

## Description

This is an experimental library to use the Linux Frame Buffer directly from with C# by using as few PInvoke calls as possible.

Long story short, it works, but I failed.

I'm not a P/Invoke expert and have never really been overly comfortable using it, but I was laying in bed sick with nothing else to do, so I gave it a go. 

There are issues I'm sure that others probably could help with.

1) I used an [https://stackoverflow.com/questions/10387603/p-invoke-ioctl-system-call](article) from 2012 on Stack Overflow to get through most of my marshalling issues. This solution is old and crusty and there must be more evolved solutions that are made for .NET Core instead of Mono. That said, it worked..  well mostly.

2) 
