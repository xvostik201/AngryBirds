using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Slingshot : MonoBehaviour
{
    [SerializeField] private LineRenderer[] _lineRenderers;
    [SerializeField] private Transform _birdShootPoint;
    [SerializeField] private Bird[] _allBirdsToLevel;
    [SerializeField] private float _defaultShootForce = 5f;
    [SerializeField] private float _maxDragDistance = 3f;


    private int _currentIndex = 0;
    private Bird _currentBird;

    public Transform BirdShootPoint => _birdShootPoint;
    public float MaxDragDistance => _maxDragDistance;

    private void Start()
    {
        foreach (var bird in _allBirdsToLevel)
            bird.Initialize(this);

        SpawnNextBird();
    }

    private void Update()
    {
        if (_currentBird != null && _currentBird.IsDragging)
        {
            for (int i = 0; i < _lineRenderers.Length; i++)
            {
                _lineRenderers[i].enabled = true;
                _lineRenderers[i].SetPosition(0, _lineRenderers[i].transform.position);
                _lineRenderers[i].SetPosition(1, _currentBird.transform.position);
            }
        }
        else
        {
            foreach (var lr in _lineRenderers)
                lr.enabled = false;
        }
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

        float dist = Vector3.Distance(bird.transform.position, _birdShootPoint.position);
        bird.SetActiveForLaunch(false);
        bird.Rigidbody2D.AddForce(
            bird.transform.right * _defaultShootForce * dist * bird.SpecialShootForce,
            ForceMode2D.Impulse
        );
        bird.Rigidbody2D.gravityScale = bird.GravityScale;

        _currentIndex++;

        Invoke(nameof(SpawnNextBird), 5f);
    }
}
