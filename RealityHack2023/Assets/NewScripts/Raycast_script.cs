using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Raycast_script : MonoBehaviour
{
    public GameObject spawn_prefab;
    GameObject spawned_object;
    bool object_spawned;
    ARRaycastManager arrayman;
    ARPlaneManager arplneman;
    Vector2 First_touch;
    Vector2 second_touch;
    float distance_current;
    float distance_previous;
    bool first_pinch = true;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Start is called before the first frame update
    void Start()
    {
        object_spawned = false;
        arrayman = GetComponent<ARRaycastManager>();
        arplneman = GetComponent<ARPlaneManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && !object_spawned)
        {
            if (arrayman.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitpose = hits[0].pose;
                spawned_object = Instantiate(spawn_prefab, hitpose.position, hitpose.rotation);
                object_spawned = true;
                arrayman.enabled = false;
                arplneman.enabled = false;

            }
        }
        if (Input.touchCount > 1 && object_spawned)
        {
            First_touch = Input.GetTouch(0).position;
            second_touch = Input.GetTouch(1).position;
            distance_current = second_touch.magnitude - First_touch.magnitude;
            if (first_pinch)
            {
                distance_previous = distance_current;
                first_pinch = false;
            }
            if (distance_current != distance_previous)
            {
                Vector3 scale_value = spawned_object.transform.localScale * (distance_current / distance_previous);
                spawned_object.transform.localScale = scale_value;
                distance_previous = distance_current;

            }

        }
        else
        {
            first_pinch = true;
        }

    }
}