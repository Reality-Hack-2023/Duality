using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(PathCreator))]
public class PathController : MonoBehaviour
{

    //Start and End locations for bezier curve
    //public Transform StartLocation, EndLocation;

    public int CurveSegments = 5;
    public float xVariance = 2f, yVariance = 2f;

    // Flipped if the left hand
    public bool isLeft = false;

    public ParticleSystem TravelingParticle;
    public Material ParticleMaterial;

    Vector3[] waypoints;

    // Start is called before the first frame update
    void Awake()
    {
        // Set material of particle
        if (ParticleMaterial)
        {
            SetParticleMaterial(ParticleMaterial);
        }

        waypoints = new Vector3[CurveSegments + 2];

        //Initiate(StartLocation, EndLocation);

    }

    private void GenerateCurve()
    {
        Vector3 StartLocation = waypoints[0];
        Vector3 EndLocation = waypoints[CurveSegments + 1];

        // Calc distance between start and end
        float xDist = EndLocation.x - StartLocation.x;
        float yDist = EndLocation.y - StartLocation.y;
        float zDist = EndLocation.z - StartLocation.z;
        //Debug.Log($"Distance between points {new Vector3(xDist, yDist, zDist)}");


        // Split distance by # segments
        float xSegmentDistance = xDist / CurveSegments;
        float ySegmentDistance = yDist / CurveSegments;
        float zSegmentDistance = zDist / CurveSegments;
        //Debug.Log($"Segments between points {new Vector3(xSegmentDistance, ySegmentDistance, zSegmentDistance)}");

        

        // Add variance into the curves with random position
        for (int i = 0; i < CurveSegments; i++)
        {
            //Determine xVariance for randomization
            float xPos = StartLocation.x + xSegmentDistance * i;
            float yPos = StartLocation.y + ySegmentDistance * i;
            //Debug.Log($"Starting xPos is {xPos}");

            //Even goes one direction, odd goes the other
            if ((i + 1) % 2 == 0)
            {
                if (!isLeft)
                {
                    xPos += Random.Range(0f, xVariance);
                    yPos += Random.Range(0f, yVariance);
                }
                else
                {
                    xPos -= Random.Range(0f, xVariance);
                    yPos -= Random.Range(0f, yVariance);
                }

            }
            else
            {
                if (isLeft)
                {
                    xPos += Random.Range(0.5f, xVariance);
                    yPos += Random.Range(0f, yVariance);
                }
                else
                {
                    xPos -= Random.Range(0.5f, xVariance);
                    yPos -= Random.Range(0f, yVariance);
                }
            }

            Vector3 newPos = new Vector3(xPos, yPos, StartLocation.z + zSegmentDistance * i);
            //Debug.Log($"New Position for segment: {newPos}");

            waypoints[i + 1] = newPos;
        }

        //waypoints[CurveSegments + 1] = EndLocation.position;


        //waypoints

        BezierPath bezierPath = new BezierPath(waypoints, false, PathSpace.xyz);

        PathCreator pathCreator = GetComponent<PathCreator>();
        pathCreator.bezierPath = bezierPath;
    }

    public void Initiate (Transform startPosition, Transform endPosition, Material mat = null)
    {
        Debug.Log("Setting up particle system");
        SetStartPosition(startPosition);
        SetEndPosition(endPosition);

        if (mat)
        {
            SetParticleMaterial(mat);
        }

        GenerateCurve();
    }

    public void SetParticleMaterial(Material material)
    {
        TravelingParticle.GetComponent<ParticleSystemRenderer>().material = material;
    }

    public void SetStartPosition(Transform newStartPos)
    {
        waypoints[0] = newStartPos.position;
        /*
        BezierPath bezierPath = new BezierPath(waypoints, false, PathSpace.xyz);

        PathCreator pathCreator = GetComponent<PathCreator>();
        pathCreator.bezierPath = bezierPath;
        */
    }


    public void SetEndPosition(Transform newEndPos)
    {
        waypoints[CurveSegments+1] = newEndPos.position;

        /*
        BezierPath bezierPath = new BezierPath(waypoints, false, PathSpace.xyz);

        PathCreator pathCreator = GetComponent<PathCreator>();
        pathCreator.bezierPath = bezierPath;
        */
    }

}
