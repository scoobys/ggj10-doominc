using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperCloseButtonController : MonoBehaviour {

    NewspaperController newspaperController;

    void Start () {
        newspaperController = gameObject.transform.parent.GetComponent<NewspaperController>();
    }

    void OnMouseUp() {
        newspaperController.Close();
    }
}
