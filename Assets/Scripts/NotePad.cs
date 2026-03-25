using UnityEngine;
using TMPro;

public class NotePad : MonoBehaviour
{
  private TextMeshProUGUI displayText;
  private string noteText = "";
  private int cursorPosition = 0;

  void Update()
  {
    if(Input.GetKeyDown(KeyCode.RightArrow)) cursorPosition = Mathf.Max(0, cursorPosition -1);

        if (Input.GetKeyDown(KeyCode.RightArrow)) cursorPosition = Mathf.Min(noteText.Length, cursorPosition +1);

        foreach (char c in Input.inputString)
        {
            if (c == '\b')
            {
                if (cursorPosition > 0)
                {
                    noteText = noteText.Remove(cursorPosition - 1, 1);
                    cursorPosition--;
                }
            }
            else if (c == '\n')
            {
                noteText = noteText.Insert(cursorPosition, "\n");
            }
            else
            {
                noteText = noteText.Insert(cursorPosition, c.ToString());
                cursorPosition++;
            }
            
        }

        string displayWithCursor = noteText.Insert(cursorPosition, "|");
        displayText.text = displayWithCursor;
  }
}
