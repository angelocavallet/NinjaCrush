using UnityEngine;
using UnityEngine.UI;

public class MasterAudioSourceSlider : MonoBehaviour
{
    private Slider slider;

    public void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = GameManager.instance.soundManager.masterVolume;

        slider.onValueChanged.AddListener(delegate (float x) { GameManager.instance.soundManager.masterVolume = x; });
    }
}
