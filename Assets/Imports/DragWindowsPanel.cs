using UnityEngine;  
using UnityEngine.EventSystems;  
  
// Attach this C# script to your Windows Panel or its title bar  
public class DragWindowsPanel : MonoBehaviour, IDragHandler  
{  
    public void OnDrag(PointerEventData eventData)  
    {  
        // Moves the panel as you drag it with the mouse  
        transform.position += (Vector3)eventData.delta;  
    }  
}
