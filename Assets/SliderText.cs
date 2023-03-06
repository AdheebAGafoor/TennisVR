using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    public TextMeshProUGUI massText;
    public TextMeshProUGUI bounceText;
    public TextMeshProUGUI ballSpeedText;
    public TextMeshProUGUI dragText;
    public TextMeshProUGUI groundBounceText;
    public TextMeshProUGUI batSpeedText;
    public TextMeshProUGUI SensitivityText;

    public Slider massSlider;
    public Slider bounceSlider;
    public Slider ballSpeedSlider;
    public Slider dragSlider;
    public Slider groundBounceSlider;
    public Slider batSpeedSlider;
    public Slider SensitivitySlider;

    private void Update()
    {
        massText.text = massSlider.value.ToString("0.00");
        bounceText.text = bounceSlider.value.ToString("0.00");
        ballSpeedText.text = ballSpeedSlider.value.ToString("0.00");
        dragText.text = dragSlider.value.ToString("0.00");
        groundBounceText.text = groundBounceSlider.value.ToString("0.00");
        batSpeedText.text = batSpeedSlider.value.ToString("0");
        SensitivityText.text = SensitivitySlider.value.ToString("0");
    }
}
