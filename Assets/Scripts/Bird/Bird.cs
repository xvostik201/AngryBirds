using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private BirdConfig _config;

    private TrailRenderer _trail;
    private Rigidbody2D _rb;
    private Slingshot _slingshot;
    private Animator _anim;
    private bool _isActive, _isFlying, _hasPressed;

    private int _collisionSoundsPlayed;
    private float _lastCollisionSoundTime;
    [SerializeField] private float _collisionSoundCooldown = 0.1f;
    [SerializeField] private int _maxCollisionSounds = 3;
    [SerializeField] private Transform _birdLineRendererPosition;
    private bool _hasReseted;

    public Transform BirdLineRendererPosition => _birdLineRendererPosition;
    public float SpecialShootForce => _config.SpecialShootForce;
    public float GravityScale => _config.GravityScale;
    public bool IsDragging { get; private set; }
    public Rigidbody2D Rigidbody2D => _rb;

    public void Initialize(Slingshot slingshot)
    {
        _slingshot = slingshot;
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _trail = GetComponent<TrailRenderer>();
        _rb.gravityScale = 0f;
        _rb.isKinematic = true;
        _trail.enabled = false;
    }

    private void Update()
    {
        if (!_isActive && !_rb.isKinematic && _isFlying)
            RotateAlongVelocity();

        if (_isFlying && !_hasPressed && Input.GetMouseButtonDown(0) && _config.TypeOfBird == BirdType.Yellow)
        {
            _hasPressed = true;
            _rb.AddForce(transform.right * _config.AdditionalForce, ForceMode2D.Impulse);
            AudioManager.Instance.PlaySFX(_config.SpecialAbilityClip);
        }
    }

    private void RotateAlongVelocity()
    {
        var v = _rb.velocity;
        if (v.sqrMagnitude > _config.FlightRotationThreshold * _config.FlightRotationThreshold)
        {
            float ang = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, ang);
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
        Vector3 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m.z = 0;
        var dir = m - _slingshot.BirdShootPoint.position;
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
        _trail.enabled = true;
        AudioManager.Instance.PlayRandomSFX(_config.FlyingClips);
    }

    public void SetActiveForLaunch(bool active)
    {
        _isActive = active;
        if (active)
        {
            _collisionSoundsPlayed = 0;
            _lastCollisionSoundTime = -Mathf.Infinity;
            _trail.enabled = false;
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
            _rb.gravityScale = 0f;
            _rb.isKinematic = true;
            _anim.enabled = true;
        }
    }

    private void LookAtSlingshot()
    {
        var to = _slingshot.BirdShootPoint.position - transform.position;
        float ang = Mathf.Atan2(to.y, to.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, ang);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _anim.enabled = false;
        _isFlying = false;

        if(!_hasReseted)
            CameraManager.Instance.ResetToInitial();

        _hasReseted = true;

        if (_collisionSoundsPlayed < _maxCollisionSounds &&
            Time.time - _lastCollisionSoundTime >= _collisionSoundCooldown)
        {
            AudioManager.Instance.PlayRandomSFX(_config.CollisionClips);
            _collisionSoundsPlayed++;
            _lastCollisionSoundTime = Time.time;
        }
    }
}
