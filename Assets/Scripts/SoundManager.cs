using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource soundEffects;

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
    }

    public void PlaySound(AudioClip clip)
    {
        PlaySound(clip, Vector2.zero);
    }

    public void PlaySound(AudioClip clip, Vector2 position)
    {
        //AudioSource.PlayClipAtPoint(clip, position);

        //var sound = new GameObject("Sound");
        //sound.transform.position = position;
        //var audioSource = sound.AddComponent<AudioSource>();
        //audioSource.clip = clip;
        //audioSource.Play();
        //StartCoroutine(DestroyAfterTime(sound, clip.length));

        soundEffects.PlayOneShot(clip);
    }

    private IEnumerator DestroyAfterTime(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
