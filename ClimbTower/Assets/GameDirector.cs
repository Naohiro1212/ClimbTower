using UnityEngine;
using TMPro;

public class GameDirector : MonoBehaviour
{
    GameObject timerText;
    MoyaControll moyaScript;
    CameraController cameraScript;
    float time = 0.0f;

    void Start()
    {
        this.timerText = GameObject.Find("Timer");
        GameObject moya = GameObject.Find("Moya");
        GameObject camera = GameObject.Find("Main Camera");
        if(moya != null)
        {
            this.moyaScript = moya.GetComponent<MoyaControll>();
        }
        if(camera != null)
        {
            this.cameraScript = camera.GetComponent<CameraController>();
        }
    }

    void Update()
    {
        this.time += Time.deltaTime;

        if (this.time >= 15.0f)
        {
            this.moyaScript.moyaSpd = 1.5f;
            this.cameraScript.scrollSpd = 1.5f;
        }
        else if (this.time >= 10.0f)
        {
            this.moyaScript.moyaSpd = 1.2f;
        }
        else if(this.time >= 5.0f)
        {
            this.moyaScript.moyaSpd = 1.0f;
        }
        else
        {
            this.moyaScript.moyaSpd = 0.8f;
        }

        this.timerText.GetComponent<TextMeshProUGUI>().text = this.time.ToString("F1");
    }
}
