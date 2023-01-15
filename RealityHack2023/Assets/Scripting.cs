using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripting : MonoBehaviour
{
    public EnergyChanneling energyChanneling;

    public TMPro.TextMeshProUGUI dialogue;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartScript())
;    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator StartScript()
    {
        yield return new WaitForSeconds(1);

        EnergyChanneling energyChannelingModule = energyChanneling.GetComponent<EnergyChanneling>();

        dialogue.text = "What is in your left hand?";

        yield return StartCoroutine(energyChannelingModule.CheckForGaze("LeftHandRaycast"));

        dialogue.text = "What is in your right hand?";

        yield return StartCoroutine(energyChannelingModule.CheckForGaze("RightHandRaycast"));

        dialogue.text = "Inspirational stuff";

        yield return new WaitForSeconds(5);

        dialogue.text = "Pull in and push out for release. ";

        yield return StartCoroutine(energyChannelingModule.CheckForRelease());



    }
}
