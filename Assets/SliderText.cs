using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    /// TextMeshProUGUI Here__________________
    public TextMeshProUGUI groundBounceText;
    public TextMeshProUGUI groundStaticFriction;
    public TextMeshProUGUI groundDynamicFriction;

    public TextMeshProUGUI massText;
    public TextMeshProUGUI bounceText;
    public TextMeshProUGUI dragText;
    public TextMeshProUGUI ballSpeedText;
    public TextMeshProUGUI ballStaticFriction;
    public TextMeshProUGUI ballDynamicFriction;

    public TextMeshProUGUI batSpeedText;
    public TextMeshProUGUI sensitivityText;
    public TextMeshProUGUI batStaticFriction;
    public TextMeshProUGUI batDynamicFriction;
    public TextMeshProUGUI batBounce;

    /// Slider Here__________________
    public Slider groundBounceSlider;
    public Slider groundStaticSlider;
    public Slider groundDynamicSlider;

    public Slider massSlider;
    public Slider bounceSlider;
    public Slider dragSlider;
    public Slider ballSpeedSlider;
    public Slider ballStaticSlider;
    public Slider ballDynamicSlider;

    public Slider batSpeedSlider;
    public Slider sensitivitySlider;
    public Slider batStaticSlider;
    public Slider batDynamicSlider;
    public Slider batBounceSlider;

    private void Update()
    {
        groundBounceText.text = groundBounceSlider.value.ToString("0.00");
        groundStaticFriction.text = groundStaticSlider.value.ToString("0.0");
        groundDynamicFriction.text = groundDynamicSlider.value.ToString("0.0");
        
        massText.text = massSlider.value.ToString("0.00");
        bounceText.text = bounceSlider.value.ToString("0.00");
        dragText.text = dragSlider.value.ToString("0.00");
        ballSpeedText.text = ballSpeedSlider.value.ToString("0.00");
        ballStaticFriction.text = ballStaticSlider.value.ToString("0.0");
        ballDynamicFriction.text = ballDynamicSlider.value.ToString("0.0");

        batSpeedText.text = batSpeedSlider.value.ToString("0");
        sensitivityText.text = sensitivitySlider.value.ToString("0");
        batStaticFriction.text = batStaticSlider.value.ToString("0.0");
        batDynamicFriction.text = batDynamicSlider.value.ToString("0.0");
        batBounce.text = batBounceSlider.value.ToString("0.0");
    }
}
