
namespace InstallerJazz.Models {
    public class InstallArguments {
        // Generic Settings
        public string WindowName { get; set; }
        public int InstallerVersion { get; set; }
        public int AppVersion { get; set; }
        public bool RunInBackground { get; set; }
        // Where To Download App
        public string SourceURL { get; set; }
        public string VersionInfoURL { get; set; }
        public bool IsZipBall { get; set; }
        public bool GithubMode { get; set; }
        // How To Deal With Downloaded Files
        public bool EnableInstallFeature { get; set; }
        public bool EnableUpdateFeature { get; set; }
        public bool EnableUninstallFeature { get; set; }
        // User Interaction Settings
        public string TargetLocation { get; set; }
        public bool AllowUsersToChooseInstallDrive { get; set; }
        public bool AllowUsersToChooseFullInstallPath { get; set; }
    }
}
