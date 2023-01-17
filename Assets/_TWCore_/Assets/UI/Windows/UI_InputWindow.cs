using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InputWindow : MonoBehaviour
{
    private Button acceptBtn;
    private Button cancelBtn;
    private TMP_Text titleText;
    private TMP_InputField inputField;

    private UI_InputWindow inputWindow;

    private bool initialized = false;
    private void Awake()
    {
        if (initialized) return;
        Initialize();

        Hide();
    }

    private void Initialize()
    {
        acceptBtn = transform.Find("AcceptButton").GetComponent<Button>();
        cancelBtn = transform.Find("CancelButton").GetComponent<Button>();
        titleText = transform.Find("TitleText").GetComponent<TMP_Text>();
        inputField = transform.Find("InputField").GetComponent<TMP_InputField>();
        initialized = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            acceptBtn.onClick.Invoke();
        
        if (Input.GetKeyDown(KeyCode.Escape))
            cancelBtn.onClick.Invoke();
    }

    public void Show(string title, string input, int characterLimit, Action onCancel, Action<string> onAccept)
    {
        Show(title, input, 
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789",
            characterLimit, onCancel, onAccept);
    }

    public void Show(string title, string input, string validCharacters, int characterLimit, Action onCancel, Action<string> onAccept)
    {
        if (!initialized)
            Initialize();

        titleText.text = title;
        inputField.characterLimit = characterLimit;
        inputField.onValidateInput = (string _, int _, char addedChar) => OnValidateInputField(validCharacters, addedChar);
        inputField.text = input;

        acceptBtn.onClick.AddListener(() => 
        {
             onAccept(inputField.text); Hide();
        });
        cancelBtn.onClick.AddListener(() => 
        {
             onCancel(); Hide();
        });

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private char OnValidateInputField(string validCharacters, char addedChar)
    {
        return validCharacters.Contains(addedChar) ? addedChar : '\0';
    }
}
