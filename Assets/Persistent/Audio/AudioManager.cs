using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager singleton;
    private int i = 0;
    
    
    
    private AudioSource[] _audioSource;
    private void Awake() {
        
        if (singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        
        _audioSource = GetComponents<AudioSource>();
    }

    public void PlayMusic() {
        if (_audioSource[i].isPlaying) return;
        _audioSource[i].Play();
    }

    public void StopMusic() {
        _audioSource[i].Stop();
    }
    
    public void AddVolume()
    {
        _audioSource[i].volume += 0.10f;
    }
    public void LowerVolume()
    {
        _audioSource[i].volume -= 0.10f;
    }
    public void FirstTrack()
    {
        i = 0;
    }
    public void SecondTrack()
    {
        i = 1;
    }
    
}