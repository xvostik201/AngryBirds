using UnityEngine;

[CreateAssetMenu(fileName = "DestructibleConfig", menuName = "ScriptableObject/DestructibleConfig")]
public class DestructibleConfig : ScriptableObject
{
    public DestructibleType Type;
    public float BreakForce;
    public float Reward;
    public Color TextColor = Color.green;
    public AudioClip[] ImpactClips;
    public AudioClip[] DeathClips;
}

