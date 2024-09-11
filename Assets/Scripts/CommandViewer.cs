using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CommandViewer : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public int maxLines = 5; // Adjust this based on how many lines can fit in the element
    
    private string fullText = ""; // Store the entire text content

    public void InsertString(string newLine)
    {
        // Append the new line at the end
        fullText += newLine + "\n";

        // Assign the updated text to TextMeshPro
        textMeshPro.text = fullText;

        // Force an update to calculate line info (important for wrapping)
        textMeshPro.ForceMeshUpdate();

        // Get the line count
        int totalLines = textMeshPro.textInfo.lineCount;

        // Check if lines are overflowing the maximum visible lines
        while (totalLines > maxLines)
        {
            // Remove the first line and reassign the text
            int firstLineEnd = fullText.IndexOf('\n') + 1;
            fullText = fullText.Substring(firstLineEnd);

            textMeshPro.text = fullText;

            // Force an update again to check the new line count
            textMeshPro.ForceMeshUpdate();

            // Update total lines count after trimming
            totalLines = textMeshPro.textInfo.lineCount;
        }
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         InsertString($"Random: {GetString()}");
    //     }
    // }

    // public string GetString()
    // {
    //     string msg = "";
    //
    //     for (int i = 0; i < Random.Range(5,30); i++)
    //     {
    //         msg += Random.Range(1, 10000);
    //     }
    //
    //     return msg;
    // }
}