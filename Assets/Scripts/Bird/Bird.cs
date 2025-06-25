using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private BirdType _typeOfBird;

    [SerializeField] private float _specialShootForce = 2f;
    [SerializeField] private float _gravityScale = 1f;
    [SerializeField] private float _flightRotationThreshold = 0.1f;

    [Header("Yellow bird settings")]
    [SerializeField] private float _additionalForce = 1.5f;
    [SerializeField] private bool _hasPressed = false;

    private bool _isActive = false;
    private bool _isFlying = false;
    private Rigidbody2D _rb;
    private Slingshot _slingshot;
    private Animator _animator;

    public float SpecialShootForce => _specialShootForce;
    public float GravityScale => _gravityScale;
    public bool IsDragging { get; private set; }
    public Rigidbody2D Rigidbody2D => _rb;

    public void Initialize(Slingshot slingshot)
    {
        _slingshot = slingshot;
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        _rb.isKinematic = true;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_isActive && !_rb.isKinematic && _isFlying)
        {
            Vector2 vel = _rb.velocity;
            if (vel.sqrMagnitude > _flightRotationThreshold * _flightRotationThreshold)
            {
                float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
        if (_isFlying && !_hasPressed && Input.GetMouseButtonDown(0))
        {
            _hasPressed = true;
            _rb.AddForce(transform.right * _additionalForce, ForceMode2D.Impulse);
        }
    }

    private void OnMouseDown()
    {
        if (!_isActive) return;
        IsDragging = true;
        _rb.isKinematic = true;
    }

    private void OnMouseDrag()
    {
        if (!_isActive) return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector3 dir = mouseWorld - _slingshot.BirdShootPoint.position;
        if (dir.magnitude > _slingshot.MaxDragDistance)
            dir = dir.normalized * _slingshot.MaxDragDistance;

        transform.position = _slingshot.BirdShootPoint.position + dir;
        LookAtSlingshot();
    }

    private void OnMouseUp()
    {
        if (!_isActive) return;
        IsDragging = false;
        _rb.isKinematic = false;
        _isFlying = true;
        _slingshot.Shoot(this);
    }

    public void SetActiveForLaunch(bool active)
    {
        _isActive = active;
        if (active)
        {
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
            _rb.gravityScale = 0f;
            _rb.isKinematic = true;
            _animator.enabled = true;
        }
    }

    private void LookAtSlingshot()
    {
        Vector3 toSlingshot = _slingshot.BirdShootPoint.position - transform.position;
        float angle = Mathf.Atan2(toSlingshot.y, toSlingshot.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _animator.enabled = false;
        _isFlying = false;
    }
}
