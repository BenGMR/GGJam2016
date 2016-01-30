using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

    public GameObject LeftEmpty, RightEmpty, MiddleEmpty;
    public Image LeftArrow, RightArrow, ControllerImage;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(LeftArrow.GetComponent<SelectionArrowBehavior>().selected)
        {
            ControllerImage.GetComponent<RectTransform>().position = LeftEmpty.GetComponent<RectTransform>().position;
        }
        else if(RightArrow.GetComponent<SelectionArrowBehavior>().selected)
        {
            ControllerImage.GetComponent<RectTransform>().position = RightEmpty.GetComponent<RectTransform>().position;
        }
        else
        {
            ControllerImage.GetComponent<RectTransform>().position = MiddleEmpty.GetComponent<RectTransform>().position;
            RightArrow.GetComponent<SelectionArrowBehavior>().noSelection = true;
            RightArrow.GetComponent<Image>().color = Color.red;
            LeftArrow.GetComponent<SelectionArrowBehavior>().noSelection = true;
            LeftArrow.GetComponent<Image>().color = Color.red;
        }
	}
}
