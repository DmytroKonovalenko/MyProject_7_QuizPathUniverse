using UnityEngine;
using TMPro;

public class NameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField inputField; 
    public TextMeshProUGUI displayText; 

    private const string NameKey = "PlayerName";
    private const string DefaultName = "Enter Name"; 

    private void Start()
    {
        string savedName = PlayerPrefs.GetString(NameKey, DefaultName);

    
        inputField.text = savedName;
        displayText.text = savedName;

 
        inputField.onValueChanged.AddListener(OnNameChanged);
    }

    private void OnNameChanged(string newName)
    {

        displayText.text = string.IsNullOrWhiteSpace(newName) ? DefaultName : newName;
    }

    public void SaveName()
    {
     
        string nameToSave = string.IsNullOrWhiteSpace(inputField.text) ? DefaultName : inputField.text;
        PlayerPrefs.SetString(NameKey, nameToSave);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
 
        SaveName();
    }
}
