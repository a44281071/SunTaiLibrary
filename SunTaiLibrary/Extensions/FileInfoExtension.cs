using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
    /// <summary>
    /// More append methods for <see cref="FileInfo"/>.
    /// </summary>
    public static class FileInfoExtension
    {
        /// <summary>
        /// Opens the file if it exists and seeks to the end of the file, or creates a new file. 
        /// </summary>
        /// <param name="file">file need to open.</param>
        /// <returns>A file opened in the append mode, with write access and unshared.</returns>
        public static Stream OpenAppend(this FileInfo file)
        {
            using Stream ss = file.Open(FileMode.Append, FileAccess.Write, FileShare.None);
            return ss;
        }
    }
}
