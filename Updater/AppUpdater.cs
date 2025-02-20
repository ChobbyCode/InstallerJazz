using InstallerJazz.FileUtils.SourceDownloader;
using InstallerJazz.Updater.Models;
using Newtonsoft.Json;

namespace InstallerJazz.Updater {
    public class AppUpdater {
        public bool isUpdate(string VersionInfoUrl, int currentVersion) {
            SourceDownloader sourceDownloader = new SourceDownloader();
            List<FileInfo> files = new List<FileInfo>();
            IEnumerable<string> DownloadedFiles = sourceDownloader.DownloadFilesConFiles(VersionInfoUrl);    
            foreach(string DownloadedFile in DownloadedFiles) {
                files.Add(new FileInfo(DownloadedFile));
            }
            var versionInfo = JsonConvert.DeserializeObject<VersionInfo>(File.ReadAllText(files.Where(f => f.Name == "version.json").FirstOrDefault().FullName));
            if (versionInfo.Version > currentVersion) return true;
            else return false;
        }
    }
}
