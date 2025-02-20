using InstallerJazz.Installer;
using InstallerJazz.Models;
using Updater;

namespace DemoInstaller {
    public class Program {
        public static void Main(string[] args) {
            InstallArguments arguments = new InstallArguments() {
                WindowName = "ChobbyCode Installer",
                InstallerVersion = 3,
                AppVersion = 1,
                RunInBackground = false,
                SourceURL = "https://github.com/ChobbyCode/MicroMacro/zipball/InstallerFiles",
                VersionInfoURL = "https://github.com/ChobbyCode/MicroMacro/zipball/VersionInformation",
                IsZipBall = true,
                GithubMode = true,
                EnableInstallFeature = true,
                EnableUpdateFeature = true,
                EnableUninstallFeature = true,

                TargetLocation = "E:/test/",
            };
            AppUpdater updater = new AppUpdater();
            updater.isUpdate(arguments.VersionInfoURL);
        }
    }
}