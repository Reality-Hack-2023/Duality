using Qualcomm.Snapdragon.Spaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetJointDataInfo : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;


    public SpacesHandManager _spacesHandManager;   // Start is called before the first frame update
    void Start()
    {
        //_spacesHandManager = FindObjectOfType<SpacesHandManager>();

        StartCoroutine(SampleJoints());
    }

    // Update is called once per frame
    void Update()
    {

        text.text = _spacesHandManager.LeftHand.Joints[0].Pose.position.x + "";

        
    }

    IEnumerator SampleJoints()
    {
        text.text = "Starting Joint Tracking";
        yield return new WaitForSeconds(2);
        while (true)
        {
            yield return new WaitForSeconds(1);
            text.text = FindObjectOfType<SpacesHandManager>().LeftHand.transform.position.x + "";
        }

    }
}
