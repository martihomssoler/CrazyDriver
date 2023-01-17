using UnityEngine;
using TMPro;
using TWCore.Variables;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UI_IntRefToText : MonoBehaviour
{
    #region BLACKBOARD VARIABLES
    [SerializeField] private IntReference intRef;
    #endregion

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _text.text = intRef.Value.ToString();
    }
}
