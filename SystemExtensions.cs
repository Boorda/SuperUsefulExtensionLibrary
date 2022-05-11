namespace SUELIB.SystemExtensions
{
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using System.Threading.Tasks;

    using System.Collections.Concurrent;
    using SUELIB.CollectionExtensions;

    public static class SystemExtensions
    {

        #region File & Directory
        /// <summary>
        /// Checks a file path to ensure it does not already exist. <para/>
        /// If it does it appends an incremented number inside of parenthesis. ex.. FileName (1), FileName (2), etc.
        /// </summary>
        /// <param name="path">Path to check for duplicate file names.</param>
        public static string CreateFileNameWithParentheses(this string path)
        {
            if (!path.IsExistingFile()) { return path; }
            var dir = Path.GetDirectoryName(path);
            var fileName = Path.GetFileNameWithoutExtension(path);
            var fileExt = Path.GetExtension(path);
            var files = Directory.GetFiles(dir);
            int incr = 1;
            foreach (var filepath in files)
            {
                var fn = Path.GetFileNameWithoutExtension(filepath);
                var cp = $"{fileName} ({incr})";
                if (fn == cp) { incr++; }
            }
            var newPath = Path.Combine(dir, $"{fileName} ({incr}){fileExt}");
            return newPath;
        }

        /// <summary>
        /// Gets file name without the extension from the specified path.
        /// </summary>
        /// <param name="path">Path to get file name from.</param>
        /// <returns></returns>
        public static string GetFileNameNoExtention(this string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        /// <summary>
        /// Gets all files with the specified extensions.
        /// </summary>
        /// <param name="dir">Directory to search.</param>
        /// <param name="extensions">Return files matching these extensions.</param>
        public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo dir, out int count, params string[] extensions)
        {
            try
            {
                if (extensions == null) { throw new ArgumentNullException("extensions"); }
                ConcurrentBag<FileInfo> files = new ConcurrentBag<FileInfo>();
                count = 0;

                var tFile = dir.EnumerateFiles();
                var tfilteredfiles = tFile.Select(x => x).Where(f => extensions.Contains($"*{f.Extension}".ToUpper()));
                files.AddRange(tfilteredfiles);

                Parallel.ForEach(dir.GetDirectories(), new ParallelOptions() { MaxDegreeOfParallelism = 10 }, (subDir) =>
                {
                    if (subDir.Attributes.HasFlag(FileAttributes.System))
                    {
                        return;
                    }

                    var allFile = subDir.EnumerateFiles();
                    var filteredfiles = allFile.Select(x => x).Where(f => extensions.Contains($"*{f.Extension.ToUpper()}"));
                    files.AddRange(filteredfiles);
                });

                count = files.Count();
                return files;
            }
            catch
            {
                count = 0;
                return null;
            }

        }

        /// <summary>
        /// Returns a boolean value indicating whether or not the specified directory exists.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsExistingDirectory(this string path)
        {
            var dir = path;
            if (Path.HasExtension(path)) { dir = Path.GetDirectoryName(path); }
            return Directory.Exists(dir);
        }

        /// <summary>
        /// Returns a boolean value indicating whether or not the specified file exists.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsExistingFile(this string path, bool throwExceptions = false)
        {
            if (!File.Exists(path))
            {
                if (throwExceptions) { throw new FileNotFoundException($"The path {path} does not exist."); }
                return false;
            }
            return true;
        }

        /// <summary>
        /// Returns a boolean value indicating whether or not the specified path (file or directory) exists.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsExistingPath(this string path)
        {
            if (File.Exists(path) || Directory.Exists(path)) { return true; }
            return false;
        }
        #endregion

    }
}
