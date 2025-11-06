using UnityEngine;

// BodyPart.cs
public class BodyPart : MonoBehaviour
{
    public string partName;
    public string groupName; // e.g., "RightArm", "LeftLeg", "Torso"
    public int price = 10;
    public float damage = 4.34782609f; // damage value
    public bool isVisible = true;
    public PlayerHealth playerHealth;


    private Material originalMaterial;
    public Material hoverMaterial;
    public Material clickedMaterial;

    private Renderer rend;
    private bool isSelected = false;

    public bool IsSelected => isSelected;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;
    }

    public void OnHoverEnter()
    {
        if (!isSelected && isVisible)
            rend.material = hoverMaterial;
    }

    public void OnHoverExit()
    {
        if (!isSelected && isVisible)
            rend.material = originalMaterial;
    }

    public void ToggleSelection()
    {
        if (!isVisible) return;

        isSelected = !isSelected;
        rend.material = isSelected ? clickedMaterial : originalMaterial;
        playerHealth.TakeDamage(damage);

    }

    public void Deselect()
    {
        isSelected = false;
        if (isVisible)
            rend.material = originalMaterial;
        playerHealth.Heal(damage);
    }

    public void LosePart()
    {
        isVisible = false;
        isSelected = false;
        rend.enabled = false;
    }
}
