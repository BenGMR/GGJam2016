using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

    public GameObject LeftEmpty, RightEmpty, MiddleEmpty;
    public Image LeftArrow, RightArrow, ControllerImage;
    public bool ReadyPosition = false, Ready = false;
    public Image AButtonImage, ReadyImage;
    public Team team = Team.none;
    bool left = false;

	// Use this for initialization
	void Start () {
        AButtonImage.enabled = false;
        ReadyImage.enabled = false; 
	}
	
	// Update is called once per frame
	void Update () {
        if(LeftArrow.GetComponent<SelectionArrowBehavior>().selected && !Ready && !ReadyPosition)
        {
            ControllerImage.GetComponent<RectTransform>().position = LeftEmpty.GetComponent<RectTransform>().position;
            ReadyPosition = true;
            AButtonImage.enabled = true;
            AButtonImage.rectTransform.localPosition = new Vector3(ControllerImage.rectTransform.localPosition.x - AButtonImage.rectTransform.rect.width, ControllerImage.rectTransform.localPosition.y);
            ReadyImage.rectTransform.localPosition = new Vector3(ControllerImage.rectTransform.localPosition.x - ReadyImage.rectTransform.rect.width, ControllerImage.rectTransform.localPosition.y);
            team = Team.left;
        }
        else if(RightArrow.GetComponent<SelectionArrowBehavior>().selected && !Ready && !ReadyPosition)
        {
            ControllerImage.GetComponent<RectTransform>().position = RightEmpty.GetComponent<RectTransform>().position;
            ReadyPosition = true;
            AButtonImage.enabled = true;
            AButtonImage.rectTransform.localPosition = new Vector3(ControllerImage.rectTransform.localPosition.x + AButtonImage.rectTransform.rect.width, ControllerImage.rectTransform.localPosition.y);
            ReadyImage.rectTransform.localPosition = new Vector3(ControllerImage.rectTransform.localPosition.x + ReadyImage.rectTransform.rect.width, ControllerImage.rectTransform.localPosition.y);
            team = Team.right;
        }
        else if (Input.GetButton("A" + this.name[name.Length - 1]) && ReadyPosition)
        {
            Ready = true;
            ReadyImage.enabled = true;
            AButtonImage.enabled = false;
        }
        else if (Input.GetButton("B" + this.name[name.Length - 1]) && Ready)
        {
            Ready = false;
            ReadyImage.enabled = false;
            AButtonImage.enabled = true;
        }
        if ((!RightArrow.GetComponent<SelectionArrowBehavior>().selected && !LeftArrow.GetComponent<SelectionArrowBehavior>().selected))
        {
            ResetTeam();
        }
        
	}

    public void ResetTeam()
    {
        ControllerImage.GetComponent<RectTransform>().position = MiddleEmpty.GetComponent<RectTransform>().position;
        RightArrow.GetComponent<SelectionArrowBehavior>().noSelection = true;
        RightArrow.GetComponent<Image>().color = Color.red;
        LeftArrow.GetComponent<SelectionArrowBehavior>().noSelection = true;
        LeftArrow.GetComponent<Image>().color = Color.red;
        ReadyPosition = false;
        AButtonImage.enabled = false;
        Ready = false;
        ReadyImage.enabled = false;
        team = Team.none;
    }
}
