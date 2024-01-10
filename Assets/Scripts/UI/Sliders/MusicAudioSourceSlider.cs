using UnityEngine;
using UnityEngine.UI;

public class MusicAudioSourceSlider : MonoBehaviour
{
    private Slider slider;

    public void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = SoundManager.instance.musicVolume;

        slider.onValueChanged.AddListener(delegate (float x) { SoundManager.instance.musicVolume = x; });
    }
}
