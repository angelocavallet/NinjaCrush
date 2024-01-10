using UnityEngine;
using UnityEngine.UI;

public class SfxAudioSourceSlider : MonoBehaviour
{
    private Slider slider;

    public void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = SoundManager.instance.sfxVolume;

        slider.onValueChanged.AddListener(delegate (float x) { SoundManager.instance.sfxVolume = x; });
    }
}
