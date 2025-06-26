using UnityEngine;
using UnityEngine.UI;

public abstract class BaseScreen : MonoBehaviour
{
    protected void SetupReload(Button button, int sceneIndex)
    {
        button.onClick.AddListener(() => SceneLoader.LoadScene(sceneIndex));
    }

    protected void SetupMute(
        Button button,
        Image iconImage,
        Sprite iconOff,
        Sprite iconOn)
    {
        bool isMuted = PlayerPrefs.GetInt(AudioManager.MusicKey, 0) == 1;
        iconImage.sprite = isMuted ? iconOn : iconOff;

        button.onClick.AddListener(() =>
        {
            bool newState = !iconImage.sprite.Equals(iconOn);
            AudioManager.Instance.ApplyMute(newState, newState);
            iconImage.sprite = newState ? iconOn : iconOff;
        });
    }
}
