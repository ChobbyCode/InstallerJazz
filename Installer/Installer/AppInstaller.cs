using InstallerJazz.Models;
using InstallerJazz.FileUtils.SourceDownloader;

namespace InstallerJazz.Installer {
    public class AppInstaller {
        public AppInstaller(InstallArguments InstallArguments) {
            SourceDownloader sourceDownloader = new SourceDownloader();
            sourceDownloader.DownloadAndCopy(InstallArguments.SourceURL, InstallArguments.TargetLocation);
        }
    }
}
