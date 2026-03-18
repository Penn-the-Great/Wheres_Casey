using UnityEngine;  
  
public class FileOpener : MonoBehaviour  
{  
    public GameObject fileDisplayPanel; // Drag your FileDisplayPanel here in the Inspector  
  
    public void OpenFilePanel()  
    {  
        fileDisplayPanel.SetActive(true); // Shows the file panel  
    }  
}