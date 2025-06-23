using UnityEngine;
using System.Diagnostics;
using System.IO;
using System;

public class ApocalypseTrigger : MonoBehaviour
{
    public float timeUntilArmageddon = 60f; // Time until detonation (seconds)  
    private string deathMessage = "OH! YOU CRASH THE GAME! CONGRANTS!";

    // ==== WINDOWS TERROR TOOLS ====  
    void LaunchChaos()
    {
        // 1. COMMAND PROPHET OF DOOM  
        ExecuteTerminalKamikaze();

        // 2. NOTEPAD OBITUARY  
        LaunchNotepadTombstone();

        // 3. FAKE ERROR REQUIEM  
        TriggerWindowsScream();

        // KILL SELF (BUT LEAVE THE MESS)  
        Application.Quit();
    }

    void ExecuteTerminalKamikaze()
    {
        Process.Start("cmd.exe", $"/k echo {deathMessage} && title SYSTEM FAILURE && color C");
    }

    void LaunchNotepadTombstone()
    {
        string tempHellPath = Path.Combine(Path.GetTempPath(), "GRAVESTONE.txt");
        File.WriteAllText(tempHellPath, deathMessage);
        Process.Start("notepad.exe", tempHellPath);
    }

    void TriggerWindowsScream()
    {
        // Use VBScript for maximum cringe compatibility  
        string vbsCode = $"MsgBox \"{deathMessage}\", vbCritical+vbSystemModal, \"CRITICAL SYSTEM FAILURE\"";
        string vbsPath = Path.Combine(Path.GetTempPath(), "SCREAM.vbs");
        File.WriteAllText(vbsPath, vbsCode);
        Process.Start("wscript.exe", vbsPath);
    }
}