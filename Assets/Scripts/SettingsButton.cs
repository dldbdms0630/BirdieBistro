using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsButton : MonoBehaviour
{
    public Button settingsButton;
    public GameObject settingsPanel;
    public Button exitButton; 
    // public Button submitButton;

    // Start is called before the first frame update
    void Start()
    {
        settingsButton.onClick.AddListener(OnSettingClick);
        exitButton.onClick.AddListener(Exit);
        // submitButton.onClick.AddListener(OnSubmit);
        
    }

    void OnSettingClick()
    {
        settingsPanel.SetActive(true);
    }

    private void Exit() 
    {
        settingsPanel.SetActive(false);
    }
}
