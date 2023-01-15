using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject prefabRight, prefabLeft;
    public Transform LeftStart, RightStart, EndLocation;

    // Start is called before the first frame update
    void Start()
    {
        //createParticle();
        StartCoroutine(SpawnParticles());
    }

    void createParticle()
    {
        GameObject particlePath = Instantiate(prefabLeft, Vector3.zero, Quaternion.identity);
        particlePath.GetComponent<PathController>().Initiate(LeftStart, EndLocation);

        GameObject particlePath2 = Instantiate(prefabRight, Vector3.zero, Quaternion.identity);
        particlePath2.GetComponent<PathController>().Initiate(RightStart, EndLocation);
    }

    IEnumerator SpawnParticles()
    {
        while (true)
        {
            createParticle();
            yield return new WaitForSeconds(1f);
        }
        
    }

}
