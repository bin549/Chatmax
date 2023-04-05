using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip clipSound;
    [SerializeField] private AudioClip slipSound;
    [SerializeField] private AudioClip quitSound;
    [SerializeField] private AudioClip tapSound;
    [SerializeField] private AudioClip homeSound;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip selectedSound;
    private static AudioManager instance;

    private void Awake()
    {        
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayTapSound()
    {
        audioSource.clip = tapSound;
        audioSource.Play();
    }

    public void PlayHomeSound()
    {
        audioSource.clip = homeSound;
        audioSource.Play();
    }

    public void PlayClickSound()
    {
        audioSource.clip = clipSound;
        audioSource.Play();
    }

    public void PlaySlipSound()
    {
        audioSource.clip = slipSound;
        audioSource.Play();
    }

    public void PlayQuitSound()
    {
        audioSource.clip = quitSound;
        audioSource.Play();
    }

    public void PlayChangeSound()
    {
        audioSource.clip = changeSound;
        audioSource.Play();
    }

    public void PlaySelectedSound()
    {
        audioSource.clip = selectedSound;
        audioSource.Play();
    }
}
