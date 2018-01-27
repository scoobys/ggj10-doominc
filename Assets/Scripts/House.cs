﻿using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class House : MonoBehaviour {
// päris
	public Quest[] quests;
	public GameObject dialogBox;
	public Text questText;
    public Sprite Lable;
    public Vector3 LablePosition;

    public float HighlightStep;

    private bool _isMouseOver;
    private float _highLightOpacity;
    private GameObject _mask;
    private Transform _transform;


    
    // Use this for initialization
    void Start () {
        _isMouseOver = false;
		questText.text = "";
        _highLightOpacity = 0;
        _mask = null;
        _transform = this.gameObject.GetComponent<Transform>();
        GetData();
    }
	
	// Update is called once per frame
	void Update () {
        Highlight();
	}

    private void OnMouseEnter()
    {
        var trans = _transform;
        _isMouseOver = true;
        _mask = new GameObject();
        _mask.AddComponent<SpriteRenderer>();
        _mask.transform.localScale = new Vector3(0.5F, 0.5F);
        _mask.transform.position = LablePosition;
        var a = _mask.GetComponent<SpriteRenderer>();
        a.sprite = Lable;
        a.sortingLayerName = "New Layer";
    }

    private void OnMouseExit()
    {
        _isMouseOver = false;
        Destroy(_mask);
    }


    void Highlight()
    {
        if (_isMouseOver && _highLightOpacity < 1)
        {
            Debug.Log("highlighting");
            _highLightOpacity += HighlightStep;
        }
        else if (!_isMouseOver && _highLightOpacity > 0) _highLightOpacity -= HighlightStep;
        var sprite = this.gameObject.GetComponent<SpriteRenderer>();
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
