using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BirdSpawner : MonoBehaviour
{

    public BirdData birdDatabase; 
    // public GameObject birdPrefab;
    // public Transform spawnArea;
    public Button inviteButton;

    [SerializeField] private GameObject speechBubble;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button recipeInputButton;
    [SerializeField] private GameObject mealForm;
    
    void Start()
    {
        inviteButton.onClick.AddListener(InviteNewBird);
        UpdateInviteButton();
    }

    public void InviteNewBird()
    {
        // Debug.Log("BirdInvited");
        if (NoBirdsInScene())
        {
            BirdData.Bird randomBirdData = GetRandomBirdData();
            SpawnBird(randomBirdData); // invite the random bird 
        }
        UpdateInviteButton(); // disable button after bird is invited 
    }

    private BirdData.Bird GetRandomBirdData()
    {
        int randomIndex = Random.Range(0, birdDatabase.birds.Count);
        return birdDatabase.birds[randomIndex];
    }

    private void SpawnBird(BirdData.Bird birdData)
    {
        if (birdData.birdPrefab != null)
        {

            GameObject bird = Instantiate(birdData.birdPrefab, GetRandomPosition(),
                Quaternion.identity);

            BirdHandler birdHandler = bird.GetComponent<BirdHandler>();
            birdHandler.AssignBirdData(birdData, speechBubble, dialogueText, recipeInputButton, mealForm);


            // GameObject speechBubble = GameObject.Find("DialogueBox"); // Adjust the name to match the actual object in your scene.
            
            
            // if (speechBubble == null)
            // {
            //     Debug.LogWarning("Speech Bubble not found!");
            // }
            // else{
            //     Debug.Log(" speech bubble found!!");
            // }
        
            // TextMeshProUGUI dialogueText = GameObject.Find("DialogueText")?.GetComponent<TextMeshProUGUI>();
            // Button recipeInputButton = GameObject.Find("Feed")?.GetComponent<Button>();
            // GameObject mealForm = GameObject.Find("MealForm");

        } else 
            Debug.LogWarning($"BirdData for {birdData.name} does not have an assigned prefab.");
    }

    private Vector3 GetRandomPosition()
    {
        // Define limits based on screen size or a predefined area.
        float xMin = -5f; // Example values, adjust based on your scene.
        float xMax = 5f;
        float yMin = -3f;
        float yMax = 1f;

        float randomX = Random.Range(xMin, xMax);
        float randomY = Random.Range(yMin, yMax);

        return new Vector3(randomX, randomY, -1f);
    }

    private bool NoBirdsInScene()
    {
        return GameObject.FindObjectsOfType<BirdHandler>().Length == 0;
    }

    private void UpdateInviteButton()
    {
        inviteButton.gameObject.SetActive(NoBirdsInScene());
    }
}
