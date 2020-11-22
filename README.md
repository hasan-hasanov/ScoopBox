<h1 align="center">
  <br>
  <img width="500" alt="scoopBoxLogo" src="assets/Logo.png">
  <br>
  ScoopBox
  <br>
</h1>

<h4 align="center">ScoopBox is library that helps launch Windows Sandbox with preinstalled applications and/or with predefined scripts.</h4>
<h4 align="center">:star: Stars on GitHub always helps!</h4>

<p align="center">
  <a href="">
    <img src="https://img.shields.io/badge/nuget-scoopbox-green" alt="nuget">
  </a>
</p>

<p align="center">
  <a href="#more-about-windows-sandbox">More About Windows Sandbox</a> •
  <a href="#how-scoopbox-works">How ScoopBox Works</a> •
  <a href="#examples">Examples</a> •
  <a href="#download">Download</a> •
  <a href="#contribute">Contribute</a>
</p>

## More About Windows Sandbox

### What Is Windows Sandbox
Technically Windows Sandbox is a lightweight virtual machine created on demand which a user can safely run applications in isolation. This virtual machine is using the same OS image as in the host machine. Software installed inside the Windows Sandbox environment remains "sandboxed" and runs separately from the base machine.

### What are the drawbacks
A sandbox is temporary. When it's closed, all the software and files and the state are deleted. You get a brand-new instance of the sandbox every time you open the application.

Software and applications installed on the host aren't directly available in the sandbox. If you need specific applications available inside the Windows Sandbox environment, they must be explicitly installed within the environment every time.

### What is ScoopBox
Since the state clears every time ScoopBox helps you launch the Windows Sandbox with preinstalled applications using a package managers like Scoop, Chocolate, Winget etc..

## How ScoopBox Works

### Windows Sandbox Configuration File
Windows Sandbox supports simple configuration files, which provide a minimal set of customization parameters for Sandbox. This feature can be used with Windows 10 build 18342 or later. Windows Sandbox configuration files are formatted as XML and are associated with Sandbox via the .wsb file extension.

A configuration file enables the user to control the following aspects of Windows Sandbox:

* **vGPU (virtualized GPU)**: Enable or disable the virtualized GPU. If vGPU is disabled, the sandbox will use Windows Advanced Rasterization Platform (WARP).
* **Networking:** Enable or disable network access within the sandbox.
* **Mapped folders:** Share folders from the host with read or write permissions. Note that exposing host directories may allow malicious software to affect the system or steal data.
* **Logon command:** A command that's executed when Windows Sandbox starts.
* **Audio input:** Shares the host's microphone input into the sandbox.
* **Video input:** Shares the host's webcam input into the sandbox.
* **Protected client:** Places increased security settings on the RDP session to the sandbox.
* **Printer redirection:** Shares printers from the host into the sandbox.
* **Clipboard redirection:** Shares the host clipboard with the sandbox so that text and files can be pasted back and forth.
* **Memory in MB:** The amount of memory, in megabytes, to assign to the sandbox.

### Simple Configuration File
Here is how a simple configuration file looks.
```xml 
<Configuration>
  <VGpu>Disable</VGpu>
  <Networking>Disable</Networking>
  <MappedFolders>
    <MappedFolder>
      <HostFolder>C:\Users\Public\Downloads</HostFolder>
      <SandboxFolder>C:\Users\WDAGUtilityAccount\Downloads</SandboxFolder>
      <ReadOnly>true</ReadOnly>
    </MappedFolder>
  </MappedFolders>
  <LogonCommand>
    <Command>explorer.exe C:\users\WDAGUtilityAccount\Downloads</Command>
  </LogonCommand>
</Configuration>
```
Add appropriate configuration attributes and save the file with the desired name, but make sure its filename extension is **.wsb**

### ScoopBox
ScoopBox takes advantage of **LogonCommand** and **Command** parts of the configuration. A user can write a script map it from the host machine to sandbox machine and it will execute on startup.
That's what ScoopBox does. It automates building of the configuration file and generates a MainScript which inside contains commands to execute user's or pacakage manager's scripts using powershell. So a user basically can execute scripts and take advantage of build in package managers to install applications.

## Examples

### Start Windows Sandbox with preinstalled applications

```csharp
ISandbox sandbox = new Sandbox();
await sandbox.Run(new ScoopPackageManagerScript(new List<string>() { "curl", "fiddler", "vscode" }));
```

### Start Windows Sandbox with user scripts

```csharp
ISandbox sandbox = new Sandbox();
await sandbox.Run(new List<IScript>()
{
    new ExternalScript(new FileInfo(@"C:\Users\Scripts\StartBrowser.ps1"), new PowershellTranslator()),
    new ExternalScript(new FileInfo(@"C:\Users\Scripts\StartExplorer.ps1"), new PowershellTranslator()),
});
```
### Start Windows Sandbox with combined scripts

```csharp
ISandbox sandbox = new Sandbox();
await sandbox.Run(new List<IScript>()
{
    // Powershell script as string to open notepad
    new LiteralScript(new List<string>() { @"Start-Process 'C:\windows\system32\notepad.exe'" }, new PowershellTranslator()),

    // Cmd script to open a browser
    new ExternalScript(new FileInfo(@"C:\Users\Scripts\StartBrowser.cmd"), new CmdTranslator()),

    // Bat script to open explorer
    new ExternalScript(new FileInfo(@"C:\Users\Scripts\OpenExplorer.bat"), new BatTranslator()),

    // Scoop package manager that installs curl and fiddler
    new ScoopPackageManagerScript(new List<string>(){ "curl", "fiddler" }),
});
```
**All scripts are ran in the order they are defined.**

### Modify sandbox configuration file

```csharp
IOptions options = new Options()
{
    AudioInput = AudioInputOptions.Enable,
    PrinterRedirection = PrinterRedirectionOptions.Enable,
    Networking = NetworkingOptions.Default,
    VGpu = VGpuOptions.Enabled,
    // ...
};

ISandbox sandbox = new Sandbox(options);
```
### Debugging sandbox

Since there is no way of debugging windows sandbox scripts or get any feedback, ScoopBox generates log file for every script. If the user has provided 5 scripts he should see 5 log files in desktop. Sadly this is the only feedback that came to my mind. If you have a better suggestion open an issue and lets discuss it.

## Download

### Coming soon on nuget!!!

## Contribute

### Did you find a bug?

Ensure the bug was not already reported by searching on GitHub under Issues.
If you're unable to find an open issue addressing the problem, open a new one. Be sure to include a title and clear description, as much relevant information as possible.

### Did you write a patch that fixes a bug?

Open a new GitHub pull request with the patch.
Ensure the PR description clearly describes the problem and solution. Include the relevant issue number if applicable.

## Did you fix whitespace, format code, or make a purely cosmetic patch?
Open a new GitHub pull request with the patch.
