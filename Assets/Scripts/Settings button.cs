using UnityEngine;
using UnityEngine.SceneManagement;

public class Settingsbutton : MonoBehaviour
{
  public void GoToSettings()
       {
        Debug.Log("Button Pressed!");
        SceneManager.LoadScene("Settings");
       }
        
}
