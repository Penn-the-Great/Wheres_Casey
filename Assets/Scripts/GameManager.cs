using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip ambientClip;  // Assign in Inspector

    private void Start()
    {
        if (ambientClip != null)
        {
            Ambience.instance.PlayAmbientSound(ambientClip, 0.3f);
        }
    }

}
