using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BirdSpawner : MonoBehaviour
{

    public BirdData birdDatabase; 
    public Button inviteButton;

    private int maxInvitesPerDay = 3;
    private int currentInvitesToday = 0;
    private DateTime lastInviteDate;

    [SerializeField] private GameObject speechBubble;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button recipeInputButton;
    [SerializeField] private GameObject mealForm;
    [SerializeField] private TextMeshProUGUI mealCountText;
    
    void Start()
    {
        inviteButton.onClick.AddListener(InviteNewBird);

        LoadInviteData();

        if (IsNewDay()) 
        {
            ResetDailyInvites();
        }
        UpdateInviteButton();
    }

    public void InviteNewBird()
    {
        // if (NoBirdsInScene())
        if (currentInvitesToday < maxInvitesPerDay)
        {
            List<BirdData.Bird> availableBirds = GetAvailableBirds();

            if (availableBirds.Count > 0)
            {
                BirdData.Bird randomBirdData = availableBirds[UnityEngine.Random.Range(0, availableBirds.Count)];
                SpawnBird(randomBirdData);
                currentInvitesToday++;
                lastInviteDate = DateTime.Now;

                SaveInviteData();
            }
            else 
            {
                Debug.LogWarning("No new bird types available to invite!");
            }

            UpdateInviteButton();
        }
        else
        {
            Debug.LogWarning("Max invites reached for today.");
            inviteButton.interactable = false;
        }
    }

    private List<BirdData.Bird> GetAvailableBirds()
    {
        List<BirdData.Bird> availableBirds = new List<BirdData.Bird>();

        // Iterate through all birds in the database.
        foreach (BirdData.Bird bird in birdDatabase.birds)
        {
            // Add the bird to the available list if it's not already in the village.
            if (!BirdAlreadyInVillage(bird.name))
            {
                availableBirds.Add(bird);
            }
        }

        return availableBirds;
    }

    private bool BirdAlreadyInVillage(string birdName)
    {
        // Check if a bird with the given name is already in the village.
        BirdHandler[] existingBirds = FindObjectsOfType<BirdHandler>();
        foreach (BirdHandler bird in existingBirds)
        {
            if (bird.birdData.name == birdName)
            {
                return true; // Bird with this name is already in the village.
            }
        }
        return false;
    }


    private BirdData.Bird GetRandomBirdData()
    {
        if (birdDatabase != null && birdDatabase.birds.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, birdDatabase.birds.Count);
            return birdDatabase.birds[randomIndex];
        }
        else
        {
            Debug.LogWarning("Bird database is empty or not assigned.");
            return null;
        }
    }

    private void SpawnBird(BirdData.Bird birdData)
    {
        if (birdData.birdPrefab != null)
        {

            GameObject bird = Instantiate(birdData.birdPrefab, GetRandomPosition(),
                Quaternion.identity);

            BirdHandler birdHandler = bird.GetComponent<BirdHandler>();
            birdHandler.AssignBirdData(birdData, speechBubble, dialogueText, recipeInputButton, mealForm, inviteButton, mealCountText);
        } else 
            Debug.LogWarning($"BirdData for {birdData.name} does not have an assigned prefab.");
    }

    private Vector3 GetRandomPosition()
    {
        // Define limits based on screen size or a predefined area.
        float xMin = -5f; // Example values, adjust based on your scene.
        float xMax = 5f;
        float yMin = -3f;
        float yMax = -1f;

        float randomX = UnityEngine.Random.Range(xMin, xMax);
        float randomY = UnityEngine.Random.Range(yMin, yMax);

        return new Vector3(randomX, randomY, -1f);
    }


    // private bool NoBirdsInScene()
    // {
    //     return GameObject.FindObjectsOfType<BirdHandler>().Length == 0;
    // }

    private void ResetDailyInvites()
    {
        currentInvitesToday = 0;
        SaveInviteData();
        UpdateInviteButton();
    }

    private bool IsNewDay()
    {
        // check if last invite date is different from today. if true then is new day
        // return lastInviteDate.Date != DateTime.Now.Date;
        return true;
    }

    private void UpdateInviteButton()
    {
        inviteButton.interactable = (currentInvitesToday < maxInvitesPerDay);
    }

    private void LoadInviteData()
    {
        currentInvitesToday = PlayerPrefs.GetInt("CurrentInvitesToday", 0);
        string lastDateString = PlayerPrefs.GetString("LastInviteDate", DateTime.Now.ToString());

        if (DateTime.TryParse(lastDateString, out DateTime parsedDate))
        {
            lastInviteDate = parsedDate;
        }
        else
        {
            lastInviteDate = DateTime.Now;
        }
    }

    private void SaveInviteData()
    {
        PlayerPrefs.SetInt("CurrentInvitesToday", currentInvitesToday);
        PlayerPrefs.SetString("LastInviteDate", lastInviteDate.ToString());
        PlayerPrefs.Save();
    }
}
