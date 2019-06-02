using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillSound : MonoBehaviour
{
    public AudioClip idleSound;
    public AudioClip attackSound;
    public AudioClip destroy;
    public AudioSource audioSource;
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        StartAttackSound();
    }
    public void StartAttackSound()
    {
        if (audioSource && attackSound)
        {
            audioSource.clip = attackSound;
            //audioSource.volume = 0.5f;
            audioSource.loop = false;
            audioSource.Play();
        }        
    }
    public void StartDestroySound()
    {
        if (audioSource && destroy)
        {
            audioSource.clip = attackSound;
            //audioSource.volume = 0.5f;
            audioSource.loop = false;
            audioSource.Play();
            var temp = new GameObject();// Instantiate(new GameObject(), new Vector3(0, 0, 0), Quaternion.identity);
            var tempSound = temp.AddComponent<AudioSource>();
            tempSound.clip = destroy;
            tempSound.Play();
            Destroy(temp, 1);
        }
    }
    public void StartIdleSound()
    {
        if (audioSource && idleSound)
        {
            audioSource.clip = idleSound;
            audioSource.loop = true;
            audioSource.Play();
        } 
    }
    private void Update()
    {
        if (audioSource && !audioSource.isPlaying) StartIdleSound();
    }
    public void OnDestroy()
    {
        StartDestroySound();
    }
}
