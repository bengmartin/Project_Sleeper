using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [Header("Door Settings")]
    public Transform doorTransform; // The object that moves (can be same as this)
    public Vector3 openOffset = new Vector3(0, 3, 0); // How far it moves when opened
    public float openSpeed = 2f; // How fast the door moves

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;

    void Start()
    {
        if (doorTransform == null)
            doorTransform = this.transform;

        closedPosition = doorTransform.position;
        openPosition = closedPosition + openOffset;
    }

    void Update()
    {
        // Smoothly move door
        if (isOpen)
            doorTransform.position = Vector3.Lerp(doorTransform.position, openPosition, Time.deltaTime * openSpeed);
        else
            doorTransform.position = Vector3.Lerp(doorTransform.position, closedPosition, Time.deltaTime * openSpeed);
    }

    // Implement interface method
    public void Interact()
    {
        isOpen = !isOpen; // Toggle door state
        Debug.Log("Door toggled: " + (isOpen ? "Open" : "Closed"));
    }

    public void OnFocus()
    {
        // Optional: highlight or show prompt
    }

    public void OnLoseFocus()
    {
        // Optional: remove highlight
    }
}
