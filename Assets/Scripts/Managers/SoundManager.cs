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

    private void PlayAudio(AudioSource source, AudioClip audio, float volume)
    {
        source.PlayOneShot(audio, volume);
    }
}
