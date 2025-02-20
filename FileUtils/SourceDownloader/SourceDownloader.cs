using System.IO.Compression;
using System.Net;

namespace InstallerJazz.FileUtils.SourceDownloader {
    public class SourceDownloader {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        public void DownloadAndCopy(string sourceUrl, string targetPath) {
            DownloadFiles(sourceUrl);

            string DownloadLocation = Directory.GetDirectories(baseDir + @"\tmp\dez\")[0];
            DirectoryInfo sourceDir = new(DownloadLocation);
            DirectoryInfo targetDir = new(targetPath);

            Files.FileUtils.CopyFiles(sourceDir, targetDir);
        }

        public IEnumerable<string> DownloadFilesConFiles(string sourceUrl) {
            DownloadFiles(sourceUrl);
            string DownloadLocation = Directory.GetDirectories(baseDir + @"\tmp\dez\")[0];
            return Directory.GetFiles(DownloadLocation);
        }

        public void DownloadFiles(string sourceUrl) {
            if (Directory.Exists(baseDir + @"\tmp\")) Directory.Delete(baseDir + @"\tmp\", true);
            if (Directory.Exists(baseDir + @"\Files\")) Directory.Delete(baseDir + @"\Files\", true);

            if (!Directory.Exists(baseDir + @"\tmp\")) Directory.CreateDirectory(baseDir + @"\tmp\");
            if (!Directory.Exists(baseDir + @"\tmp\zip")) Directory.CreateDirectory(baseDir + @"\tmp\zip");
            if (!Directory.Exists(baseDir + @"\tmp\dez")) Directory.CreateDirectory(baseDir + @"\tmp\dez");
            if (!Directory.Exists(baseDir + @"\Files\")) Directory.CreateDirectory(baseDir + @"\Files\");

            using (var client = new WebClient()) {
                client.DownloadFile(sourceUrl, baseDir + @"\tmp\zip\download.zip");
            }

            ZipFile.ExtractToDirectory(baseDir + @"\tmp\zip\download.zip", baseDir + @"\tmp\dez\");
        }
    }
}
