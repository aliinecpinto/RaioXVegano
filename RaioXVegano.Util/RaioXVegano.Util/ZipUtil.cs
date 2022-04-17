using System.IO;
using System.IO.Compression;

namespace RaioXVegano.Util
{
    public static class ZipUtil
    {
        public static void GeraZip(string originPath, string destinyPath, string zipName)
        {
            string zipFile = Path.Combine(destinyPath, zipName);

            if (File.Exists(zipFile))
            {
                File.Delete(zipFile);
            }

            ZipFile.CreateFromDirectory(originPath, zipFile);
        }
    }
}
