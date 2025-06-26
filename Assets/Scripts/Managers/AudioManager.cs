using UnityEngine;
using UnityEngine.UI;

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
    
    private string KEY_MUSIC_MUTED = "MusicMuted";
    private string KEY_SFX_MUTED = "SfxMuted";

    public string MusicKey => KEY_MUSIC_MUTED;
    public string SFXKey => KEY_SFX_MUTED;

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
        if (PlayerPrefs.HasKey(KEY_MUSIC_MUTED) && PlayerPrefs.HasKey(KEY_SFX_MUTED))
        {
            bool musicMuted = PlayerPrefs.GetInt(KEY_MUSIC_MUTED) == 1;
            bool sfxMuted = PlayerPrefs.GetInt(KEY_SFX_MUTED) == 1;
            ApplyMute(musicMuted, sfxMuted);
        }
        
        if (_defaultMusicClip != null)
            PlayMusic(_defaultMusicClip, loop: true);
    }

    public void ApplyMute(bool muteMusic, bool muteSfx)
    {
        SetMusicMute(muteMusic);
        SetSFXMute(muteSfx);

        PlayerPrefs.SetInt(KEY_MUSIC_MUTED, muteMusic ? 1 : 0);
        PlayerPrefs.SetInt(KEY_SFX_MUTED, muteSfx ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || _sfxSource == null || _sfxSource.volume == 0f) return;
        _sfxSource.PlayOneShot(clip);
    }
    public void PlayRandomSFX(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return;
        PlaySFX(clips[Random.Range(0, clips.Length)]);
    }
    public void PlayMusic(AudioClip musicClip, bool loop = true)
    {
        if (musicClip == null || _musicSource == null || _musicSource.volume == 0f) return;
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
        if (_sfxSource != null && _sfxSource.volume > 0f)
            _sfxSource.volume = _sfxVolume;
    }
    public void SetMusicVolume(float volume)
    {
        _musicVolume = Mathf.Clamp01(volume);
        if (_musicSource != null && _musicSource.volume > 0f)
            _musicSource.volume = _musicVolume;
    }

    public void SetSFXMute(bool mute)
    {
        if (_sfxSource != null)
            _sfxSource.volume = mute ? 0f : _sfxVolume;
    }
    public void SetMusicMute(bool mute)
    {
        if (_musicSource != null)
            _musicSource.volume = mute ? 0f : _musicVolume;
    }

    public float GetSFXVolume() => _sfxVolume;
    public float GetMusicVolume() => _musicVolume;
}
