using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostStrength = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.gameObject.name == "Player")
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                float currentSpeed = playerRb.velocity.magnitude;
                Vector3 boostDirection = other.transform.up;
                playerRb.AddForce(boostDirection * boostStrength, ForceMode.Impulse);
            }
        }
    }
}
