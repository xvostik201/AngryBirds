using System;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public static event Action<Bird> OnBirdLaunched;

    [SerializeField] private LineRenderer[] _lineRenderers;
    [SerializeField] private LineRenderer _trajectoryRenderer;

    [SerializeField] private Transform _birdShootPoint;

    [SerializeField] private Bird[] _allBirdsToLevel;

    [SerializeField] private float _defaultShootForce = 5f;

    [SerializeField] private float _maxDragDistance = 3f;

    [SerializeField] private int _trajectoryPoints = 30;
    [SerializeField] private float _timeStep = 0.1f;

    private int _currentIndex = 0;
    private Bird _currentBird;

    public Transform BirdShootPoint => _birdShootPoint;
    public float MaxDragDistance => _maxDragDistance;

    private void Start()
    {
        _allBirdsToLevel = FindObjectsOfType<Bird>();

        foreach (var bird in _allBirdsToLevel)
            bird.Initialize(this);

        SpawnNextBird();

        _trajectoryRenderer.positionCount = _trajectoryPoints;
        _trajectoryRenderer.enabled = false;
    }

    private void Update()
    {
        if (_currentBird != null && _currentBird.IsDragging)
        {
            for (int i = 0; i < _lineRenderers.Length; i++)
            {
                _lineRenderers[i].enabled = true;
                _lineRenderers[i].SetPosition(0, _lineRenderers[i].transform.position);
                _lineRenderers[i].SetPosition(1, _currentBird.BirdLineRendererPosition.position);
            }

            DrawTrajectory();
        }
        else
        {
            foreach (var lr in _lineRenderers)
                lr.enabled = false;

            _trajectoryRenderer.enabled = false;
        }
    }

    private void DrawTrajectory()
    {
        Vector3 startPos = _birdShootPoint.position;
        Vector3 dir = (_currentBird.transform.position - startPos).normalized;
        float dist = Vector3.Distance(_currentBird.transform.position, startPos);
        Vector3 velocity = -dir * _defaultShootForce * dist * _currentBird.SpecialShootForce;
        Vector3 gravity = Physics2D.gravity;

        for (int i = 0; i < _trajectoryPoints; i++)
        {
            float t = i * _timeStep;
            Vector3 point = startPos + velocity * t + .65f * (Vector3)gravity * t * t;
            _trajectoryRenderer.SetPosition(i, point);
        }

        _trajectoryRenderer.enabled = true;
    }

    private void SpawnNextBird()
    {
        if (_currentIndex >= _allBirdsToLevel.Length)
        {
            Debug.Log("Птицы кончились, lose - win");
            return;
        }

        _currentBird = _allBirdsToLevel[_currentIndex];
        _currentBird.transform.position = _birdShootPoint.position;
        _currentBird.SetActiveForLaunch(true);
    }

    public void Shoot(Bird bird)
    {
        if (bird != _currentBird) return;

        OnBirdLaunched?.Invoke(bird);

        float dist = Vector3.Distance(bird.transform.position, _birdShootPoint.position);

        bird.SetActiveForLaunch(false);
        bird.Rigidbody2D.AddForce(
            bird.transform.right * _defaultShootForce * dist * bird.SpecialShootForce,
            ForceMode2D.Impulse
        );
        bird.Rigidbody2D.gravityScale = bird.GravityScale;

        _trajectoryRenderer.enabled = false;

        _currentIndex++;
        Invoke(nameof(SpawnNextBird), 1.5f);
    }
}
