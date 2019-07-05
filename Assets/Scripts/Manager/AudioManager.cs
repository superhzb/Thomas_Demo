using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    private AudioSource[] audiosourceArray; //0-BG Music, 1-SoundFX
    private bool canPlaySoundFX = true;
    private bool canPlayBGMusic = true;

    public AudioManager()
    {
        audiosourceArray = GameManager.Instance.GetComponents<AudioSource>();
    }

    public void PlayBGMusic(AudioClip clip)
    {
        if (!canPlayBGMusic) return; 

        if (!audiosourceArray[0].isPlaying || audiosourceArray[0].clip != clip)
        {
            audiosourceArray[0].clip = clip;
            audiosourceArray[0].Play();
        }
    }

    public void PlayBGMusic(string clipName)
    {
        AudioClip clip = GameManager.Instance.mAudioFactory.GetClip(clipName);

        if (!canPlayBGMusic) return;

        if (!audiosourceArray[0].isPlaying || audiosourceArray[0].clip != clip)
        {
            audiosourceArray[0].clip = clip;
            audiosourceArray[0].Play();
        }
    }

    public void PlaySoundFX(AudioClip clip)
    {
        if (!canPlaySoundFX) return;

        audiosourceArray[1].PlayOneShot(clip);
    }

    public void PlaySoundFX(string clipName)
    {
        AudioClip clip = GameManager.Instance.mAudioFactory.GetClip(clipName);

        if (!canPlaySoundFX) return;

        audiosourceArray[1].PlayOneShot(clip);
    }

    public void StopBGMusic()
    {
        audiosourceArray[0].Stop();
    }
    public void TurnOnBGMusic()
    {
        audiosourceArray[0].Play();
    }

    public void PlayButtonAudioClip()
    {
        PlaySoundFX(GameManager.Instance.mAudioFactory.GetClip("Button"));
    }

    public void PlayPageAudioClip()
    {
        if (!audiosourceArray[1].isPlaying)
        {
            audiosourceArray[1].PlayOneShot(GameManager.Instance.mAudioFactory.GetClip("Paging"));
        }
    }

    public void ToggleMusic()
    {
        canPlayBGMusic = !canPlayBGMusic;
        if (canPlaySoundFX)
        {
            TurnOnBGMusic();
        }
        else
        {
            StopBGMusic();
        }
    }

    public void ToggleSoundFX()
    {
        canPlaySoundFX = !canPlaySoundFX;
    }
}
