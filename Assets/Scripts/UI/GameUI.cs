using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : BaseScreen
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _scoreAdditionalSpeedValue = 3000f;
    private float _currentScore;
    private float _displayedScore;

    [SerializeField] private Button _reloadSceneButton;

    [SerializeField] private Button _muteGameButton;
    [SerializeField] private Sprite _muteOff;
    [SerializeField] private Sprite _muteOn;
    private Image _muteImage;

    public static GameUI Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _scoreText.text = _displayedScore.ToString();
        _muteImage = _muteGameButton.GetComponent<Image>();
    }

    private void Start()
    {
        SetupReload(_reloadSceneButton, -1);
        SetupMute(_muteGameButton, _muteImage, _muteOff, _muteOn);
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
