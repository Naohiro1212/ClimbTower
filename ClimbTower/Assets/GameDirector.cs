using UnityEngine;
using TMPro;

public class GameDirector : MonoBehaviour
{
    [SerializeField] private MoyaControll moyaScript;
    [SerializeField] private CameraController cameraScript;
    float time = 0.0f;

    /// <summary>
    /// 各段階のスピードと、何秒経った後にスピードを上げるか
    /// </summary>
    private const int speedStepsNum = 4;
    private const int speedUpTimingsNum = 3;

    [SerializeField] private float[] speedSteps = new float[speedStepsNum];
    [SerializeField] private float[] speedUpTiming = new float[speedUpTimingsNum];


    void Start()
    {
        // モヤスクリプトが見つからなければデバグ出力
        if (moyaScript == null)
        {
            Debug.Log("モヤが指定されていません");
        }

        // カメラスクリプトが見つからなければデバグ出力
        if (cameraScript == null)
        {
            Debug.Log("カメラが指定されていません");
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
    }
}
