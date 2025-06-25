using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Destructible
{
    public override void Die()
    {
        ParticlePoolManager.Instance.PlayAnimationAt(transform);
        base.Die();
    }
}
