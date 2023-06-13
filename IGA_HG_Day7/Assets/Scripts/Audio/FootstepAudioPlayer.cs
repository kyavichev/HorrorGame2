using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepAudioPlayer : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    public float timer { protected set; get; }
    public bool isPlayingFX { protected set; get; } = false;

    public float speedModifier = 1;

    public PlayerController playerController;


    // Update is called once per frame
    void Update()
    {
        if (playerController != null && Mathf.Abs(playerController.GetCurrentSpeed()) > 0)
        {
            if(!isPlayingFX == false)
            {
                timer = 0;
                PlayRandomAudio();
                isPlayingFX = true;

            }
            else
            {
                timer += Time.deltaTime * speedModifier;
                if (timer >= 0.75f)
                {
                    timer = 0; //timer % 0.75f; // module operator
                    PlayRandomAudio();
                }
            }
        }
        else
        {
            isPlayingFX = false;
        }
    }


    protected void PlayRandomAudio()
    {
        var audio = GetRandomClip();
        audioSource.PlayOneShot(audio);
    }


    protected AudioClip GetRandomClip()
    {
        return audioClips[Random.Range(0, audioClips.Length)];
    }
}

