using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class House : MonoBehaviour {
// päris
	public Quest[] quests;
	private GameObject dialogBox;
	private TextMeshProUGUI questText;
    public Sprite Lable;
    public Sprite HighLight;
    public Vector3 LablePosition;
    public Vector3 HighLightPosition;
    public Vector3 LableScale = new Vector3(0.5F, 0.5F);

    public AudioSource EnterSound;

    public float HighlightStep = 0.05F;

    private bool _isMouseOver;
    private GameObject _lable;
    private GameObject _highlight;
    private Transform _transform;



    // Use this for initialization
    void Start () {
        DialogBoxHolder dialogBoxHolder = transform.parent.gameObject.GetComponent<DialogBoxHolder>();
        dialogBox = dialogBoxHolder.dialogBox;
        questText = dialogBoxHolder.questText;
        questText.text = "";
        _isMouseOver = false;
        _lable = null;
        _transform = this.gameObject.GetComponent<Transform>();
        GetData();
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void OnMouseEnter()
    {
        _isMouseOver = true;
        if(EnterSound!=null)  EnterSound.Play();
            Addlable();
        AddHighlight();
    }

    private void OnMouseExit()
    {
        _isMouseOver = false;
        Destroy(_lable);
        Destroy(_highlight);
        if (EnterSound != null) EnterSound.Stop();
    }

    private void Addlable()
    {
        _lable = new GameObject();
        _lable.AddComponent<SpriteRenderer>();
        _lable.transform.localScale = LableScale;
        _lable.transform.position = LablePosition;
        var a = _lable.GetComponent<SpriteRenderer>();
        a.sprite = Lable;
        a.sortingLayerName = "Lable";
    }

    private void AddHighlight()
    {
        _highlight = new GameObject();
        _highlight.AddComponent<SpriteRenderer>();
        _highlight.transform.position = _transform.position;
        if (HighLightPosition != new Vector3(0,0)) _highlight.transform.position = HighLightPosition;
        var a = _highlight.GetComponent<SpriteRenderer>();
        a.sprite = HighLight;
        a.sortingLayerName = "Highlight";
    }


    void Highlight()
    {
        var a = _highlight.GetComponent<SpriteRenderer>();
        var b = a.color;
        if (_isMouseOver && b.a < 0.99F) b.a += HighlightStep;
        else if (!_isMouseOver && b.a > 0.01F) b.a -= HighlightStep;
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
			b.GetComponentInChildren<TextMeshProUGUI>().text = solution.action;
            b.onClick.AddListener(delegate{HandleOptionSelected(solution);});
			b.gameObject.SetActive(true);
		}
	}

    void HandleOptionSelected(QuestSolution selectedOption)
    {
        Debug.Log("Selected option " + selectedOption.action);
        Debug.Log("Closer to doom by " + selectedOption.closerToDoom);
        dialogBox.SetActive(false);
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
