using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool forceScroll = false;
    public float scrollSpd = 1.0f;
    public float maxPosY = 10.0f;
    [SerializeField] private GameNormalTimer gameNormalTimer;

    void Update()
    {
        if (forceScroll && gameNormalTimer.IsStop() == false)
        {
            this.transform.position += new Vector3(0, scrollSpd * Time.deltaTime, 0);
            if (this.transform.position.y > maxPosY)
            {
                forceScroll = false;
            }
        }
    }
}