using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerminalInterface : MonoBehaviour
{
    public CommandViewer Viewer;
    public string PreviousCommand;

    private string CurrentDirectory = DirectoryManager.Home;
    
    public void RunCommand(string command)
    {
        PreviousCommand = command;
        Viewer.InsertString($"> {command}");   
        
        ParseAndRunCommand(command);
    }

    private void ParseAndRunCommand(string Command)
    {
        string[] keys = Command.Split(" ");

        if (keys.Length == 0)
        {
            Viewer.InsertString("> ");
            return;
        }
        
        switch (keys[0])
        {
            case "":
                break;
            case " ":
                break;
            
            
            case "ls":
                string[] Dirs = DirectoryManager.GetAllDirectoriesInDirectory(CurrentDirectory);
                if (Dirs.Length == 0)
                {
                    Viewer.InsertString("No Directories");
                }
                else
                {
                    foreach (var dir in Dirs)
                    {
                        Viewer.InsertString(dir);
                    }
                }

                string[] Files = DirectoryManager.GetAllFilesInDirectory(CurrentDirectory);
                if (Files.Length == 0)
                {
                    Viewer.InsertString("No Files");
                }
                else
                {
                    foreach (var file in Files)
                    {
                        Viewer.InsertString(file);
                    }
                }

                break;
            default:
                Viewer.InsertString("Command Does not Exist, Try Correcting Syntax or Help Command");
                break;
        }
    }
}
