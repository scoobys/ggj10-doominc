using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
public class House : MonoBehaviour {
// päris
	public Quest[] quests;
	public GameObject dialogBox;
	public Text questText;
    public bool IsMouseOver;
    public float HighlightStep;

    private float _highLightOpacity;


    
    // Use this for initialization
    void Start () {
        IsMouseOver = false;
		questText.text = "";
        _highLightOpacity = 0;
        //GetData();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void Highlight()
    {
        if (IsMouseOver && _highLightOpacity < 1) _highLightOpacity += HighlightStep;
        else if(!IsMouseOver && _highLightOpacity>0) _highLightOpacity -= HighlightStep;
    }
    //void GetData()
    //{
    //    var path = Application.dataPath + "/andmed2.xml";
    //    Debug.Log(path);
    //    if (File.Exists(path))
    //    {
    //        try
    //        {
    //            DataContractSerializer serializer = new DataContractSerializer(typeof(List<Assets.Models.House>), null,
    //               0x7FFF /*maxItemsInObjectGraph*/,
    //               false /*ignoreExtensionDataObject*/,
    //               true /*preserveObjectReferences : this is where the magic happens */,
    //               null /*dataContractSurrogate*/);

    //            using (FileStream fs = File.Open(path, FileMode.Open))
    //            {
    //                var a = (List<Assets.Models.House>)serializer.ReadObject(fs);
    //                a.ForEach(house => {
    //                    Debug.Log(house.Name);
    //                });
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            Debug.Log("Whoops");
    //        }
    //    }
    //}

	void OnMouseUp()
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
