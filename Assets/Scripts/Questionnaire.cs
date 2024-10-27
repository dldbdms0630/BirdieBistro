using System.Collections;
using System.Collections.Generic;
using TMPro;  // For TextMeshPro support
using UnityEngine;
using UnityEngine.UI;

public class Questionnaire : MonoBehaviour
{

    public TMP_InputField numberOfMeals;  // TextMeshPro input field
    public BirdData birdData;
    public MealSubmissionForm form;

    public Toggle chineseToggle;
    public Toggle indianToggle;
    public Toggle italianToggle;
    public Toggle mediterraneanToggle;
    public Toggle mexicanToggle;
    public Toggle yesToggle;

    public Button submitButton; //name submit button this var.
    private string resultText;

    public int mealGoal = 3;
    private List<string> birdsToDelete;
    string willingness;

    void Start() 
    {
        birdsToDelete = new List<string>();
        submitButton.onClick.AddListener(OnSubmit);
    }
    
    void OnSubmit()
    {
        //only stored inputs in the variables for now
        mealGoal = int.Parse(numberOfMeals.text); //goal for number of meals
        form.UpdateMealCountText();
        form.UpdateRecipeInputButton();
        
        // if (yesToggle.isOn) then we put all birds in the selected
        // and also call BirdData to update it 
        birdData.ResetBirds();

        if (!yesToggle.isOn) // then not willing to try new cuisines. 
        {
            if (!italianToggle.isOn) birdsToDelete.Add("Criticbird");
            if (!chineseToggle.isOn) birdsToDelete.Add("Opaline");
            if (!mexicanToggle.isOn) birdsToDelete.Add("Hummingbird");
            if (!indianToggle.isOn) birdsToDelete.Add("Redbird");
            if (!mediterraneanToggle.isOn) birdsToDelete.Add("Cockatoo");


            Debug.Log("Birds in the list: " + string.Join(", ", birdsToDelete));
            birdData.RemoveBirdsByName(birdsToDelete);

        }

        gameObject.SetActive(false);

    }
}