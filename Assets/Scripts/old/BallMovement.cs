using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float accelerationForce = 100f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Add forward force to the ball forward on w key press
        if (Input.GetKey(KeyCode.W))
        {
            AccelerateBall();
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision with " + other.gameObject.name);
    }

    void AccelerateBall()
    {
        rb.AddForce((new Vector3(1f, 0f, 0f)) * accelerationForce * Time.deltaTime);
    }
}
