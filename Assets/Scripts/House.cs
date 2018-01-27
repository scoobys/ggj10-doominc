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
    public List<Question> Questions;
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
    private ClockController _clock;

    // Use this for initialization
    void Start () {
        dialogBox = GameObject.FindGameObjectWithTag("Question");
        _isMouseOver = false;
        _lable = null;
        Questions = new List<Question>() { new Question(),new Question(), new Question()};
        _transform = this.gameObject.GetComponent<Transform>();
        GetData();
        _clock = GameObject.FindGameObjectWithTag("Clock").GetComponent<ClockController>();

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
        var question = Questions[0];
		dialogBox.SetActive(true);
		questText.text = "Your Action:"; // Kysimus
		Button[] buttons = dialogBox.GetComponentsInChildren<Button>(true);
		Debug.Log("nuppe: " + buttons.Length);
		for (int i = 0; i < buttons.Length; i++)
		{
			//if (i >= question.Answers.Count-1)
			//{
			//	break;
			//}
			Button b = buttons[i];
			var answer = "placeholder";
			b.GetComponentInChildren<TextMeshProUGUI>().text = answer; // nuppu tekst
            b.onClick.AddListener(delegate{HandleOptionSelected(new Answer());});
			b.gameObject.SetActive(true);
		}
	}

    void HandleOptionSelected(Answer selectedOption)
    {
        var random = new System.Random();
        //Debug.Log("Selected option " + selectedOption.action);
        //Debug.Log("Closer to doom by " + selectedOption.closerToDoom);
        dialogBox.SetActive(false);
        Debug.Log(_clock.GetValue());
        _clock.SetValueOffset(0.5F);
        Debug.Log(_clock.GetValue());
    }
}