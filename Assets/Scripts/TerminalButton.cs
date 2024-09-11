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
        if (Input.GetKeyDown(KeyCode.Return) && TerminalApplication.ActiveTerminal != null)
        {
            TerminalApplication.ActiveTerminal.Interface.RunCommand(TerminalApplication.ActiveTerminal.TerminalInput.text);
            TerminalApplication.ActiveTerminal.TerminalInput.text = "";
            TerminalApplication.ActiveTerminal.TerminalInput.ActivateInputField();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && TerminalApplication.ActiveTerminal != null)
        {
            TerminalApplication.ActiveTerminal.TerminalInput.text =
                TerminalApplication.ActiveTerminal.Interface.PreviousCommand;
            TerminalApplication.ActiveTerminal.TerminalInput.ActivateInputField();
            TerminalApplication.ActiveTerminal.TerminalInput.caretPosition =
                TerminalApplication.ActiveTerminal.TerminalInput.text.Length;
        }
    }

    public void CreateTerminal()
    {
        Instantiate(TerminalPrefab, Terminals.transform);
    }
}
