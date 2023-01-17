using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TWCore.Variables;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UI_FloatRefToText : MonoBehaviour
{
    #region BLACKBOARD VARIABLES
    [SerializeField] private FloatReference floatRef;
    #endregion

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _text.text = floatRef.Value.ToString();
    }
}
