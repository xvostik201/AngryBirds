using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    private float _currentScore;

    [SerializeField] private Button _reloadSceneButton;
    [SerializeField] private Button _muteGameButton;

    [SerializeField] private Sprite _muteOff;
    [SerializeField] private Sprite _muteOn;
    void Start()
    {
        _reloadSceneButton.onClick.AddListener(() => SceneLoader.LoadScene(-1));
    }

    void Update()
    {
        
    }
}
