using UnityEngine;
using TMPro;
using TWCore.Variables;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UI_IntRefToGradiant : MonoBehaviour
{
    #region BLACKBOARD VARIABLES
    [SerializeField] private FloatReference Min;
    [SerializeField] private IntReference Max;
    [SerializeField] private IntReference Curr;
    #endregion

    public Gradient GradientColor;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        float max = Max;
        float curr = Curr;
        float time = curr / max;
        _text.color = GradientColor.Evaluate(time);
    }
}
