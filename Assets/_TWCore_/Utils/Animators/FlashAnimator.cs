using System.Collections;
using System.Collections.Generic;
using TWCore.Events;
using UnityEngine;
using System.Linq;
using TWCore.Utils;

[RequireComponent(typeof(GameEventListener))]
public class FlashAnimator : MonoBehaviour
{
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float durationInSeconds;

    private List<SpriteRenderer> renderers;
    private List<Material> originalMaterials;

    private Coroutine flashRoutine;

    void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>(true).ToList();
        originalMaterials = renderers.Select(r => r.material).ToList();
        flashMaterial = new Material(flashMaterial);
    }

    private IEnumerator FlashCoroutine(Color color)
    {
        flashMaterial.color = color;
        renderers.ForEach(r => r.material = flashMaterial);
        yield return new WaitForSeconds(durationInSeconds);

        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].material = originalMaterials[i];
        }

    }

    public void Flash(string colorString)
    {
        var color = CodeUtils.GetColorFromString(colorString);
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashCoroutine(color));
    }
}
