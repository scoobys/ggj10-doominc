using System.Collections;
using Assets.Models;
using System.Collections.Generic;
using System.IO;
using System;
using System.Xml.Serialization;
using UnityEngine;

public class Game : MonoBehaviour {

    public static Game instance;

    public string villageName = "Doom";

	private TextAsset questData;

    void Awake () {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public List<Question> GetQuestions(string houseName)
    {

        questData = Resources.Load("questData") as TextAsset;
        List<Question> Questions = new List<Question>();
        {
            try
            {
                var a = new List<Assets.Models.House>();
                var xs = new XmlSerializer(typeof(List<Assets.Models.House>));
                
                // using (var sr = new StreamReader(path))
                using (var sr = new StringReader(questData.text))
                {
                    a = (List<Assets.Models.House>)xs.Deserialize(sr);
                }
                a.ForEach(house =>
                {
                    if(house.Name == houseName)
                    {
                        Questions = house.Questions;
                    }
                });
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        return Questions;
    }
}
