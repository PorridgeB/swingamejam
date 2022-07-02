using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour
{
    public AudioClip click;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        if (SoundManager.instance == null)
        {
            return;
        }

        button.onClick.AddListener(() => SoundManager.instance.PlaySound(click, 0.8f));
    }
}
