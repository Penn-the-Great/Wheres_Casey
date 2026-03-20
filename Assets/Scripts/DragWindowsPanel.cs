using UnityEngine;  
using UnityEngine.EventSystems;  
  
// Attach this C# script to your Windows Panel or its title bar  
public class DragWindowsPanel : MonoBehaviour, IDragHandler  
{  

    [SerializeField] private bool clampToScreen = true;
    [SerializeField] private Canvas canvas;

    void Start()
    {
        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }
    }


    public void OnDrag(PointerEventData eventData)  
    {  
        // Moves the panel as you drag it with the mouse  
        transform.position += (Vector3)eventData.delta;  

        if (clampToScreen)
        {
            ClampToBounds();
        }
    }  

     private void ClampToBounds()
    {
        if (canvas == null) return;
        
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        RectTransform panelRect = GetComponent<RectTransform>();
        
        Vector3[] canvasCorners = new Vector3[4];
        canvasRect.GetWorldCorners(canvasCorners);
        
        Vector3[] panelCorners = new Vector3[4];
        panelRect.GetWorldCorners(panelCorners);
        
    
        if (panelCorners[0].x < canvasCorners[0].x)
        {
            transform.position += Vector3.right * (canvasCorners[0].x - panelCorners[0].x);
        }
        
      
        if (panelCorners[2].x > canvasCorners[2].x)
        {
            transform.position += Vector3.left * (panelCorners[2].x - canvasCorners[2].x);
        }
        

        if (panelCorners[0].y < canvasCorners[0].y)
        {
            transform.position += Vector3.up * (canvasCorners[0].y - panelCorners[0].y);
        }
        
   
        if (panelCorners[2].y > canvasCorners[2].y)
        {
            transform.position += Vector3.down * (panelCorners[2].y - canvasCorners[2].y);
        }
    }

}
