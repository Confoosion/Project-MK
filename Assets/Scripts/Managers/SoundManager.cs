using UnityEngine;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Singleton;

    void Awake()
    {
        if(Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // public AudioMixer mainAudioMixer;
    public AudioSource masterAudioSource;
    public AudioSource playerAudioSource;
    public AudioSource attackAudioSource;
    public AudioSource UIAudioSource;

    public void PlayCharacterAttackAudio(CharacterSO character)
    {
        if(character.attackSFX != null)
        {
            PlayAudio(playerAudioSource, character.attackSFX, character.attackVolume);      
        }
    }

    public void PlayPlayerAudio(AudioClip audio, float volume = 1f)
    {
        PlayAudio(playerAudioSource, audio, volume);
    }

    public void PlayAttackAudio(AudioClip audio, float volume = 1f)
    {
        PlayAudio(attackAudioSource, audio, volume);
    }

    public void PlayUIAudio(AudioClip audio, float volume = 1f)
    {
        PlayAudio(UIAudioSource, audio, volume);
    }

    private void PlayAudio(AudioSource source, AudioClip audio, float volume)
    {
        if(audio != null)
            source.PlayOneShot(audio, volume);
    }
}
