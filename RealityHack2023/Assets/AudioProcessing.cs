using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioProcessing : MonoBehaviour
{

    public List<AudioClip> clips = new List<AudioClip>();
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClip(int num)
    {
        source.PlayOneShot(clips[num]);
    }
}
