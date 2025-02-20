# <div align="center">Installer Jazz</div>
<div align="center">The worst way to handle installing an application</div>

## How To Use This?

This is a combination of the installers that I have developed for [Micro Macro](https://github.com/ChobbyCode/MicroMacro), and [Terminal Chad](https://github.com/ChobbyCode/TerminalChad) which have been put into a collection of about 50 dlls to make it a bit easier to make an installer. If you are going to use this it would be easier to just use the windows installer thing. I only made this to be awkward.

## Step 1: Clone this repository

![image](https://github.com/user-attachments/assets/3f4284b7-fde7-44b5-a2b7-493add16db2a)

You can clone it this way, or through visual studio or vscode, or just use git. 

## Step 2: Make A Project & Add Project Reference 

![image](https://github.com/user-attachments/assets/c73aa001-3d72-4b06-ab2e-abec563c2092)

Make a new console application call it DemoInstaller or something. 

![image](https://github.com/user-attachments/assets/26532cdf-24bd-454f-a283-ac3408a2dd8b)

Add a project reference to the dll called InstallerJazz because that is where all the jazz happens. 

## Step 3: Copy This Silly Code

```
using InstallerJazz;
using InstallerJazz.Models;

namespace DemoInstaller;

public class Program {
    public static void Main(string[] args) {
        InstallArguments installArguments = new InstallArguments() {
            WindowName = "DemoInstaller", // Console name
            InstallerVersion = 0, // Doesn't actually do anything, may change
            AppVersion = 0, // Quite Important To Set This To The Correct Value
            RunInBackground = false, //Doesn't do anything for the moment

            SourceURL = "https://github.com/ChobbyCode/MicroMacro/zipball/InstallerFiles", // The url where it will download the program from
            VersionInfoURL = "https://github.com/ChobbyCode/MicroMacro/zipball/VersionInformation", // Information on the version of the application

            InstallDotNet = true, // Will it also install dotnet with the app?
            EnableInstallFeature = true, // Pretty Simple
            EnableUninstallFeature = true,
            EnableUpdateFeature = true,

            TargetLocation = "\\Program Files\\ChobbyCode\\", // Relative Location Where The App Will Be Installed
            AllowUsersToChooseInstallDrive = true, // If this is enabled it allows the users to choose full install drive. If not target location will have to be change to a full path so "C:\ProgramFiles..."
        };

        AppPackager appPackager = new AppPackager(args, installArguments);
    }
}
```

Have a read through the code, it's mainly just settings that, but is pretty useful. 

We are not done yet! Change the SourceURL to where your zipball of your app is going to be downloaded from, and VersionInforURL where a zipball of a json file will be downloaded from. we can do this on github for free.

## Step 4: Git Branches Evil Trick

![image](https://github.com/user-attachments/assets/2c2a84be-8c8d-4a37-9fa3-445fb32a9ebb)

As seen in the screenshot if we create a new branch on github (we can call it whatever we want), and just put the files that the installer will install from there. 
The url we will put into SourceURL. It will be in the format: https://github.com/[USERNAME]/[REPO]/zipball/[BRANCH]/

We also want to do the same for version information. However, on this branch we will have the only file be called: version.json. Copy the following to put in that file.:

```
{
    "Version": 1
}
```

So that AppVersion = 0 in the installArguments will be compared to this value. If this value is higher then it will install the app, but I don't think this actually works so don't use it. 
