using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class control : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private float speed = 30f;
    [SerializeField] private float xRange = 11f;
    [SerializeField] private float yRange = 6.5f;

    [Header("Laser")]
    [SerializeField] private GameObject[] lasers;
    [SerializeField] private AudioClip laserSFX;
    private AudioSource audioSource;

    [Header("Movement Settings")]
    [SerializeField] private float pitchFactor = -2f;
    [SerializeField] private float yawFactor = 2f;
    [SerializeField] private float controlPitchFactor = 15f;
    [SerializeField] private float rollControl = -20f;

    [Header("UI Controls")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private Button fireButton;


    private float xInput, yInput;
    private float laserCooldown = 0.2f;
    private float lastShotTime = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ActivateLasers(false);

        if (fireButton != null)
            fireButton.onClick.AddListener(FireLaser); // Link button to fire method
    }

    void Update()
    {
        ProcessMovement();
        HandleRotation();
    }

    void ProcessMovement()
    {
        xInput = joystick.Horizontal; // Read joystick X movement
        yInput = joystick.Vertical;   // Read joystick Y movement

        float xOffset = xInput * Time.deltaTime * speed;
        float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -xRange, xRange);

        float yOffset = yInput * Time.deltaTime * speed;
        float newYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -yRange, yRange);

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }

    void HandleRotation()
    {
        float pitch = (transform.localPosition.y * pitchFactor) + (yInput * controlPitchFactor);
        float yaw = transform.localPosition.x * yawFactor;
        float roll = xInput * rollControl;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    public void FireLaser()
    {
        if (Time.time - lastShotTime >= laserCooldown)
        {
            lastShotTime = Time.time;
            ActivateLasers(true);
            audioSource.PlayOneShot(laserSFX);
            StartCoroutine(DisableLaserAfterDelay(0.1f));
        }
    }

    void ActivateLasers(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionParticles = laser.GetComponent<ParticleSystem>().emission;
            emissionParticles.enabled = isActive;
        }
    }

    IEnumerator DisableLaserAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ActivateLasers(false);
    }
}
