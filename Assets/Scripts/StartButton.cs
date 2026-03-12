using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
  public void StartGame()  
{  
    // Assuming you have a reference to your ScreenFader  
    ScreenFader fader = FindObjectOfType<ScreenFader>();  
    StartCoroutine(fader.FadeAndLoadScene("Game", 1f)); // 1 second fade  
    
}  
}

