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
    public bool complete = false; //sees if user successfully puts in entry

    // Start is called before the first frame update
    void Start()
    {
        // Add listener to buttons
        submitButton.onClick.AddListener(OnSubmit);
        exitButton.onClick.AddListener(Exit);
    }

    private void OnSubmit()
    {
        // Gather inputs
        string recipeName = recipeNameInput.text;
        string dishType = dishTypeDropdown.options[dishTypeDropdown.value].text;
        string ingredients = ingredientsInput.text;
        // string date = dateInput.text;

        string mealType = mealTypeDropdown.options[mealTypeDropdown.value].text;

        // string mealType = "";
        // if (breakfastToggle.isOn) mealType = "Breakfast";
        // else if (lunchToggle.isOn) mealType = "Lunch";
        // else if (dinnerToggle.isOn) mealType = "Dinner";

        string preparedMethod = cookedToggle.isOn ? "Cooked" : "Takeout";

        // Validation
        if (string.IsNullOrEmpty(recipeName) || string.IsNullOrEmpty(dishType) || string.IsNullOrEmpty(ingredients) || string.IsNullOrEmpty(mealType))
        {
            Debug.LogWarning("Please fill in all fields.");
            return;
        }

        // Display the submitted data (for testing purposes)
        string message = $"Recipe Name: {recipeName}\nDish Type: {dishType}\nIngredients: {ingredients}\nMeal Type: {mealType}\nPrepared Method: {preparedMethod}";
        Debug.Log(message);
        complete = true;
        gameObject.SetActive(false);
    }

    private void Exit() {
        gameObject.SetActive(false);
        // Application.Quit();
    }
}
