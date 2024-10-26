using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BirdHandler : MonoBehaviour
{
    public BirdData birdData; 
    public GameObject speechBubble;
    public TextMeshProUGUI dialogueText; // make into textmeshpro instead?? 
    public GameObject recipeInputButton;
    public Vector3 zoomedInScale = new Vector3(1.1f, 1.1f, 1f);
    public Vector3 originalScale;

    private bool isZoomedIn = false;
    private string birdName; 

    void Start()
    {
        originalScale = transform.localScale; // store originalScale for resetting 
        // hide UI initially 
        speechBubble.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        // recipeInputButton.SetActive(false);
        birdName = gameObject.name;
    }

    void OnMouseDown()
    {
        Debug.Log("Pressed Down!");
        if (!isZoomedIn)
        {
            ZoomIn();
        } 
        else
        {
            ZoomOut();
        }
        
    }

    void ZoomIn()
    {
        transform.localScale = zoomedInScale;
        isZoomedIn = true;
        speechBubble.SetActive(true);
        dialogueText.gameObject.SetActive(true);

        dialogueText.text = birdData.GetBirdByName(birdName).dialogue;
    }

    void ZoomOut()
    {
        transform.localScale = originalScale;
        isZoomedIn = false;
        speechBubble.SetActive(false);


        dialogueText.gameObject.SetActive(false);
    }
}
