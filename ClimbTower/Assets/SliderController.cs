using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private CameraController cameraController;

    private float startY;
    private float maxY;
    private float goalposY = 5;

    void Start()
    {
        cameraController = FindAnyObjectByType<CameraController>();

        if (cameraController != null)
        {
            startY = cameraController.transform.position.y;
            maxY = cameraController.maxPosY - goalposY;
        }

        slider.value = 0f;
    }

    void Update()
    {
        if (cameraController == null)
        {
            return;
        }

        float currentY = cameraController.transform.position.y;

        float totalDistance = maxY - startY;

        if (totalDistance > 0)
        {
            float progress = (currentY - startY) / totalDistance;

            slider.value = Mathf.Clamp01(progress);
        }

    }
}
