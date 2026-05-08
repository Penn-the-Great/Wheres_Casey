using UnityEngine;

public class QuitManager : MonoBehaviour
{
    public void QuitGame()
    {
        // This closes the application for built games
        Application.Quit();
        
        // This shows it's working in the Unity Editor console
        Debug.Log("Game is exiting...");
        
        // Optional: Exit Play Mode while testing in the Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}