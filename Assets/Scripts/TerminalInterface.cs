using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
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
                Viewer.InsertString($"{DirectoryManager.GetSubPath(CurrentDirectory, Application.dataPath)} : ");
                if (Dirs.Length == 0)
                {
                    Viewer.InsertString("No Directories");
                }
                else
                {
                    foreach (var dir in Dirs)
                    {
                        Viewer.InsertString($"Dir: {dir}");
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
                        if(!file.Contains(".meta")) Viewer.InsertString(file);
                    }
                }

                break;
            
            case "clear":
                string clearmsg = "";
                for (int i = 0; i < 30; i++)
                {
                    clearmsg += "\n";
                }
                Viewer.InsertString(clearmsg);
                break;
            
            case "pwd":
                Viewer.InsertString(DirectoryManager.GetCurrentDirectory(CurrentDirectory));
                break;
            
            case "mkdir":
                if (keys.Length < 2)
                {
                    ParseError();
                }
                else
                {
                    Directory.CreateDirectory(CurrentDirectory + Path.DirectorySeparatorChar + keys[1]);
                }
                break ;
            
            case "cd":
                if (keys.Length < 2)
                {
                    // Change to home directory if no argument is provided
                    CurrentDirectory = DirectoryManager.Home;
                    Viewer.InsertString($"=> {DirectoryManager.GetSubPath(CurrentDirectory, Application.dataPath)}");
                    return;
                }

                if (keys[1] == "..")
                {
                    // Navigate to the parent directory
                    CurrentDirectory = DirectoryManager.GetParentDirectory(CurrentDirectory);
                    Viewer.InsertString($"=> {DirectoryManager.GetSubPath(CurrentDirectory, Application.dataPath)}");
                    return;
                }

                // Construct the new path
                string newPath = Path.Combine(CurrentDirectory, keys[1]);

                if (Directory.Exists(newPath))
                {
                    // Change to the specified subdirectory if it exists
                    CurrentDirectory = newPath;
                    Viewer.InsertString($"=> {DirectoryManager.GetSubPath(CurrentDirectory, Application.dataPath)}");
                }
                else
                {
                    // Output error message if the directory does not exist
                    Viewer.InsertString("Folder Does not Exist");
                }
                break;
            
            case "rmd":
                if (keys.Length < 2)
                {
                    ParseError();
                    return;
                }

                string P = Path.Combine(CurrentDirectory, keys[1]);
                if (Directory.Exists(P))
                {
                    Directory.Delete(P, true);
                }
                else
                {
                    Viewer.InsertString("Directory Does not Exist");
                }
                break;
            default:
                ParseError();
                break;
        }
    }

    public void ParseError()
    {
        Viewer.InsertString("Command Does not Exist, Try Correcting Syntax or Help Command");
    }
}
