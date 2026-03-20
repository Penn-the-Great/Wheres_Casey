using UnityEngine;
using TMPro;

public class NotepadWindow : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI displayText;

    void Start()
    {
        if(inputField != null)
        {
            inputField.onValueChanged.AddListener(OnTextChanged);

            inputField.onEndEdit.AddListener(OnTextSubmitted);
        }
    }

    void OnTextChanged(string newText)
    {
        Debug.Log("User typed:" + newText);
    }

    void OnTextSubmitted(string submittedText)
    {
        Debug.Log("User submitted:" + submittedText);

        if (displayText != null)
        {
            displayText.text = submittedText;
        }
    }
    
    public string GetInputText()
    {
        return inputField.text;
    }
}
