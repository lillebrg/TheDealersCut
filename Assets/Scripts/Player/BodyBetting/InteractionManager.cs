using UnityEngine;

// InteractionManager.cs
public class InteractionManager : MonoBehaviour
{
    private BodyPart hoveredPart;
    public LayerMask bodyPartLayer;
    public BodyBettingManager bettingManager;

    void Update()
    {
        HandleHoverAndClick();
    }

    void HandleHoverAndClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, bodyPartLayer))
        {
            BodyPart part = hit.collider.GetComponent<BodyPart>();

            if (hoveredPart != part)
            {
                if (hoveredPart != null)
                    hoveredPart.OnHoverExit();

                hoveredPart = part;
                hoveredPart.OnHoverEnter();
            }

            if (Input.GetMouseButtonDown(0) && hoveredPart != null)
            {
                bettingManager.SelectOrDeselectPart(hoveredPart);
            }
        }
        else
        {
            if (hoveredPart != null)
            {
                hoveredPart.OnHoverExit();
                hoveredPart = null;
            }
        }
    }
}
