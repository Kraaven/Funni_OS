using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DirectoryManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static string Home = Path.Combine(Application.dataPath, "Home");
    void Start()
    {
        Directory.CreateDirectory(Home);
    }

    public static string[] GetAllDirectoriesInDirectory(string currentDir)
    {
        // Get all directories in the specified directory
        string[] rawDirList = Directory.GetDirectories(currentDir);

        // If there are no directories, return an empty array
        if (rawDirList.Length == 0)
            return new string[0];

        // Create an array to store the directory names
        string[] directoryNames = new string[rawDirList.Length];

        // Extract directory names from full paths
        for (int i = 0; i < rawDirList.Length; i++)
        {
            // Get only the directory name, not the full path
            directoryNames[i] = Path.GetFileName(rawDirList[i]);
        }

        return directoryNames;
    }
    
    public static string[] GetAllFilesInDirectory(string currentDir)
    {
        // Get all file paths in the specified directory
        string[] rawFileList = Directory.GetFiles(currentDir);

        // If there are no files, return an empty array
        if (rawFileList.Length == 0)
            return new string[0];

        // Create an array to store the file names
        string[] fileNames = new string[rawFileList.Length];

        // Extract file names from full paths
        for (int i = 0; i < rawFileList.Length; i++)
        {
            // Get only the file name, not the full path
            fileNames[i] = Path.GetFileName(rawFileList[i]);
        }

        return fileNames;
    }
    
    public static string GetSubPath(string fullPath, string basePath)
    {
        // Normalize paths to use the correct directory separator for the current OS
        string normalizedFullPath = fullPath.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
        string normalizedBasePath = basePath.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);

        // Ensure the basePath ends with a separator for accurate extraction
        if (!normalizedBasePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            normalizedBasePath += Path.DirectorySeparatorChar;
        }

        // Check if the fullPath starts with the basePath
        if (normalizedFullPath.StartsWith(normalizedBasePath, StringComparison.OrdinalIgnoreCase))
        {
            // Remove the basePath from the fullPath
            return normalizedFullPath.Substring(normalizedBasePath.Length);
        }
        else
        {
            // Return the fullPath if basePath is not a prefix
            return fullPath;
        }
    }


    public static string GetCurrentDirectory(string CurrentDir)
    {
        return GetSubPath(CurrentDir, Application.dataPath);
    }
    
    public static string GetParentDirectory(string fullPath)
    {
        // Normalize path separators
        string normalizedPath = fullPath.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);

        // Get the directory name of the parent directory
        string parentDirectory = Path.GetDirectoryName(normalizedPath);

        return parentDirectory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
