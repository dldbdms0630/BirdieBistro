using System.Collections;
using System.Collections.Generic;
using TMPro;  // For TextMeshPro support
using UnityEngine;
using UnityEngine.UI;

public class Questionnaire : MonoBehaviour
{

    public TMP_InputField numberOfMeals;  // TextMeshPro input field

    public Toggle italianToggle;
    public Toggle chineseToggle;
    public Toggle mexicanToggle;
    public Toggle indianToggle;
    public Toggle mediterraneanToggle;
    public Toggle yesToggle;
    public Toggle noNewCuisines;

    public Button submitButton; //name submit button this var.
    public TextMeshProUGUI resultText;

    public int mealGoal;
    public List<string> selectedCuisines;
    string willingness;

    void Start() 
    {
        submitButton.onClick.AddListener(OnSubmit);
    }
    
    void OnSubmit()
    {
        //only stored inputs in the variables for now
        mealGoal = int.Parse(numberOfMeals.text); //goal for number of meals
        
        selectedCuisines = new List<string>(); //list of types of meals user will eat

        if (italianToggle.isOn) selectedCuisines.Add("Italian");
        if (chineseToggle.isOn) selectedCuisines.Add("Chinese");
        if (mexicanToggle.isOn) selectedCuisines.Add("Mexican");
        if (indianToggle.isOn) selectedCuisines.Add("Indian");
        if (mediterraneanToggle.isOn) selectedCuisines.Add("Mediterranean");
        
        string willingness = yesToggle.isOn ? "Yes" : "No"; //will user try every type of dish

        resultText.text = "Selected cuisines: " + string.Join(", ", selectedCuisines) + "\n" +
                          "Willing to try new cuisines: " + willingness;

        Debug.Log(resultText.text);
    }
}