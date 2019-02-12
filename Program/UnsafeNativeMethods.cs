namespace Program
{
    using System;
    using System.Runtime.ConstrainedExecution;
    using System.Runtime.InteropServices;
    using System.Text;

    internal static class UnsafeNativeMethods
    {
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("libc", EntryPoint = "close", SetLastError = true)]
        internal static extern int Close(IntPtr handle);

        [DllImport("libc", EntryPoint = "ioctl", SetLastError = true)]
        internal static extern int Ioctl(SafeUnixHandle handle, uint request, ref FrameBufferVarScreenInfo capability);

        [DllImport("libc", EntryPoint = "open", SetLastError = true)]
        internal static extern SafeUnixHandle Open(string path, uint flag);

        internal static string Strerror(int error)
        {
            try
            {
                // var buffer = new StringBuilder(256);
                // var result = Strerror(error, buffer, (ulong)buffer.Capacity);
                // return (result != -1) ? buffer.ToString() : null;
                return error.ToString();
            }
            catch (EntryPointNotFoundException)
            {
                return null;
            }
        }

        // [DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_Syscall_strerror_r", SetLastError = true)]
        // private static extern int Strerror(int error, [Out] StringBuilder buffer, ulong length);
    }
}
