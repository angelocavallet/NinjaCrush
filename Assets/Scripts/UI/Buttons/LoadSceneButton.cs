using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public void loadScene(string sceneName)
    {
        if (GameManager.instance) GameManager.instance.Continue();
        SoundManager.instance.NextSoundTrack();
        SceneManager.LoadScene(sceneName);
    }
}
