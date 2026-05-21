using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRSimpleInteractable))]
public sealed class Cube : MonoBehaviour
{
    [SerializeField] private Color touchedColor = Color.green;
    [SerializeField] private Renderer targetRenderer;

    private XRSimpleInteractable interactable;
    private MaterialPropertyBlock propertyBlock;
    private Color initialColor = Color.white;

    // Esta variable en memoria RAM es la que evitará CUALQUIER fallo de la GPU
    private Color currentColor;

    private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");
    private static readonly int ColorId = Shader.PropertyToID("_Color");
    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();

        propertyBlock = new MaterialPropertyBlock();

        // Guardamos el color inicial real del material
        initialColor = ReadInitialColor();
        currentColor = initialColor;
    }
    private void Start()
    {
        GameManagerGame.instance.AddToTheListOfTopos(this);
    }
    private void OnEnable()
    {
        interactable.hoverEntered.AddListener(OnHoverEntered);
    }

    private void OnDisable()
    {
        interactable.hoverEntered.RemoveListener(OnHoverEntered);
    }
    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (targetRenderer == null) return;

        if (currentColor.Equals(Color.red))
        {
            SetColor(touchedColor);
            GameManagerGame.instance.SumScore(10);
        }
    }
    private Color ReadInitialColor()
    {
        if (targetRenderer == null || targetRenderer.sharedMaterial == null)
            return Color.white;

        Material material = targetRenderer.sharedMaterial;

        if (material.HasProperty(BaseColorId))
            return material.GetColor(BaseColorId);

        if (material.HasProperty(ColorId))
            return material.GetColor(ColorId);

        return Color.white;
    }

    // El GameManager llama aquí. Si le pasa Color.white, forzamos que use el color inicial del objeto
    internal void ChangeColor(Color color)
    {
        if (color.Equals(Color.white))
        {
            SetColor(initialColor);
        }
        else
        {
            SetColor(color);
        }
    }

    private void SetColor(Color color)
    {
        if (targetRenderer == null) return;

        targetRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor(BaseColorId, color);
        propertyBlock.SetColor(ColorId, color);
        targetRenderer.SetPropertyBlock(propertyBlock);

        // Actualizamos la variable local inmediatamente para el siguiente frame
        currentColor = color;
    }
    internal Color GetColor()
    {
        return currentColor;
    }
}