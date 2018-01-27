using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class House : MonoBehaviour {
    public Quest[] quests;
    public GameObject dialogBox;
    public TextMeshProUGUI questText;
    public bool IsMouseOver;
    public float HighlightStep;

    private float _highLightOpacity;

    // Use this for initialization
    void Start () {
        IsMouseOver = false;
        questText.text = "";
        _highLightOpacity = 0;
<<<<<<< HEAD
        GetData();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
=======
        //GetData();
    }
>>>>>>> 6d8c3c9ee5b438a35376cd83860bc830339f1995

    // Update is called once per frame
    void Update () {

    }

    void Highlight()
    {
        if (IsMouseOver && _highLightOpacity < 1) _highLightOpacity += HighlightStep;
        else if(!IsMouseOver && _highLightOpacity>0) _highLightOpacity -= HighlightStep;
    }

    void GetData()
    {
        var path = Application.dataPath + "/data2.xml";
        Debug.Log(path);
        if (File.Exists(path))
        {
            try
            {
                //DataContractSerializer serializer = new DataContractSerializer(typeof(List<Assets.Models.House>), null,
                //   0x7FFF /*maxItemsInObjectGraph*/,
                //   false /*ignoreExtensionDataObject*/,
                //   true /*preserveObjectReferences : this is where the magic happens */,
                //   null /*dataContractSurrogate*/);

                //using (FileStream fs = File.Open(path, FileMode.Open))
                //{
                //    var a = (List<Assets.Models.House>)serializer.ReadObject(fs);
                //    Debug.Log(a.Count);
                //    a.ForEach(house =>
                //    {
                //        Debug.Log(house.Name);
                //    });
                //}
                var a = new List<Assets.Models.House>();
                var xs = new XmlSerializer(typeof(List<Assets.Models.House>));
                using (var sr = new StreamReader(path))
                {
                    a = (List<Assets.Models.House>)xs.Deserialize(sr);
                }
                Debug.Log(a.Count);
                a.ForEach(house =>
                {
                    Debug.Log(house.Name);
                });
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }

    void OnMouseUp()
<<<<<<< HEAD
	{
		dialogBox.SetActive(true);
	
		Quest quest = quests[0];
		questText.text = quest.questText;
		Button[] buttons = dialogBox.GetComponentsInChildren<Button>(true);
		Debug.Log("nuppe: " + buttons.Length);
		for (int i = 0; i < buttons.Length; i++)
		{
			if (i >= quest.solutions.Length)
			{
				break;
			}
			Button b = buttons[i];
			QuestSolution solution = quest.solutions[i];
			b.GetComponentInChildren<Text>().text = solution.action;
			b.gameObject.SetActive(true);
		}
	}
=======
    {
        Quest quest = quests[0];
        Debug.Log("Quest text: " + quest.questText);

        questText.text = quest.questText;
        Debug.Log("Quest text field: " + questText.text);

        Button[] buttons = dialogBox.GetComponentsInChildren<Button>(true);
        Debug.Log("Buttons: " + buttons.Length);

        for (int i = 0; i < buttons.Length; i++)
        {
            Debug.Log("Populating button: " + i);
            if (i >= quest.solutions.Length)
            {
                Debug.Log("Too many solutions.");
                break;
            }

            Button button = buttons[i];
            QuestSolution solution = quest.solutions[i];
            Debug.Log("Solution action: " + solution.action);
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = solution.action;
            button.gameObject.SetActive(true);
        }

        dialogBox.SetActive(true);
    }
>>>>>>> 6d8c3c9ee5b438a35376cd83860bc830339f1995
}

[System.Serializable]
public struct Quest
{
    public string questText;
    public QuestSolution[] solutions;


}

[System.Serializable]
public struct QuestSolution
{
    public string action;
    public float closerToDoom;
}
