using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager singleton;
    private AudioSource _audioSource;
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
        
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic() {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic() {
        _audioSource.Stop();
    }
    
    public void AddVolume()
    {
        _audioSource.volume += 0.10f;
    }
    public void LowerVolume()
    {
        _audioSource.volume -= 0.10f;
    }
}