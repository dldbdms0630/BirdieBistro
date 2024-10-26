using TMPro;  // For TextMeshPro support
using UnityEngine;
using UnityEngine.UI;

public class PopUSystem : MonoBehaviour
{

    public TMP_InputField inputField;  // TextMeshPro input field

    public Toggle italianToggle;
    public Toggle chineseToggle;
    public Toggle mexicanToggle;
    public Toggle indianToggle;
    public Toggle mediterraneanToggle;
    public Toggle yesNewCuisines;
    public Toggle noNewCuisines;

    public Button submitButton; //name submit button this var.
    public TextMeshProUGUI displayText;

    void Start() 
    {
        submitButton.onClick.AddListener(OnSubmit);
    }
    
    void OnSubmit()
    {
        
        List<string> selectedCuisines = new List<string>();

        if (italianToggle.isOn) selectedCuisines.Add("Italian");
        if (chineseToggle.isOn) selectedCuisines.Add("Chinese");
        if (mexicanToggle.isOn) selectedCuisines.Add("Mexican");
        if (indianToggle.isOn) selectedCuisines.Add("Indian");
        if (mediterraneanToggle.isOn) selectedCuisines.Add("Mediterranean");
        
        string willingness = yesToggle.isOn ? "Yes" : "No";

        resultText.text = "Selected cuisines: " + string.Join(", ", selectedCuisines) + "\n" +
                          "Willing to try new cuisines: " + willingness;

        Debug.Log(resultText.text);
    }
}