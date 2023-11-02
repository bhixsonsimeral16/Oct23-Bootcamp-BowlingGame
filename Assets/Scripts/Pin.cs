using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Pin : MonoBehaviour
{
    bool outOfBounds = false;
    public bool isFallen = false;

    Vector3 startingPosition;
    Quaternion startingRotation;

    [SerializeField] Rigidbody pinRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        pinRigidbody = GetComponent<Rigidbody>();
        startingPosition = transform.position;
        startingRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            CheckIsFallen();
        }
    }

    void CheckIsFallen()
    {
        // If the pin is tilted more than 5 degrees, it is considered fallen
        if (Quaternion.Angle(startingRotation, transform.rotation) > 5)
        {
            isFallen = true;
        }

        // If the pin has entered the pit, it is considered fallen
        else if (outOfBounds)
        {
            isFallen = true;
        }
    }

    public void ResetPin()
    {
        gameObject.SetActive(true);
        pinRigidbody.velocity = Vector3.zero;
        pinRigidbody.angularVelocity = Vector3.zero;
        pinRigidbody.isKinematic = true;

        // Reset position and rotation
        // Reset position is slightly above the ground
        transform.position = startingPosition + Vector3.up * 0.01f;
        transform.rotation = startingRotation;
        isFallen = false;
        outOfBounds = false;
        pinRigidbody.isKinematic = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // This is the general lane boundary,
        // but will most often be used for if the pin enters the pit
        if (other.gameObject.CompareTag("Boundary"))
        {
            outOfBounds = true;
        }
    }
}
