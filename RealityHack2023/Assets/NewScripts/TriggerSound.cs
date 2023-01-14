using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{

    public AudioSource playSound; 

    void OntriggerEnter(Collider other)
    {
        playSound.Play();
    }

}
