using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts;

public class House : MonoBehaviour {

    private Game game;
    public List<Question> Questions;
	private GameObject dialogBox;
	private TextMeshProUGUI questText;
    public Sprite Lable;
    public Sprite HighLight;
    public Vector3 LablePosition;
    public Vector3 HighLightPosition;
    public Vector3 LableScale = new Vector3(0.5F, 0.5F);
    public string Name;
    public CurrentHouse CurrentHouse;

    public AudioSource EnterSound;
    public AudioSource Ambient;

    public float HighlightStep = 0.05F;

    private bool _isMouseOver;
    private GameObject _lable;
    private GameObject _highlight;
    private Transform _transform;
    private ClockController _clock;
    private SkyController _skyController;
    private DateTime _lastClick;
    private GameObject _questionPanel;
    private NewspaperController _newspaperController;

    // Use this for initialization

    void Awake()
    {
        game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        _questionPanel = GameObject.FindGameObjectWithTag("Question");
    }

    void Start () {
        DialogBoxHolder dialogBoxHolder = transform.parent.gameObject.GetComponent<DialogBoxHolder>();
        dialogBox = dialogBoxHolder.dialogBox;
        questText = dialogBoxHolder.questText;
        questText.text = "";
        _questionPanel.SetActive(false);
        _isMouseOver = false;
        _lable = null;
        _transform = this.gameObject.GetComponent<Transform>();
        Questions = game.GetQuestions(Name);
        _clock = GameObject.FindGameObjectWithTag("Clock").GetComponent<ClockController>();
        _skyController = GameObject.FindGameObjectWithTag("Sky").GetComponent<SkyController>();
        _newspaperController = GameObject.FindGameObjectWithTag("Newspaper").GetComponent<NewspaperController>();
        _lastClick = DateTime.Now;
    }

	// Update is called once per frame
	void Update () {
    }

    private void OnMouseEnter()
    {
        _isMouseOver = true;
        if(EnterSound!=null && game.clickHouseEnabled)  EnterSound.Play();
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

    private void AddQuestionLable()
    {
        var lable = GameObject.FindGameObjectWithTag("QuestionLable");
        var image = lable.GetComponent<Image>();
        image.preserveAspect = true;
        image.sprite = Lable;
    }

    private void AddHighlight()
    {
        _highlight = new GameObject();
        _highlight.AddComponent<SpriteRenderer>();
        _highlight.transform.position = _transform.position;
        if (HighLightPosition != new Vector3(0,0)) _highlight.transform.position = HighLightPosition;
        var a = _highlight.GetComponent<SpriteRenderer>();
        a.sprite = HighLight;
        a.sortingLayerName = "Hihlight";
    }


    void Highlight()
    {
        var a = _highlight.GetComponent<SpriteRenderer>();
        var b = a.color;
        if (_isMouseOver && b.a < 0.99F) b.a += HighlightStep;
        else if (!_isMouseOver && b.a > 0.01F) b.a -= HighlightStep;
    }


    void UnderlineText(TextMeshProUGUI text)
    {
        text.fontStyle = FontStyles.Underline;
    }

    void OnMouseUp()
	{
        if (!game.clickHouseEnabled)
        {
            return;
        }
        game.SetCurrentHouse(this);
        // game.clickHouseEnabled = false;
        if (Questions.Count > 0 && !_questionPanel.activeSelf)
        {
            var question = Questions[UnityEngine.Random.Range(0,Questions.Count)];
            dialogBox.SetActive(true);
            _questionPanel.SetActive(true);
            AddQuestionLable();
            questText.text = question.Name;
            Button[] buttons = dialogBox.GetComponentsInChildren<Button>(true);
            Debug.Log("nuppe: " + buttons.Length);
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i >= question.Answers.Count)
                {
                	break;
                }
                Button b = buttons[i];
                var answer = question.Answers[i];
                var tmp = b.GetComponentInChildren<TextMeshProUGUI>();
                tmp.text = answer.Name;
                b.GetComponentInChildren<TextMeshProUGUI>().text = answer.Name; // nuppu tekst
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(delegate { HandleOptionSelected(answer); });
                b.gameObject.SetActive(true);
            }
            Questions.Remove(question);
        }
	}

    void HandleOptionSelected(Answer selectedOption)
    {
        if (DateTime.Now - _lastClick > new TimeSpan(0, 0, 0, 0, 500))
        {
            Debug.Log("Selected option " + selectedOption.Name);
            Debug.Log("Closer to doom by " + selectedOption.Doom + "Current doom:" + _clock.GetValue());
            dialogBox.SetActive(false);
            _questionPanel.SetActive(false);
            _clock.SetValueOffset(selectedOption.Doom);
            _skyController.NextDay(_clock.GetRelativeValue());
            _newspaperController.OpenDelayed(Game.instance.newspaperOpenDelay, (selectedOption.News != null ? selectedOption.News : "No news is good news"));
            _lastClick = DateTime.Now;
        }
    }
}
