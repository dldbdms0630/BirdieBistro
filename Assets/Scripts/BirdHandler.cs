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
    public Questionnaire openQuestionnaire;
    private const string FirstTimeKey = "IsFirstTimeEver";
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
        openQuestionnaire.SetActive(false); //feel free to put this in game handler
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
        Timer timer = new Timer(86400000); //timer made to reset the mealCount var
        timer.Elapsed += every24Hours;
        timer.AutoReset = true; // Automatically reset the timer after it elapses
        timer.Enabled = true;
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
    private void RunFirstTimeEverSetup()
    {
        openQuestionnaire.SetActive(true);
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

    private static void every24Hours(Object source, ElapsedEventArgs e)
    {
        if (mealCount < openQuestionnaire.mealGoal) //oh dear.. you didn't eat enough today.
        {
            foreach (bird in birdData.birds) //remove 2 hearts instead of 1 each day
            {
            bird.heartCount--; //whenever heartCount reaches 0, bird leaves
                if (heartCount == 0)
                {
                    bird.gameObject.SetActive(false); //i think thats how you do it..
                }
            }
        }
        mealCount = 0; // reset meals
        foreach (bird in birdData.birds)
        {
            bird.heartCount--; //whenever heartCount reaches 0, bird leave
            if (heartCount == 0)
            {
                bird.gameObject.SetActive(false); //i think thats how you do it..
            }
        }
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
            mealForm.setActive(true);
            if (mealForm.complete) {
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
