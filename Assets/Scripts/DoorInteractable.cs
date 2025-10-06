using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoorInteractable : MonoBehaviour, IInteractable
{
    [Header("Door Settings")]
    public Transform doorTransform; // The object that moves (can be same as this)
    public Vector3 openOffset = new Vector3(0, 3, 0); // How far it moves when opened
    public float openSpeed = 2f; // How fast the door moves

    [Header("Audio Settings")]
    public AudioClip openSound;
    public AudioClip closeSound;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;
    private AudioSource audioSource;

    void Start()
    {
        if (doorTransform == null)
            doorTransform = this.transform;

        closedPosition = doorTransform.position;
        openPosition = closedPosition + openOffset;

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
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

        // Play corresponding sound
        if (audioSource != null)
        {
            AudioClip clipToPlay = isOpen ? openSound : closeSound;
            if (clipToPlay != null)
            {
                audioSource.clip = clipToPlay;
                audioSource.pitch = Random.Range(0.95f, 1.05f); // Small pitch variation
                audioSource.Play();
            }
        }
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
