using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleManager : MonoBehaviour
{

    public Transform endPosition;

    public static ParticleManager Instance;

    private List<ParticleSystem> allParticles;

    public Color JoyColor;
    public Color FocusColor;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            allParticles = new List<ParticleSystem>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetEndPosition(Transform newEndPosition)
    {
        endPosition = newEndPosition;
    }

    public void AddNewParticleSystem(GameObject toAdd)
    {
        allParticles.Add(toAdd.transform.Find("ParticleSystem").GetComponent<ParticleSystem>());
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = allParticles.Count - 1; i >= 0; i--)
        {
            if (allParticles[i].transform.position == endPosition.position)
            {
                Destroy(allParticles[i].transform.parent.gameObject, 1f);
                allParticles.RemoveAt(i);
                
            }
        }
    }
}

