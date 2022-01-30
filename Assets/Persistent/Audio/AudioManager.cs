using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

    public static AudioManager singleton;
    private AudioSource _audioSource;

    [SerializeField]
    public AudioClip menuClip;
    public AudioClip tutorialClip;
    public AudioClip level1Clip;
    public AudioClip boss1Clip;
    public AudioClip level2Clip;
    public AudioClip boss2Clip;
    public AudioClip level3Clip;
    public AudioClip boss3Clip;
    public AudioClip level4Clip;
    public AudioClip boss4Clip;

    private void Awake() {

        if (singleton != null) {
            Destroy(gameObject);
        }
        else {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }


    void Update() {
        int y = SceneManager.GetActiveScene().buildIndex;

        switch (y) {
            case 0:     //menu
                _audioSource.clip = menuClip;
                PlayMusic();
                break;
            case 1:     //tutorial
                _audioSource.clip = tutorialClip;
                PlayMusic();
                break;
            case 2:     //level1
                _audioSource.clip = level1Clip;
                PlayMusic();
                break;
            case 3:     //crane
                _audioSource.clip = boss1Clip;
                PlayMusic();
                break;
            case 4:
                _audioSource.clip = level2Clip;
                PlayMusic();
                break;
            case 5:
                _audioSource.clip = boss2Clip;
                PlayMusic();
                break;
            case 6:
                _audioSource.clip = level3Clip;
                PlayMusic();
                break;
            case 7:
                _audioSource.clip = boss3Clip;
                PlayMusic();
                break;
            case 8:
                _audioSource.clip = level4Clip;
                PlayMusic();
                break;
            case 9:
                _audioSource.clip = boss4Clip;
                PlayMusic();
                break;
            default:
                _audioSource.clip = menuClip;
                PlayMusic();
                break;
        }
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