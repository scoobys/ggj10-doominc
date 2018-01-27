using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class House : MonoBehaviour {
// päris
	public Quest[] quests;
	public GameObject dialogBox;
	public Text questText;
	public Button template;

	// Use this for initialization
	void Start () {
		questText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseUp()
	{
		dialogBox.SetActive(true);
	
		Quest quest = quests[0];
		questText.text = quest.questText;
		Button[] buttons = dialogBox.GetComponentsInChildren<Button>();
		for (int i = 0; i < quest.resolutions.Length; i++)
		{
			Button b = buttons[i];
			QuestResolution resolution = quest.resolutions[i];
			b.GetComponentInChildren<Text>().text = resolution.action;
			// b.gameObject.SetActive(true);
		}
		// foreach (QuestResolution q in quest.resolutions)
		// {
		// 	Debug.Log("Quest resolution " + q.action);
		// 	Button button = Instantiate(template, dialogBox.transform, false);
		// 	// button.transform.parent = dialogBox.transform;
		// 	// button.transform.SetParent(dialogBox.transform, false);
		// 	// Text buttonText = button.transform.GetChild(0);
		// 	Debug.Log("Button enabled " + button.enabled);
		// }
	}
}

[System.Serializable]
public struct Quest
{
	public string questText;
	public QuestResolution[] resolutions;


}

[System.Serializable]
public struct QuestResolution
{
	public string action;
	public float closerToDoom;
}
