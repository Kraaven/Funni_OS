using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TerminalButton : MonoBehaviour
{
    public RectTransform Terminals; 
    public TerminalApplication TerminalPrefab;

    public CanvasGroup GRP;

    public static CanvasGroup GlobalCanvasGRP; 
    // Start is called before the first frame update
    void Start()
    {
        GlobalCanvasGRP = GRP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTerminal()
    {
        Instantiate(TerminalPrefab, Terminals.transform);
    }
}
