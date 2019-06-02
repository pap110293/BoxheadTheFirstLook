using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnemy : MonoBehaviour
{
    public AudioClip idleSound;
    public AudioSource audioSource;
    Coroutine coroutine = null;
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        if (coroutine != null) StopCoroutine(coroutine);
        //coroutine = StartCoroutine(SoundPlay(10.0f));
    }
    IEnumerator SoundPlay(float _delayTime)
    {
        while (true)
        {
            if (audioSource && idleSound)
            {
                audioSource.clip = idleSound;
                audioSource.loop = false;
                audioSource.Play();
            }
            yield return new WaitForSecondsRealtime(_delayTime);
        }
    }
    private void OnDestroy()
    {
        if (coroutine != null) StopCoroutine(coroutine);
    }
}
