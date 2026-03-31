using UnityEngine;
using UnityEngine.SceneManagement;

public class Settingsbutton : MonoBehaviour
{
  public void GoToSettings()  
{  
    // Assuming you have a reference to your ScreenFader  
    ScreenFader fader = FindObjectOfType<ScreenFader>();  
    StartCoroutine(fader.FadeAndLoadScene("Settings", 1f)); // 1 second fade  
    Debug.Log("settings clicked");
}  
}
