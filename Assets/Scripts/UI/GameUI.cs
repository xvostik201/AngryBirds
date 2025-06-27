using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : BaseScreen
{
    [Header("score")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _scoreAdditionalSpeedValue = 3000f;
    private float _currentScore;
    private float _displayedScore;

    [Header("IGUB")]
    [SerializeField] private Button _reloadSceneButton;

    [SerializeField] private Button _muteGameButton;
    [SerializeField] private Sprite _muteOff;
    [SerializeField] private Sprite _muteOn;
    private Image _muteImage;

    [Header("LEVEL STATE PANEL")]
    [SerializeField] private GameObject _levelStatePanel;

    [SerializeField] private GameObject _winObj;
    [SerializeField] private GameObject _loseObj;
    [SerializeField] private GameObject _pauseObj;
    private bool _hasPaused = false;


    [SerializeField] private Button _loadNextLevelBT;
    [SerializeField] private Button _reloadLevelBT;
    [SerializeField] private Button _exitToMenuBT;
    [SerializeField] private Button _pauseBT;
    [SerializeField] private Button _continueBT;





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
        SetupReload(_reloadLevelBT, -1);
        SetupMute(_muteGameButton, _muteImage, _muteOff, _muteOn);

        _loadNextLevelBT.onClick.AddListener(() => SceneLoader.LoadScene(-1, false, true));
        _exitToMenuBT.onClick.AddListener(() => SceneLoader.LoadScene(0));
        _pauseBT.onClick.AddListener(() => Pause());
        _continueBT.onClick.AddListener(() => Pause());
    }

    private void Pause()
    {
        _hasPaused = !_hasPaused;

        float timeScale = _hasPaused ? 0f : 1f;
        TimeManager.ChangeTimeScale(timeScale);

        _levelStatePanel.SetActive(_hasPaused);
        _pauseObj.SetActive(_hasPaused);
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

    public void ActivateStatePanel(bool win)
    {
        _levelStatePanel.SetActive(true);
        if(win)
            _winObj.SetActive(true);
        else
            _loseObj.SetActive(true);
    }
}
