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
        else if(relSpeed > 0.2f)
        {
            AudioManager.Instance.PlayRandomSFX(_config.ImpactClips);
        }
    }

    public virtual void Die()
    {
        AudioManager.Instance.PlayRandomSFX(_config.DeathClips);

        GameObject textGO = Instantiate(_additionalTextPrefab, transform.position, Quaternion.identity);
        textGO.GetComponent<TextMeshPro>().text = _config.Reward.ToString();
        
        ParticlePoolManager.Instance.PlayAnimationAt(transform);
        Destroy(gameObject);
    }
}
