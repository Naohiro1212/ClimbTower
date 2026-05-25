using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool forceScroll = false;
    public float scrollSpd = 1.0f;

    void Update()
    {
        if(forceScroll)
        {
            this.transform.position += new Vector3(0, scrollSpd * Time.deltaTime, 0);
        }
    }
}
