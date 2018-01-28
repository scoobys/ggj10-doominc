using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Underline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI _text;
    private void Awake()
    {
        _text = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.fontStyle = FontStyles.Underline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.fontStyle = FontStyles.Normal;
    }
}