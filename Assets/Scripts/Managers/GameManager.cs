using UnityEngine;
using System.Collections;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float _idleTimeRequired = 3f;

    [SerializeField] private float _idleVelocityThreshold = 0.1f;

    private bool _gameEnded;
    private int _birdsLeft;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _birdsLeft = FindObjectsOfType<Bird>().Length;
    }

    public void OnPigDied()
    {
        if (_gameEnded) return;
        _gameEnded = true;
        Lose();
    }

    public void OnBirdLaunched()
    {
        _birdsLeft--;
        if (_gameEnded) return;

        if (_birdsLeft <= 0)
            StartCoroutine(CheckWinCondition());
    }

    private IEnumerator CheckWinCondition()
    {
        float idleTimer = 0f;

        while (idleTimer < _idleTimeRequired)
        {
            bool anyMoving = FindObjectsOfType<Bird>()
                .Select(b => b.GetComponent<Rigidbody2D>())
                .Where(rb => rb != null)
                .Any(rb => rb.velocity.magnitude > _idleVelocityThreshold);

            if (anyMoving)
                idleTimer = 0f;  
            else
                idleTimer += Time.deltaTime;

            yield return null;
        }

        if (!_gameEnded)
        {
            _gameEnded = true;
            Win();
        }
    }

    private void Win()
    {
        Debug.Log("WIN!");
    }

    private void Lose()
    {
        Debug.Log("LOSE!");
    }
}
