using Qualcomm.Snapdragon.Spaces;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.ParticleSystem;

public class EnergyChanneling : MonoBehaviour
{

    public GameObject palmTriggerPrefab;
    public GameObject palmTriggerPrefabRight;


    public GameObject leftHandTriggerZone;
    public GameObject rightHandTriggerZone;


    public TMPro.TextMeshProUGUI rayHitText;
    public TMPro.TextMeshProUGUI neuroDataText;
    public TMPro.TextMeshProUGUI distanceFromLeftHandToHeadText;


    public Transform headLocation;

    public Transform particleTarget;

    public UnityEvent energyReleased = new UnityEvent();

    public Coroutine gazeCheck;
    public Coroutine accumulateEnergy;

    public bool isAccumulatingLeft;
    public bool isAccumulatingRight;


    public GameObject LeftHandParticleSystemPrefab;
    public GameObject RightHandParticleSystemPrefab;


    private Dictionary<string, string> neuroFields = new Dictionary<string, string>();

    List<GameObject> particlesSystems = new List<GameObject>();
    StringBuilder arrayBuilder = new StringBuilder();

    //FROM ARCTOP CODE, just how they handle updates from the data stream
    public void OnArrayValueChanged(string key, float[] value)
    {
        // here we store the value into our dictionary
        arrayBuilder.Clear();
        foreach (var kvp in value)
        {
            // add each key value pair as a line to the string builder
            arrayBuilder.Append($"{kvp},");
        }
        // update the UI text value with the value of the new string builder
        arrayBuilder.Length--;
        neuroFields[key] = arrayBuilder.ToString();
        //UpdateUI();
    }

    public void OnNeuroValueChanged(string key, float value)
    {
        // here we store the value into our dictionary
        neuroFields[key] = value.ToString();
    }
    //END FROMARCTOP

    // Start is called before the first frame update
    void Start()
    {
        
        //palms that you look at to trigger behavior
        leftHandTriggerZone = Instantiate(palmTriggerPrefab, this.transform.position, this.transform.rotation);
        rightHandTriggerZone = Instantiate(palmTriggerPrefabRight, this.transform.position + Vector3.up*3, this.transform.rotation);


        

        FindObjectOfType<SpacesHandManager>().handsChanged += HandleHandMovement;


        //StartCoroutine(StartScript()); 



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //SET ZONE

    void HandleHandMovement(SpacesHandsChangedEventArgs args)
    {
        leftHandTriggerZone.transform.position = FindObjectOfType<SpacesHandManager>().LeftHand.Joints[0].Pose.position;
        leftHandTriggerZone.transform.rotation = FindObjectOfType<SpacesHandManager>().LeftHand.transform.rotation;
        leftHandTriggerZone.transform.Rotate(new Vector3(90, 0, 0));


        rightHandTriggerZone.transform.position = FindObjectOfType<SpacesHandManager>().LeftHand.Joints[0].Pose.position;
        rightHandTriggerZone.transform.rotation = FindObjectOfType<SpacesHandManager>().LeftHand.transform.rotation;
        rightHandTriggerZone.transform.Rotate(new Vector3(-90, 0, 0));

    }




    public IEnumerator CheckForGaze(string handType)
    {
        bool found = false;
        
        while(!found){

            yield return new WaitForSeconds(0.1f);
            //perform raycast

            Ray ray;
            RaycastHit hit;

            ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));

            if(Physics.Raycast(ray, out hit, 100))
            {


                if(hit.collider.gameObject.tag ==  handType)
                {
                    rayHitText.text = "HIT " + handType;

                    if(handType == "LeftHandRaycast")
                    {
                        rayHitText.text = "Accumulating";
                        isAccumulatingLeft = true;
                        accumulateEnergy = StartCoroutine(AccumulateParticles("enjoyment"));
                    }
                    if (handType == "RightHandRaycast")
                    {
                        isAccumulatingRight = true;
                        accumulateEnergy = StartCoroutine(AccumulateParticles("focus"));
                    }
                    found = true;
                }
            }
            else
            {
               // rayHitText.text = "NOT HIT";
            }
        }

    }

    StringBuilder builder = new StringBuilder();



    IEnumerator StartScript()
    {
        yield return new WaitForSeconds(3);

        rayHitText.text = "What is in your left hand???";
        neuroDataText.text = "What is in your left hand ??";


        gazeCheck = StartCoroutine(CheckForGaze("LeftHandRaycast"));

        //wait for release

        //sart right

    }

    IEnumerator AccumulateParticles(string key)
    {

        //(CheckForRelease());


        while (isAccumulatingLeft)
        {
            yield return new WaitForSeconds(0.25f);

            //display neural data
            builder.Clear();
            foreach (var kvp in neuroFields)
            {
                builder.AppendLine($"{kvp.Key} : {kvp.Value}");
            }

            neuroDataText.text = builder.ToString();

            //new particle system
            GameObject particles = Instantiate(LeftHandParticleSystemPrefab, Vector3.zero, leftHandTriggerZone.transform.rotation);
            
           

            if ((int) float.Parse(neuroFields[key]) != -1)
            {



                MainModule mainParticle = particles.GetComponentInChildren<ParticleSystem>().main;
                mainParticle.startColor = new Color(float.Parse(neuroFields["enjoyment"]) / 100.0f, 0.5f, 0.5f);


            }



            particles.GetComponent<PathController>().Initiate(leftHandTriggerZone.transform, particleTarget.transform);

            particlesSystems.Add(particles);


        }
    }


        
        public IEnumerator CheckForRelease()
        {
            
            bool isReleased = false;
            while (!isReleased)
            {

                float distanceThreshold = 0.5f;


                yield return new WaitForSeconds(0.1f);

                float distance = Vector3.Distance(leftHandTriggerZone.transform.position, headLocation.position);
                //distance between hand and headset
                distanceFromLeftHandToHeadText.text = "Distance from hand to head: " + distance;

                if (distance > distanceThreshold)
                {
                    StopCoroutine(accumulateEnergy);
                    isAccumulatingLeft = false;

                    foreach(GameObject p in particlesSystems)
                    {
                        Destroy(p);
                    }


                    headLocation.Translate(Vector3.up * 0.5f);
                    energyReleased?.Invoke();
                }
            }
        }




    }
