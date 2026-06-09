using UnityEngine;

public class MoyaControll : MonoBehaviour
{
    public float moyaSpd = 0.5f;
    public float posMaxY = 20.0f;
    [SerializeField] private GameNormalTimer gameNormalTimer;

    void Start()
    {
        if (gameNormalTimer != null)
        {
            Debug.Log("ノーマルタイムが入っていません");
        }
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if(transform.position.y < posMaxY && gameNormalTimer.IsStop() == false)
        {
            transform.Translate(Vector3.up * moyaSpd * Time.deltaTime);
        }
    }
}
