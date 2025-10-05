using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactRange = 3f;
    public LayerMask interactableMask;

    [Header("References")]
    public Transform cameraTransform;

    private IInteractable currentInteractable;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactRange, interactableMask))
            {
                Debug.Log("Interacted with: " + hit.collider.name);
            }
            else
            {
                Debug.Log("Nothing to interact with.");
            }
        }
    }


    void CheckForInteractable()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableMask))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (interactable != currentInteractable)
                {
                    if (currentInteractable != null)
                        currentInteractable.OnLoseFocus();

                    currentInteractable = interactable;
                    currentInteractable.OnFocus();
                }
                return;
            }
        }

        // If we get here, player isn't looking at any interactable
        if (currentInteractable != null)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }
}
