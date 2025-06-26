using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _scoreAdditionalSpeedValue = 750f;
    private float _currentScore;
    private float _displayedScore;            

    [SerializeField] private Button _reloadSceneButton;
    [SerializeField] private Button _muteGameButton;

    [SerializeField] private Sprite _muteOff;
    [SerializeField] private Sprite _muteOn;

    private Image _muteButtonImage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _muteButtonImage = _muteGameButton.GetComponent<Image>();
    }

    private void Start()
    {
        _reloadSceneButton.onClick.AddListener(() => SceneLoader.LoadScene(-1));
        _muteGameButton.onClick.AddListener(TryToMute);

        bool isMuted = PlayerPrefs.GetInt(AudioManager.MusicKey, 0) == 1;
        _muteButtonImage.sprite = isMuted ? _muteOn : _muteOff;

        _displayedScore = _currentScore;
        _scoreText.text = _displayedScore.ToString();
    }

    private void TryToMute()
    {
        bool currentlyMuted = _muteButtonImage.sprite == _muteOn;
        bool newMuteState = !currentlyMuted;

        AudioManager.Instance.ApplyMute(newMuteState, newMuteState);
        _muteButtonImage.sprite = newMuteState ? _muteOn : _muteOff;
    }

    public void AddScore(float value)
    {
        _currentScore += value;
    }

    private void Update()
    {
        if (!Mathf.Approximately(_displayedScore, _currentScore))
        {
            _displayedScore = Mathf.MoveTowards(
                _displayedScore,
                _currentScore,
                 _scoreAdditionalSpeedValue * Time.deltaTime
            );
            _scoreText.text = _displayedScore.ToString("0");
        }
    }
}
