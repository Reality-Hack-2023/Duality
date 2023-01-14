using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using Newtonsoft;

public class DataPersistence : MonoBehaviour
{

    public string filePath = "contributions.txt";

    public ContributionData data;


    [Serializable]
    public class ContributionData
    {

        public List<Vector3> contributions  = new List<Vector3>();

    }





    // Start is called before the first frame update
    void Start()
    {
        data = new ContributionData();
        

        data.contributions.Add(new Vector3(1, 2, 3));
        data.contributions.Add(new Vector3(4, 5, 6));
        WriteContributions();
        //test out create
        
        LoadContributions();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadContributions()
    {

        string path = Application.persistentDataPath + "/" + filePath;
       // Debug.Log(path);
        //write to file


        if (File.Exists(path))
        {
            using(StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                data = JsonUtility.FromJson<ContributionData>(json);
            }


        }
    }

    public void WriteContributions()
    {

        //data structure to JSON


        string path = Application.persistentDataPath + "/" + filePath;

        Debug.Log(path);

        FileStream fileStream = new FileStream(path, FileMode.Create);

        string json = JsonUtility.ToJson(data);


        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }


    public void AddContribution(Vector3 contribution)
    {
        data.contributions.Add(contribution);
        WriteContributions();
    }
    

   

}
