using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Destructible
{
    [SerializeField] private AudioClip[] _deathClips;

    public override void Die()
    {
        AudioManager.Instance.PlayRandomSFX(_deathClips);
        ParticlePoolManager.Instance.PlayAnimationAt(transform);
        base.Die();
    }
}
