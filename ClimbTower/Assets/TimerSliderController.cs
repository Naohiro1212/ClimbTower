using UnityEngine;
using UnityEngine.UI;

public class TimerSliderController : MonoBehaviour
{
    private float timer = 0.0f;
    [SerializeField]private float limit = 60.0f; 
    [SerializeField] private Slider timerSlider; 
    private GameNormalTimer gameNormalTimer;

    void Start()
    {
        gameNormalTimer = FindAnyObjectByType<GameNormalTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer = gameNormalTimer.GetTime();
        float t = timer / limit;
        timerSlider.value = Mathf.Lerp(0f, 1f, t);
    }
}
