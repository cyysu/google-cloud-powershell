# Cloud Tools for PowerShell (Beta)

[![Build status](https://ci.appveyor.com/api/projects/status/r11ovv4348852ktt?svg=true)](https://ci.appveyor.com/project/GoogleCloudPowerShell/google-cloud-powershell)

This repository contains PowerShell cmdlets for interacting with the Google
Cloud Platform. You can use Cloud Tools for PowerShell to manage your existing
cloud resources or create new ones.

# Installation

Cloud Tools for PowerShell is included in the Windows version of the
[Google Cloud SDK](https://cloud.google.com/sdk/docs/quickstart-windows).

If you already have the Cloud SDK installed on your machine, you can uninstall
and reinstall to get Cloud Tools for PowerShell. Or, you can install it
manually via:

    # Install the "powershell" component of the Cloud SDK
    $ gcloud components install powershell

    # Run "AppendPsModulePath.ps1" to register the Cloud Tools for PowerShell
    # module. Where the script is located depends on where you installed the
    # Cloud SDK. For normal user-based installs:
    $env:LOCALAPPDATA\Google\Cloud SDK\google-cloud-sdk\platform\PowerShell\GoogleCloud\1.0.0.0
    
    # For admin-based installs:
    ${env:ProgramFiles(x86)}\Google\Cloud SDK\google-cloud-sdk\platform\PowerShell\GoogleCloud\1.0.0.0

Cloud Tools for PowerShell uses your Cloud SDK credentials. So if you have not
already, run `gcloud auth login` or `gcloud init` to login.

# Documentation

You can learn more about Cloud Tools for PowerShell, with Quick Start and How-To
guides, at https://cloud.google.com/powershell/.

You can see a full cmdlet reference at:
https://googlecloudplatform.github.io/google-cloud-powershell/

# Support

To get help on using these cmdlets, please
[log an issue](https://github.com/GoogleCloudPlatform/google-cloud-powershell/issues/new)
with this project. While we will eventually be able to offer support on
StackOverflow, but for now your best bet is to contact the dev team directly.

Patches are encouraged, and may be submitted by forking this project and
submitting a Pull Request. See [CONTRIBUTING.md](CONTRIBUTING.md) for more
information.

# License

Apache 2.0. See [LICENSE](LICENSE) for more information.
