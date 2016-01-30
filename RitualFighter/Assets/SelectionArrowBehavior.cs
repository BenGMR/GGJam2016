using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SelectionArrowBehavior : MonoBehaviour
{

    public bool selected = false, noSelection = true;
    bool previousMove = false;

    Image image;
    float alpha = 1f, alphaIncrement = .02f;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();


    }

    // Update is called once per frame
    void Update()
    {

        if (noSelection)
        {
            alpha -= alphaIncrement;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

            if (alpha <= 0 || alpha >= 1f)
            {
                alphaIncrement *= -1;
            }
        }
        else if (selected && image.color != Color.blue)
        {
            image.color = Color.blue;
        }
        else if (!selected)
        {
            image.color = new Color(0, 0, 0, 0);
        }


        char indexChar = this.name[name.Length - 1];
        float axisValue = Input.GetAxis("LeftStickHorizontal" + indexChar.ToString());

        if (Math.Abs(axisValue) > .5f)
        {
            if (!previousMove)
            {
                if (axisValue < 0)
                {
                    if (this.name == "LeftArrowImage" + indexChar)
                    {
                        if (noSelection)
                        {
                            selected = true;
                            noSelection = false;
                        }
                    }
                    else
                    {
                        noSelection = false;
                        selected = false;
                    }
                }
                else
                {
                    if (this.name == "RightArrowImage" + indexChar)
                    {
                        if (noSelection)
                        {
                            selected = true;
                            noSelection = false;
                        }
                    }
                    else
                    {
                        noSelection = false;
                        selected = false;
                    }
                }

                previousMove = true;
            }
        }
        else
        {
            previousMove = false;
        }

    }
}
