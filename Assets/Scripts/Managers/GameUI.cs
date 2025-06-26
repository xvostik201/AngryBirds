using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    private float _currentScore;

    [SerializeField] private Button _reloadSceneButton;
    [SerializeField] private Button _muteGameButton;

    [SerializeField] private Sprite _muteOff;
    [SerializeField] private Sprite _muteOn;

    private Image _muteButtonImage;

    private void Awake()
    {
        _muteButtonImage = _muteGameButton.GetComponent<Image>();
    }

    private void Start()
    {
        _reloadSceneButton.onClick.AddListener(() => SceneLoader.LoadScene(-1));
        _muteGameButton.onClick.AddListener(TryToMute);

        bool isMuted = PlayerPrefs.GetInt(AudioManager.Instance.MusicKey, 0) == 1;
        _muteButtonImage.sprite = isMuted ? _muteOn : _muteOff;
    }

    private void TryToMute()
    {
        bool currentlyMuted = (_muteButtonImage.sprite == _muteOn);
        bool newMuteState = !currentlyMuted;

        AudioManager.Instance.ApplyMute(newMuteState, newMuteState);

        _muteButtonImage.sprite = newMuteState ? _muteOn : _muteOff;
    }

    private void Update()
    {
    }
}
