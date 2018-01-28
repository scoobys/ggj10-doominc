using System.Collections;
using Assets.Models;
using System.Collections.Generic;
using System.IO;
using System;
using System.Xml.Serialization;
using UnityEngine;
using Assets.Scripts;

public class Game : MonoBehaviour {

    public static Game instance;

    public string villageName = "Doom";
    public float newspaperOpenDelay = 3.0f;
    public float transmissionOpenDuration = 5.0f;
    public bool clickHouseEnabled;

	private TextAsset questData;
    private Dictionary<string, List<Question>> houseToQuestions;

    private CurrentHouse currentHouse;

    void Awake () {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        houseToQuestions = new Dictionary<string, List<Question>>();
        InitQuestData();
        clickHouseEnabled = true;
        currentHouse = CurrentHouse.None;
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

    public void SetCurrentHouse(House house)
    {
        currentHouse = house.CurrentHouse;
        
        if (currentHouse == CurrentHouse.None)
        {
            clickHouseEnabled = true;
        }
        else
        {
            clickHouseEnabled = false;
        }
    }

    public void ClearCurrentHouse()
    {
        currentHouse = CurrentHouse.None;
        clickHouseEnabled = true;
    }

    public bool isAnyHouseSelected()
    {
        return currentHouse != CurrentHouse.None;
    }
}
