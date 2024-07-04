using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;    
    public AudioClip clip;  
}

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;
    #region singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion singleton

    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourgeBGM;

    public string[] playSoundName;

    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    private void Start()
    {
        playSoundName = new string[audioSourceEffects.Length];
    }

    public void PlaySE(string name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying)
                    {
                        playSoundName[j] = effectSounds[i].name;
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 소리가 재생중.");
                return;
            }
        }
        Debug.Log(name + "이 SoundManager에 없.");
    }

    public void StopAllSE()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundName[i] == name)
            {
                audioSourceEffects[i].Stop();
                return;
            }
        }
        Debug.Log($"재생중인 {name}사운드가 없습니다.");
    } 
}