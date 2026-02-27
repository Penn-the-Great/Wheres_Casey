using UnityEngine;
using UnityEngine.SceneManagement;

public class Backbutton : MonoBehaviour
{
  public void GoToTitle()  
{  
    // Assuming you have a reference to your ScreenFader  
    ScreenFader fader = FindObjectOfType<ScreenFader>();  
    StartCoroutine(fader.FadeAndLoadScene("Title", 1f)); // 1 second fade  
    
}  
}
