using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVController : MonoBehaviour
{
    public Transform player;
    public float minFOV = 60f;  // Minimum FOV
    public float maxFOV = 100f;  // Maximum FOV
    public float speedThreshold = 5f;  // Speed threshold to trigger FOV change
    public float lerpSpeed = 5f;  // Speed at which FOV changes
      
    public Camera playerCamera;
    private float currentFOV;
    private float targetFOV;

    void Start()
    {
        currentFOV = playerCamera.fieldOfView;
        targetFOV = currentFOV;
    }

    void Update()
    {
        float playerSpeed = player.GetComponent<Rigidbody>().velocity.magnitude;

        // Calculate the target FOV based on player speed
        if (playerSpeed >= speedThreshold)
        {
            targetFOV = Mathf.Lerp(minFOV, maxFOV, (playerSpeed - speedThreshold) / (playerSpeed * 2));
        }
        else
        {
            targetFOV = minFOV;
        }

        // Smoothly change the FOV towards the target FOV
        currentFOV = Mathf.Lerp(currentFOV, targetFOV, Time.deltaTime * lerpSpeed);

        // Apply the FOV to the camera
        playerCamera.fieldOfView = currentFOV;
    }
}
