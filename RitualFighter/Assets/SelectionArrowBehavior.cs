using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectionArrowBehavior : MonoBehaviour {

    public bool selected = false;

    Image image;
    float alpha = 1f, alphaIncrement = .025f;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        
	}
	
	// Update is called once per frame
	void Update () {
	   if(!selected)
       {
           alpha -= alphaIncrement;
           image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
           if(alpha <= 0 || alpha >= 1f)
           {
               alphaIncrement *= -1;
           }
       }
       else if(image.color != Color.blue)
       {
           image.color = Color.blue;
       }
	}
}
