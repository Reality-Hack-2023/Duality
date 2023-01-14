using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject prefab;
    public Transform StartLocation, EndLocation;

    // Start is called before the first frame update
    void Start()
    {
        GameObject particlePath = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        particlePath.GetComponent<PathController>().Initiate(StartLocation, EndLocation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
