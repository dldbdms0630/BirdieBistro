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
    private const string FirstTimeKey = "IsFirstTimeEver";


    void Start()
    {
        speechBubble.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        recipeInputButton.gameObject.SetActive(false);
        mealForm.SetActive(false);

        if (PlayerPrefs.HasKey(FirstTimeKey))
        {
            // The game has been launched before, nothing to do
            Debug.Log("Welcome back!");
        }
        else
        {
            // This is the first time ever opening the game
            Debug.Log("First time ever opening the game!");

            RunFirstTimeEverSetup();

            // Mark that the first time setup has been completed
            PlayerPrefs.SetInt(FirstTimeKey, 1);
            PlayerPrefs.Save(); 
        }
    }

    void RunFirstTimeEverSetup()
    {
        // big back bird logo 
        // get instructions up 
    }
    
}
