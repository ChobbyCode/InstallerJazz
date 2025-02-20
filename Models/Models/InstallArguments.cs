
namespace InstallerJazz.Models {
    public class InstallArguments {
        // Generic Settings
        public string WindowName { get; set; } = "ChobbyCode Installer";
        public int InstallerVersion { get; set; } = 3;
        public int AppVersion { get; set; } = 0;
        public bool RunInBackground { get; set; } = false;
        // Where To Download App
        public string SourceURL { get; set; }
        public string VersionInfoURL { get; set; }
        public bool IsZipBall { get; set; } = true;
        public bool GithubMode { get; set; } = true;
        // How To Deal With Downloaded Files
        public bool EnableInstallFeature { get; set; } = true;
        public bool EnableUpdateFeature { get; set; } = false;
        public bool EnableUninstallFeature { get; set; } = false;
        // User Interaction Settings
        public string TargetLocation { get; set; } = "\\Program Files\\ChobbyCode\\";
        public bool AllowUsersToChooseInstallDrive { get; set; } = true;
        public bool AllowUsersToChooseFullInstallPath { get; set; } = false;
    }
}
