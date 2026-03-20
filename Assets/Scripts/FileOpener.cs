using UnityEngine;  
using UnityEngine.EventSystems;
  
public class FileOpener : MonoBehaviour, IPointerClickHandler
{  
    public GameObject imageToDisplay;

    public void OnPointerClick(PointerEventData eventData)
    {
        DisplayImage();
    }

    public void DisplayImage()
    {
        if(imageToDisplay != null)
        {
            imageToDisplay.SetActive(true);
        }
    }

    public void HideImage()
    {
        if (imageToDisplay != null)
        {
            imageToDisplay.SetActive(false);
        }
    }
      
}