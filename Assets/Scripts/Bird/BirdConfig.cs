using UnityEngine;

[CreateAssetMenu(fileName = "BirdConfig", menuName = "ScriptableObject/Bird Config", order = 1)]
public class BirdConfig : ScriptableObject
{
    [Header("Type of bird")]
    public BirdType TypeOfBird;

    [Header("General settings")]
    public float SpecialShootForce = 2f;
    public float GravityScale = 1f;
    public float FlightRotationThreshold = 0.1f;
    public float AdditionalForce = 1.5f;

    [Header("Audio")]
    public AudioClip[] FlyingClips;
    public AudioClip[] CollisionClips;
    public AudioClip SpecialAbilityClip;
}

