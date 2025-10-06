using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    public AudioClip footstepClip; // Add this field in Inspector

    private CharacterController controller;
    private AudioSource audioSource; // Add this reference
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Play randomized footsteps when grounded and moving
        if (controller.isGrounded && move.magnitude > 0.1f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = footstepClip;

                // Start playback at a random position in the clip
                audioSource.time = Random.Range(0f, footstepClip.length - 0.05f);

                // Add subtle pitch variation
                audioSource.pitch = Random.Range(0.9f, 1.1f);

                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
