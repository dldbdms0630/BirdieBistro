using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BirdData : MonoBehaviour
{
    public static BirdData Instance; // Singleton instance
    public List<Bird> birds = new List<Bird>();


    public class Bird
    {
        public string name;
        public string dialogue; 
        public string dialogue_love;
        public string dialogue_hate;
        public string preferredCuisine;
        public int heartCount; // OR could change around how happy the bird is
        public GameObject birdPrefab;
    }


    void Awake()
    {
        // ensure there's only one instance of this manager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        // ALL BIRD DATA GOES HERE
        // side note: heartcount of the bird can depend on whether user has initially chosen the preferred cuisine as frequently cooked or not.
        // like if frequently cooked, then do like 3 days; if not, then do like 5 days. 
        birds.Add(new Bird { name = "Cockatoo", dialogue = "A taste of hummus or olives, perhaps?", 
            dialogue_love = "Now that’s a dish I can dive into!", dialogue_hate = "This is missing that Mediterranean zing.",
            preferredCuisine = "Mediterranean", heartCount = 3, birdPrefab = GetBirdPrefab("Cockatoo")});

        birds.Add(new Bird { name = "Redbird", dialogue = "I feel a bit chilly. Some curry could heat me up!",
            dialogue_love = "Mmm, the spices are just perfect!", dialogue_hate = "This is missing some serious flavor.",
            preferredCuisine = "Indian", heartCount = 3, birdPrefab = GetBirdPrefab("Redbird")});

        birds.Add(new Bird { name = "Opaline", dialogue = "I smell ginger and soy sauce...", 
            dialogue_love = "This hits the spot!", dialogue_hate = "Where's the wok magic??",
            preferredCuisine = "Chinese", heartCount = 3, birdPrefab = GetBirdPrefab("Opaline")});

        birds.Add(new Bird { name = "Hummingbird", dialogue = "Craving some salsa... not the dance!", 
            dialogue_love = "¡Esto es una FIESTA!", dialogue_hate = "Doesn’t exactly scream Taco Tuesday...",
            preferredCuisine = "Mexican", heartCount = 3, birdPrefab = GetBirdPrefab("Hummingbird") });

        birds.Add(new Bird { name = "Criticbird", dialogue = "Mamma Mia! I'm hungry for garlic.", 
            dialogue_love = "This tastes like a cozy evening in Rome!", dialogue_hate = "I’ll be honest, this is not al dente.",
            preferredCuisine = "Italian", heartCount = 3, birdPrefab = GetBirdPrefab("Criticbird") });

        birds.Add(new Bird { name = "Duck", dialogue = "Peep. Feed me!", 
            dialogue_love = "Yum, yum!!! Scrumptious! ", dialogue_hate = "I don't dislike much, but hey, it happens.",
            preferredCuisine = "All", heartCount = 3, birdPrefab = GetBirdPrefab("Duck") });
        
        Debug.Log($"First bird's name: {birds[0].name}");

    }

    public void ResetBirds()
    {
        // Clear the current list before re-adding birds.
        birds.Clear();

        // Re-add all original bird data.
        birds.Add(new Bird { name = "Cockatoo", dialogue = "A taste of hummus or olives, perhaps?", 
            dialogue_love = "Now that’s a dish I can dive into!", dialogue_hate = "This is missing that Mediterranean zing.",
            preferredCuisine = "Mediterranean", heartCount = 3, birdPrefab = GetBirdPrefab("Cockatoo")});

        birds.Add(new Bird { name = "Redbird", dialogue = "I feel a bit chilly. Some curry could heat me up!",
            dialogue_love = "Mmm, the spices are just perfect!", dialogue_hate = "This is missing some serious flavor.",
            preferredCuisine = "Indian", heartCount = 3, birdPrefab = GetBirdPrefab("Redbird")});

        birds.Add(new Bird { name = "Opaline", dialogue = "I smell ginger and soy sauce...", 
            dialogue_love = "This hits the spot!", dialogue_hate = "Where's the wok magic??",
            preferredCuisine = "Chinese", heartCount = 3, birdPrefab = GetBirdPrefab("Opaline")});

        birds.Add(new Bird { name = "Hummingbird", dialogue = "Craving some salsa... not the dance!", 
            dialogue_love = "¡Esto es una FIESTA!", dialogue_hate = "Doesn’t exactly scream Taco Tuesday...",
            preferredCuisine = "Mexican", heartCount = 3, birdPrefab = GetBirdPrefab("Hummingbird") });

        birds.Add(new Bird { name = "Criticbird", dialogue = "Mamma Mia! I'm hungry for garlic.", 
            dialogue_love = "This tastes like a cozy evening in Rome!", dialogue_hate = "I’ll be honest, this is not al dente.",
            preferredCuisine = "Italian", heartCount = 3, birdPrefab = GetBirdPrefab("Criticbird") });

        birds.Add(new Bird { name = "Duck", dialogue = "Peep. Feed me!", 
            dialogue_love = "Yum, yum!!! Scrumptious! ", dialogue_hate = "I don't dislike much, but hey, it happens.",
            preferredCuisine = "All", heartCount = 3, birdPrefab = GetBirdPrefab("Duck") });

        Debug.Log("Bird list has been reset.");
    }


    public void RemoveBirdsByName(List<string> birdNamesToRemove)
    {
        foreach (string name in birdNamesToRemove)
        {
            // Remove from the data list.
            birds.RemoveAll(bird => bird.name == name);

            // Find any existing bird instances in the scene and destroy them.
            BirdHandler[] birdInstances = FindObjectsOfType<BirdHandler>();
            foreach (BirdHandler birdInstance in birdInstances)
            {
                if (birdInstance.birdData.name == name)
                {
                    Destroy(birdInstance.gameObject);
                }
            }
        }
        
        Debug.Log("Birds removed. Remaining birds:");
        foreach (var bird in birds)
        {
            Debug.Log(bird.name);
        }
    }



    // based on questionairre choose what to delete. 

    private GameObject GetBirdPrefab(string prefabName)
    {
        return Resources.Load<GameObject>(prefabName);
    }
    
    public Bird GetBirdByName(string birdName)
    {
        return birds.Find(bird => bird.name == birdName);
    }
}
