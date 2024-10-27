using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MealSubmissionForm : MonoBehaviour
{
    // UI Elements
    public TMP_InputField recipeNameInput;
    public TMP_Dropdown dishTypeDropdown;
    public TMP_Dropdown mealTypeDropdown;
    public TMP_InputField ingredientsInput;
    public Toggle cookedToggle;
    public Toggle takeoutToggle;
    // public Button imageUploadButton; // TAKE OUT JUST FOR NOW. 
    public Button submitButton; //button to submit
    public Button exitButton; //button when user wants to exit
    // public bool complete = false; //sees if user successfully puts in entry


    private BirdHandler birdHandler; // reference to BirdHandler that has opened the form 

    void Start()
    {
        // Add listener to buttons
        submitButton.onClick.AddListener(OnSubmit);
        exitButton.onClick.AddListener(Exit);
    }

    public void SetBirdHandler(BirdHandler handler)
    {
        birdHandler = handler;
    }

    private void OnSubmit()
    {
        // Gather inputs
        string recipeName = recipeNameInput.text;
        string dishType = dishTypeDropdown.options[dishTypeDropdown.value].text;
        string ingredients = ingredientsInput.text;
        string mealType = mealTypeDropdown.options[mealTypeDropdown.value].text;
        string preparedMethod = "";

        if (cookedToggle.isOn)
        {
            preparedMethod = "Cooked";
        } else if (takeoutToggle.isOn)
        {
            preparedMethod = "Takeout";
        }

        // Validation
        if (string.IsNullOrEmpty(recipeName) || string.IsNullOrEmpty(dishType) || string.IsNullOrEmpty(ingredients) || string.IsNullOrEmpty(mealType) || !cookedToggle.isOn && !takeoutToggle.isOn)
        {
            Debug.LogWarning("Please fill in all fields, including selecting a preparation method.");
            return;
        }

        if (birdHandler != null) 
        {
            birdHandler.ReceiveMeal(dishType, preparedMethod);
        }
        else
        {
            Debug.LogWarning("BirdHandler reference is not set!");
        }
        // Display the submitted data (for testing purposes)
        string message = $"Recipe Name: {recipeName}\nDish Type: {dishType}\nIngredients: {ingredients}\nMeal Type: {mealType}\nPrepared Method: {preparedMethod}";
        Debug.Log(message);

        /* ADD STUFF HERE CALLING OTHER SCRIPTS IF WE WANT A SCRAPBOOK OR WHATEVER */

        ResetForm();


        

        // complete = true;
        gameObject.SetActive(false);

        if (birdHandler != null)
        {
            birdHandler.ReactivateBirdInteraction();
        }
    }

    private void Exit() {
        gameObject.SetActive(false);

        if (birdHandler != null)
        {
            birdHandler.ReactivateBirdInteraction();
        }
    }

    private void ResetForm()
    {
        recipeNameInput.text = "";
        dishTypeDropdown.value = 0;
        ingredientsInput.text = "";
        mealTypeDropdown.value = 0;
        cookedToggle.isOn = false;
        takeoutToggle.isOn = false;
    }
}
