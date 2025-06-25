using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;

    [Header("Default Music Clip")]
    [SerializeField] private AudioClip _defaultMusicClip;

    [Header("Volume Settings (0–1)")]
    [Range(0f, 1f)][SerializeField] private float _sfxVolume = 1f;
    [Range(0f, 1f)][SerializeField] private float _musicVolume = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (_sfxSource != null) _sfxSource.volume = _sfxVolume;
        if (_musicSource != null) _musicSource.volume = _musicVolume;
    }

    private void Start()
    {
        if (_defaultMusicClip != null)
            PlayMusic(_defaultMusicClip, loop: true);
    }
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || _sfxSource == null) return;
        _sfxSource.PlayOneShot(clip);
    }
    public void PlayRandomSFX(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return;
        var clip = clips[Random.Range(0, clips.Length)];
        PlaySFX(clip);
    }
    public void PlayMusic(AudioClip musicClip, bool loop = true)
    {
        if (musicClip == null || _musicSource == null) return;
        _musicSource.clip = musicClip;
        _musicSource.loop = loop;
        _musicSource.Play();
    }
    public void StopMusic()
    {
        if (_musicSource != null && _musicSource.isPlaying)
            _musicSource.Stop();
    }
    public void SetSFXVolume(float volume)
    {
        _sfxVolume = Mathf.Clamp01(volume);
        if (_sfxSource != null)
            _sfxSource.volume = _sfxVolume;
    }
    public void SetMusicVolume(float volume)
    {
        _musicVolume = Mathf.Clamp01(volume);
        if (_musicSource != null)
            _musicSource.volume = _musicVolume;
    }
    public float GetSFXVolume() => _sfxVolume;
    public float GetMusicVolume() => _musicVolume;
}
