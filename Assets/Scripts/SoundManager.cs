using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource soundEffects;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        soundEffects = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        //var sound = new GameObject("Sound");
        //sound.transform.position = position;
        //var audioSource = sound.AddComponent<AudioSource>();
        //audioSource.clip = clip;
        //audioSource.Play();
        //StartCoroutine(DestroyAfterTime(sound, clip.length));

        soundEffects.PlayOneShot(clip);
    }

    //private IEnumerator DestroyAfterTime(GameObject gameObject, float time)
    //{
    //    yield return new WaitForSeconds(time);

    //    Destroy(gameObject);
    //}
}
