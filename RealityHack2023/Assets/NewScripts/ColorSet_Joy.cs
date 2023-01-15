using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ColorSet_Joy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem[] allParticleSystems = GetComponentsInChildren<ParticleSystem>();
        //Debug.Log(allParticleSystems.Length);

        foreach(ParticleSystem system in allParticleSystems)
        {
            
            MainModule mainParticle = system.GetComponentInChildren<ParticleSystem>().main;
            mainParticle.startColor = ParticleManager.Instance.JoyColor;
            Debug.Log($"Start Color changed to {ParticleManager.Instance.JoyColor}");
            //Debug.Log($"")
        }
    }
}
