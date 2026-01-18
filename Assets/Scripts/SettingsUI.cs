using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    public Slider BGMSlider;

    public TextMeshProUGUI BGMSliderValue;

    public Slider SESlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        SetBGMValueText();

    }

    public void SetBGMValueText()
    {
        //SliderÇÃValueÇÕÇOÇ©ÇÁÇPÇ»ÇÃÇ≈ÇÌÇ©ÇËÇ‚Ç∑Ç≥èdéãÇ≈100î{Ç∑ÇÈ
        BGMSliderValue.text = $"{BGMSlider.value * 100 }";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
