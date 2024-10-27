using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameHandler : MonoBehaviour
{
    public GameObject speechBubble;
    public TextMeshProUGUI dialogueText;
    public Button recipeInputButton;
    public GameObject mealForm;

    void Start()
    {
        speechBubble.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        recipeInputButton.gameObject.SetActive(false);
        mealForm.SetActive(false);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
