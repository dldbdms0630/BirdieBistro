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

    public Vector3 zoomedInScale = new Vector3(1.1f, 1.1f, 1f);
    public Vector3 zoomedInPosition = new Vector3(-4.14f, -1.3f, -1.44f);
    public Sprite happySprite;

    private Vector3 originalPosition;
    private Vector3 originalScale;
    // private BirdData.Bird specificBird;

    private bool isZoomedIn = false;
    private string birdName; 
    private int mealCount = 0;

    private List<BirdHandler> otherBirds = new List<BirdHandler>();

    void Start()
    {
        originalScale = transform.localScale; // store originalScale for resetting 
        originalPosition = transform.position;
        // hide UI initially 
        // speechBubble.SetActive(false);
        // dialogueText.gameObject.SetActive(false);
        // recipeInputButton.gameObject.SetActive(false);
        // mealForm.SetActive(false);
        birdName = gameObject.name;
        // specificBird = birdData.BirdData.Bird;
    }

    public void AssignBirdData(BirdData.Bird data, GameObject speechBubble, TextMeshProUGUI dialogueText, Button recipeInputButton, GameObject mealForm)
    {
        Debug.Log("Data Assigned.");
        // specificBird = data;
        birdData = data;
        this.speechBubble = speechBubble;
        this.dialogueText = dialogueText;
        this.recipeInputButton = recipeInputButton;
        this.mealForm = mealForm;

        Debug.Log($"SpeechBubble: {speechBubble}, DialogueText: {dialogueText}, RecipeInputButton: {recipeInputButton}, MealForm: {mealForm}");

    }

    void OnMouseDown()
    {
        Debug.Log("Pressed Down!");
        if (!isZoomedIn)
        {
            ZoomIn();
            DeactivateOtherBirds();
        } 
        else
        {
            ZoomOut();
            ReactivateOtherBirds();
        }
    }

    void ZoomIn()
    {
        transform.localScale = zoomedInScale;
        transform.position = zoomedInPosition;
        isZoomedIn = true;
        
        speechBubble.SetActive(true);
        dialogueText.gameObject.SetActive(true);        
        recipeInputButton.gameObject.SetActive(true);

        dialogueText.text = birdData.dialogue;

        recipeInputButton.onClick.AddListener(OnMealFormClicked);
        

        // dialogueText.text = birdData.GetBirdByName(birdName).dialogue;
    }

    void OnMealFormClicked()
    {
        if (mealForm != null)
        {
            MealSubmissionForm form = mealForm.GetComponent<MealSubmissionForm>();
            mealForm.SetActive(true);
            if (form.complete) {
                mealCount++; //gotta reset mealcount everyday (DATETIME) 
            }
        }
        else
        {
            Debug.LogWarning("mealform is not assigned.");
        }
    }

    void ZoomOut()
    {
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
    }

    void ReactivateOtherBirds()
    {
        foreach (BirdHandler bird in otherBirds)
        {
            bird.gameObject.SetActive(true);
        }
        otherBirds.Clear();
    }
}
