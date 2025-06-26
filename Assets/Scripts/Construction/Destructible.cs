using TMPro;
using UnityEngine;

public abstract class Destructible : MonoBehaviour, IDestructible
{
    [SerializeField] private DestructibleConfig _config;
    [SerializeField] private GameObject _additionalTextPrefab;

    protected Rigidbody2D _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        float relSpeed = collision.relativeVelocity.magnitude;
        if (relSpeed > _config.BreakForce)
        {
            Die();
        }
        else if (relSpeed > 0.2f)
        {
            AudioManager.Instance.PlayRandomSFX(_config.ImpactClips);
        }
    }

    public virtual void Die()
    {
        AudioManager.Instance.PlayRandomSFX(_config.DeathClips);

        if (_additionalTextPrefab != null)
        {
            GameObject textGO = Instantiate(
                _additionalTextPrefab,
                transform.position,
                Quaternion.identity
            );

            var tmp = textGO.GetComponent<TMP_Text>();
            if (tmp != null)
                tmp.text = _config.Reward.ToString();

        }

        GameUI.Instance.AddScore(_config.Reward);
        ParticlePoolManager.Instance.PlayAnimationAt(transform);
        Destroy(gameObject);
    }
}
