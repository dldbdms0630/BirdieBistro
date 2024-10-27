using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BirdHandler : MonoBehaviour
{
    public BirdData.Bird birdData; 
    public GameObject speechBubble;
    public TextMeshProUGUI dialogueText; // make into textmeshpro instead?? 
    public Button recipeInputButton;
    public GameObject mealForm;
    public Button inviteButton;

    public Vector3 zoomedInScale = new Vector3(0.9f, 0.9f, 1f);
    public Vector3 zoomedInPosition = new Vector3(-4.14f, -1.3f, -1.44f);
    public Sprite happySprite;

    private SpriteRenderer spriteRenderer; 
    private Sprite originalSprite; // store the original sprite
    private Collider2D birdCollider;
    private bool isZoomedIn = false;

    private Vector3 originalPosition;
    private Vector3 originalScale;
    // private BirdData.Bird specificBird;

    private string birdName; 
    // private int mealCount = 0;
    // private int maxMealsPerDay = 3;
    private DateTime lastMealSubmissionDate;


    private List<BirdHandler> otherBirds = new List<BirdHandler>();

    void Start()
    {
        // UpdateMealCountText();
        // ClearPlayerPrefsForTesting();
        originalScale = transform.localScale; // store originalScale for resetting 
        originalPosition = transform.position;
        birdCollider = GetComponent<Collider2D>();
        
        birdName = gameObject.name;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer != null) 
        {
            originalSprite = spriteRenderer.sprite; // store originl sprite
        }


        // LoadMealData();
        // Check if a new day has started.
        // if (IsNewDay())
        // {
        //     ResetDailyMealCount();
        // }
        // // Update the recipe input button's status.
        // UpdateRecipeInputButton();
    }

    // private void ClearPlayerPrefsForTesting()
    // {
    //     PlayerPrefs.DeleteKey($"{birdData.name}_MealCount");
    //     PlayerPrefs.DeleteKey($"{birdData.name}_LastMealDate");
    //     PlayerPrefs.Save();
    //     Debug.Log("PlayerPrefs cleared for testing.");
    // }

    public void AssignBirdData(BirdData.Bird data, GameObject speechBubble, TextMeshProUGUI dialogueText, Button recipeInputButton, GameObject mealForm, Button inviteButton, TextMeshProUGUI mealCountText)
    {
        Debug.Log("Data Assigned.");
        // specificBird = data;
        birdData = data;
        this.speechBubble = speechBubble;
        this.dialogueText = dialogueText;
        this.recipeInputButton = recipeInputButton;
        this.mealForm = mealForm;
        this.inviteButton = inviteButton;
        // this.mealCountText = mealCountText;

        // Debug.Log($"SpeechBubble: {speechBubble}, DialogueText: {dialogueText}, RecipeInputButton: {recipeInputButton}, MealForm: {mealForm}");

    }

    void OnMouseDown()
    {
        Debug.Log("Pressed Down!");
        if (!isZoomedIn && !IsMealFormActive())
        {
            ZoomIn();
            DeactivateOtherBirds();
        } 
        else if (isZoomedIn)
        {
            ZoomOut();
            ReactivateOtherBirds();
        }
    }

    private bool IsMealFormActive()
    {
        return mealForm != null && mealForm.activeSelf;
    }

    void ZoomIn()
    {
        if (spriteRenderer != null) 
        {
            spriteRenderer.sprite = originalSprite;
        }

        transform.localScale = zoomedInScale;
        transform.position = zoomedInPosition;
        isZoomedIn = true;
        
        speechBubble.SetActive(true);
        dialogueText.gameObject.SetActive(true);        
        recipeInputButton.gameObject.SetActive(true);

        dialogueText.text = birdData.dialogue;
        recipeInputButton.onClick.AddListener(OnMealFormClicked);

    }

    void OnMealFormClicked()
    {
        if (mealForm != null)
        {
            MealSubmissionForm form = mealForm.GetComponent<MealSubmissionForm>();
            form.SetBirdHandler(this); // set this birdhandler as the reference for 
            mealForm.SetActive(true);            
            if (birdCollider != null)
            {
                birdCollider.enabled = false;
            }

            // if (form.complete) {
            //     mealCount++; //gotta reset mealcount everyday (DATETIME) 
            // }
        }
        else
        {
            Debug.LogWarning("mealform is not assigned.");
        }
    }

    void ZoomOut()
    {
        ReactivateBirdInteraction();

        transform.localScale = originalScale;
        transform.position = originalPosition; 
        isZoomedIn = false;
        speechBubble.SetActive(false);
        recipeInputButton.gameObject.SetActive(false);

        dialogueText.gameObject.SetActive(false);

    }

    void DeactivateOtherBirds()
    {
        otherBirds.Clear();
        BirdHandler[] allBirds = FindObjectsOfType<BirdHandler>();
        
        foreach(BirdHandler bird in allBirds)
        {
            if (bird != this) // add all birds except this one
            {
                bird.gameObject.SetActive(false);
                otherBirds.Add(bird);
            }
        }
        inviteButton.gameObject.SetActive(false);
    }

    public void ReactivateBirdInteraction()
    {
        if (birdCollider != null)
        {
            birdCollider.enabled = true;
        }
    }
    

    void ReactivateOtherBirds()
    {
        foreach (BirdHandler bird in otherBirds)
        {
            bird.gameObject.SetActive(true);
        }
        otherBirds.Clear();

        inviteButton.gameObject.SetActive(true);

    }

    public void ReceiveMeal(string dishType, string preparedMethod)
    {
        Debug.Log($"BHandler - Meal received for {birdData.name}: {dishType}, {preparedMethod}");
        
        bool isDuck = birdData.name.Equals("Duck", StringComparison.OrdinalIgnoreCase);

        bool isPreferredCuisine = birdData.preferredCuisine == "All" || dishType == birdData.preferredCuisine;
        bool isHomeCooked = preparedMethod == "Cooked";
        bool isTakeOut = preparedMethod == "Takeout";

        if (isPreferredCuisine)
        {
            if (isHomeCooked)
            {
                // Homecooked and matches the preferred cuisine: increase heart.
                birdData.heartCount++;
                dialogueText.text = birdData.dialogue_love;

                // Change sprite to happy if applicable.
                if (spriteRenderer != null && happySprite != null)
                {
                    spriteRenderer.sprite = happySprite;
                }
                Debug.Log($"{birdData.name} loves the homecooked meal and gained a heart!");
            }
            else if (isTakeOut)
            {
                // don't match cuisine type but still cook then 50% 

                // Takeout and matches the preferred cuisine: 50% chance for everything 
                float chance = .5f;
                if (UnityEngine.Random.value < chance)
                {
                    birdData.heartCount++;
                    dialogueText.text = birdData.dialogue_love;

                    // Change sprite to happy if applicable.
                    if (spriteRenderer != null && happySprite != null)
                    {
                        spriteRenderer.sprite = happySprite;
                    }
                    Debug.Log($"{birdData.name} was okay with the takeout and gained a heart!");
                }
                else
                {
                    dialogueText.text = birdData.dialogue_hate;
                    Debug.Log($"{birdData.name} didn't appreciate the takeout.");
                }
            }
        }
        else
        {
            Debug.Log($"{birdData.name} doesn't like this piece of shiat.");
            // birdData.heartCount--;
            dialogueText.text = birdData.dialogue_hate;

            // check if heart count has reached zero
            if (birdData.heartCount <= 0)
            {
                Debug.Log($"{birdData.name} is leaving the village.");
                LeaveVillage();
            }
        }
        
        // mealCount++;
        // UpdateMealCountText();
        // lastMealSubmissionDate = DateTime.Now;
        // SaveMealData();


        // if (mealCount >= maxMealsPerDay) 
        // {
        //     recipeInputButton.interactable = false;
        // }
        // UpdateBirdUI();
    }

    // private void UpdateMealCountText()
    // {
    //     mealCountText.text = mealCount.ToString() +"/" +maxMealsPerDay.ToString() + " meals";
    // }

    // private void ResetDailyMealCount()
    // {
    //     if (IsNewDay())
    //     {
    //     mealCount = 0;
    //     UpdateMealCountText();
    //     recipeInputButton.interactable = true;
    //     SaveMealData();
    //     }
    // }

    // private bool IsNewDay()
    // {
    //     return lastMealSubmissionDate.Date != DateTime.Now.Date;
    // }

    // private void LoadMealData()
    // {
    //     // Load the meal count and last meal submission date from PlayerPrefs.
    //     mealCount = PlayerPrefs.GetInt($"{birdData.name}_MealCount", 0);
    //     string lastDateString = PlayerPrefs.GetString($"{birdData.name}_LastMealDate", DateTime.Now.ToString());

    //     if (DateTime.TryParse(lastDateString, out DateTime parsedDate))
    //     {
    //         lastMealSubmissionDate = parsedDate;
    //     }
    //     else
    //     {
    //         lastMealSubmissionDate = DateTime.Now;
    //     }

    //     UpdateMealCountText();
    //     UpdateRecipeInputButton();
    // }

    // private void SaveMealData()
    // {
    //     // Save the meal count and last meal submission date to PlayerPrefs.
    //     PlayerPrefs.SetInt($"{birdData.name}_MealCount", mealCount);
    //     PlayerPrefs.SetString($"{birdData.name}_LastMealDate", lastMealSubmissionDate.ToString());
    //     PlayerPrefs.Save();
    // }

    // private void UpdateRecipeInputButton()
    // {
    //     // Update the recipe input button based on the current meal count.
    //     recipeInputButton.interactable = mealCount < maxMealsPerDay;
    // }


    private void LeaveVillage()
    {
        Destroy(gameObject);
    }

    // private void UpdateBirdUI()
    // {
    //     // update stuff like heartcount, dialogue etc based on new state 
    // }
}
