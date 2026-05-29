using UnityEngine;
using UnityEngine.UI;

public class TimerSliderController : MonoBehaviour
{
    private float timer = 0.0f;
    [SerializeField] private Slider timerSlider;
    private GameNormalTimer gameNormalTimer;

    void Start()
    {
        gameNormalTimer = FindAnyObjectByType<GameNormalTimer>();
        timerSlider.value = 0f;
    }

    void Update()
    {
        timer = gameNormalTimer.GetTime();
        float limit = gameNormalTimer.GetLimit();
        float t = Mathf.Clamp01(timer / limit);
        timerSlider.value = t;
    }
}
