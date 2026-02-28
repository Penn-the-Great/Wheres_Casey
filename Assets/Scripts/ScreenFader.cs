using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;

       void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void FadeButtonClick()
    {
        StartCoroutine(FadeInOut(1f, 2f));
    }

    public void StartFadeIn(float duration)
    {
        StartCoroutine(FadeIn(duration));
        Debug.Log("Fading");
        
    }
    
    public IEnumerator FadeAndLoadScene(string sceneName, float fadeDuration)  
{  
    // Fade to black  
    yield return StartCoroutine(FadeIn(fadeDuration));  
    // Now load the scene  
    UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName); 
     yield return StartCoroutine(FadeOut(fadeDuration));
}  
    
   
   public IEnumerator FadeInOut(float fadeDuration, float waitTime)  
{  

    yield return StartCoroutine(FadeIn(fadeDuration));  
  
   
    yield return new WaitForSeconds(waitTime);  
  
    
    yield return StartCoroutine(FadeOut(fadeDuration));  

    StartCoroutine(FadeInOut(1f, 2f));
}  
   
   
   
    public IEnumerator FadeIn(float duration)
    {
        Color c = fadeImage.color;
        for (float t = 0; t <duration; t += Time.deltaTime)
        {
            c.a = Mathf.Lerp(0, 1, t / duration);
            fadeImage.color = c;
            yield return null;
            
        }
        c.a = 1;
        fadeImage.color = c;

        
    }

    public IEnumerator FadeOut(float duration)
    {
        Color c = fadeImage.color;
        for (float t = 0; t <duration; t += Time.deltaTime)
        {
            c.a = Mathf.Lerp(1, 0, t / duration);
            fadeImage.color = c;
            yield return null;
        }
        c.a = 0;
        fadeImage.color = c;
    }




}
