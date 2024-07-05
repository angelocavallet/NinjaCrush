using UnityEngine;
using UnityEngine.UI;

public class MusicAudioSourceSlider : MonoBehaviour
{
    private Slider slider;

    public void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = GameManager.instance.soundManager.musicVolume;

        slider.onValueChanged.AddListener(delegate (float x) { GameManager.instance.soundManager.musicVolume = x; });
    }
}
