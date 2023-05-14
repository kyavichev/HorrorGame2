using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepAudioPlayer : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    public float timer { protected set; get; }

    public float speedModifier = 1;

    public IHasSpeed trackedMover;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime * _entity.DesiredSpeed * speedModifier;
        timer += Time.deltaTime * speedModifier;

        if (timer >= 1f)
        {
            timer = timer % 1f;

            var audio = GetRandomClip();
            audioSource.PlayOneShot(audio);
        }
    }


    protected AudioClip GetRandomClip()
    {
        return audioClips[Random.Range(0, audioClips.Length)];
    }
}
