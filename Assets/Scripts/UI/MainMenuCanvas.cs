using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : BaseScreen
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private Button _muteButton;

    [SerializeField] private Sprite _muteOff;
    [SerializeField] private Sprite _muteOn;
    private Image _muteImage;

    private void Awake()
    {
        _muteImage = _muteButton.GetComponent<Image>();
    }

    private void Start()
    {
        SetupReload(_playButton, 1);  
        SetupReload(_exitButton, 0);   
        SetupMute(_muteButton, _muteImage, _muteOff, _muteOn);
    }
}
