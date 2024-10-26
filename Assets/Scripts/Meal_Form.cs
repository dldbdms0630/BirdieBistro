using UnityEngine;
using UnityEngine.UI;

public class MealSubmissionForm : MonoBehaviour
{
    // UI Elements
    public InputField recipeNameInput;
    public Dropdown dishTypeDropdown;
    public InputField ingredientsInput;
    public InputField dateInput;
    public Toggle breakfastToggle;
    public Toggle lunchToggle;
    public Toggle dinnerToggle;
    public Toggle cookedToggle;
    public Toggle takeoutToggle;
    public Button imageUploadButton;
    public Button submitButton;
    public Button reopenButton;

    // Start is called before the first frame update
    void Start()
    {
        // Add listener to buttons
        submitButton.onClick.AddListener(OnSubmit);
        reopenButton.onClick.AddListener(OnReopen);

        // Hide the reopen button initially
        reopenButton.gameObject.SetActive(false);
    }

    private void OnSubmit()
    {
        // Gather inputs
        string recipeName = recipeNameInput.text;
        string dishType = dishTypeDropdown.options[dishTypeDropdown.value].text;
        string ingredients = ingredientsInput.text;
        string date = dateInput.text;

        string mealType = "";
        if (breakfastToggle.isOn) mealType = "Breakfast";
        else if (lunchToggle.isOn) mealType = "Lunch";
        else if (dinnerToggle.isOn) mealType = "Dinner";

        string preparedMethod = cookedToggle.isOn ? "Cooked" : "Takeout";

        // Validation
        if (string.IsNullOrEmpty(recipeName) || string.IsNullOrEmpty(dishType) || string.IsNullOrEmpty(ingredients) || string.IsNullOrEmpty(date) || string.IsNullOrEmpty(mealType))
        {
            Debug.LogWarning("Please fill in all fields.");
            return;
        }

        // Display the submitted data (for testing purposes)
        string message = $"Recipe Name: {recipeName}\nDish Type: {dishType}\nIngredients: {ingredients}\nDate: {date}\nMeal Type: {mealType}\nPrepared Method: {preparedMethod}";
        Debug.Log(message);

        // Hide the form and show the reopen button
        gameObject.SetActive(false);
        reopenButton.gameObject.SetActive(true);
    }

    private void OnReopen()
    {
        // Reset the form
        recipeNameInput.text = "";
        dishTypeDropdown.value = 0; // Assuming the first option is "Select dish type"
        ingredientsInput.text = "";
        dateInput.text = "";
        breakfastToggle.isOn = false;
        lunchToggle.isOn = false;
        dinnerToggle.isOn = false;
        cookedToggle.isOn = false;
        takeoutToggle.isOn = false;

        // Show the form and hide the reopen button
        gameObject.SetActive(true);
        reopenButton.gameObject.SetActive(false);
    }
}
