using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public void loadScene(string sceneName)
    {
        SoundManager.instance.NextSoundTrack();
        SceneManager.LoadScene(sceneName);
    }
}
