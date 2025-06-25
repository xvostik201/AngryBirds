using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : Destructible
{
    [SerializeField] private AudioClip[] _deathClips;

    public override void Die()
    {
        AudioManager.Instance.PlayRandomSFX(_deathClips);
        ParticlePoolManager.Instance.PlayAnimationAt(transform);
        base.Die();
    }
}
