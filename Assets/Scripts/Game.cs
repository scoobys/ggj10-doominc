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
    private Dictionary<string, List<Question>> houseToQuestions;

    void Awake () {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        houseToQuestions = new Dictionary<string, List<Question>>();
        InitQuestData();
    }

    private void InitQuestData()
    {
        questData = Resources.Load("questData") as TextAsset;
        {
            try
            {
                var a = new List<Assets.Models.House>();
                var xs = new XmlSerializer(typeof(List<Assets.Models.House>));
                using (var sr = new StringReader(questData.text))
                {
                    a = (List<Assets.Models.House>)xs.Deserialize(sr);
                }
                a.ForEach(house =>
                {
                    houseToQuestions[house.Name] = house.Questions;
                });
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }

    public List<Question> GetQuestions(string houseName)
    {
        List<Question> questions;
        if (houseToQuestions.TryGetValue(houseName, out questions))
        {
            return questions;
        }
        else
        {
            return new List<Question>();
        }
    }
}
