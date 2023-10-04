using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

int captcherPoints = 0;
int crackerPoints = 2;

var files = Directory.GetFiles("../user-data");
files = files
    .OrderBy(f => Random.Shared.Next())
    .Take(8)
    .ToArray();

Process cmd = new Process();
cmd.StartInfo.FileName = "cmd.exe";
cmd.StartInfo.RedirectStandardInput = true;
cmd.StartInfo.RedirectStandardOutput = true;
cmd.StartInfo.CreateNoWindow = true;
cmd.StartInfo.UseShellExecute = false;
cmd.Start();

cmd.StandardInput.WriteLine("cd ..");
cmd.StandardInput.WriteLine("cd captcher-app");

// Skip lines
cmd.StandardOutput.ReadLine();
cmd.StandardOutput.ReadLine();
cmd.StandardOutput.ReadLine();
cmd.StandardOutput.ReadLine();
cmd.StandardOutput.ReadLine();
cmd.StandardOutput.ReadLine();

foreach (var file in files)
{
    cmd.StandardInput.WriteLine($"dotnet run '{file}'");
    // Skip lines
    cmd.StandardOutput.ReadLine();
    cmd.StandardOutput.ReadLine();
    if (cmd.StandardOutput.ReadLine() == "User")
        captcherPoints++;
}

