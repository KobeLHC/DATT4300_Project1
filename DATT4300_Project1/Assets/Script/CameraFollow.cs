using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform[] targetArray;
    public float smoothSpeed = 0.125f;
    public int currentIndex = 1;

    public Vector3 offset;
    
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if(currentIndex < targetArray.Length - 1)
            {
                currentIndex++;
                transform.position = targetArray[currentIndex].position + offset;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                transform.position = targetArray[currentIndex].position + offset;
            }
        }
    }
}
