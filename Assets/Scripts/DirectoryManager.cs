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

    public static string[] GetAllDirectoriesInDirectory(string CurrentDir)
    {
       string[] RawDirList = Directory.GetDirectories(CurrentDir);
       
       print(RawDirList.Length);
       foreach (var s in RawDirList)
       {
           print(s);
       }
       
       if (RawDirList.Length == 0) return RawDirList;

       string[] DirectoryNames = new string[RawDirList.Length];

       for (int i = 0; i < RawDirList.Length; i++)
       {
           DirectoryNames[i] = "Dir: " + Path.GetDirectoryName(RawDirList[i]);
       }

       return DirectoryNames;
    }
    
    public static string[] GetAllFilesInDirectory(string CurrentDir)
    {
        string[] RawFileList = Directory.GetFiles(CurrentDir);
        if (RawFileList.Length == 0) return RawFileList;

        string[] FileNames = new string[RawFileList.Length];

        for (int i = 0; i < RawFileList.Length; i++)
        {
            FileNames[i] =  Path.GetDirectoryName(RawFileList[i]);
        }

        return FileNames;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
