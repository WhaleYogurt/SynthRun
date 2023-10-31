using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnHit : MonoBehaviour
{
    public GameObject intactWall; // Assign your intact wall model here
    public GameObject brokenWall; // Assign your broken wall model here
     
    public float breakForceThreshold = 10f; // You can adjust this value based on your needs

    private bool isBroken = false;
    public BoxCollider wallCollider;
    public bool usePreMadeModel = true;
    public Fracture script;
    public bool slowTime = true;

    private void Start()
    {
        // Make sure intact wall is shown at start and broken wall is hidden
        intactWall.SetActive(true);
        brokenWall.SetActive(false);

        wallCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (usePreMadeModel)
        {
            if (isBroken) return; // No need to check again if the wall is already broken

            float impactForce = collision.relativeVelocity.magnitude * collision.rigidbody?.mass ?? 1f;

            if (impactForce > breakForceThreshold)
            {
                BreakWall();
            }
        }
        else
        {
            if (script != null)
            {
                script.CauseFracture();
            }
        }
    }

    private void BreakWall()
    {
        isBroken = true;

        // Swap models
        intactWall.SetActive(false);
        brokenWall.SetActive(true);

        // Disable the box collider
        if (wallCollider != null)
        {
            wallCollider.enabled = false;
        }

        // Slow down time
        if (slowTime) { StartCoroutine(SlowMotionEffect()); }
    }

    private IEnumerator SlowMotionEffect()
    {
        float originalTimeScale = Time.timeScale;
        float originalFixedDeltaTime = Time.fixedDeltaTime;
        float targetTimeScale = 0.2f;  // Target slow-motion speed
        float transitionDuration = 0.5f; // Time taken to transition to and from slow-motion. Adjust as needed.
        float slowMotionDuration = 1f;  // Duration of the slow-motion effect once fully transitioned. Adjust as needed.
         
        // Transition into slow-motion
        for (float t = 0; t < 1; t += Time.unscaledDeltaTime / transitionDuration)
        {
            Time.timeScale = Mathf.Lerp(originalTimeScale, targetTimeScale, t);
            Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;
            yield return null;
        }
        Time.timeScale = targetTimeScale; // Ensure we hit the target exactly

        // Wait in slow-motion
        yield return new WaitForSecondsRealtime(slowMotionDuration);

        // Transition out of slow-motion
        for (float t = 0; t < 1; t += Time.unscaledDeltaTime / transitionDuration)
        {
            Time.timeScale = Mathf.Lerp(targetTimeScale, originalTimeScale, t);
            Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;
            yield return null;
        }
        Time.timeScale = originalTimeScale; // Reset time scale to its original value
        Time.fixedDeltaTime = originalFixedDeltaTime; // Reset fixedDeltaTime to its original value
    }
}
