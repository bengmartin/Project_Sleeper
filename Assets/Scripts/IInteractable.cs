using UnityEngine;

public interface IInteractable
{
    // Called when the player presses their interact key
    void Interact();

    // Called when the player is looking at the object (for crosshair or UI prompts)
    void OnFocus();

    // Called when the player looks away from the object
    void OnLoseFocus();
}
