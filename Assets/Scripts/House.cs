using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class House : MonoBehaviour {
// päris
	public Quest[] quests;
	public GameObject dialogBox;
	public Text questText;

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
