using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody ballRigidbody;
    [SerializeField] Collider ballCollider;
    GameManager gameManager;
    bool hasCollided = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ballRigidbody = GetComponent<Rigidbody>();
        ballCollider = GetComponent<Collider>();
        hasCollided = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Boundary"))
        {
            Debug.Log("Ball out of bounds");
            gameManager.soundManager.PlaySound("thud");
            gameManager.SetNextThrow();

            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Close-up"))
        {
            // Switch to close-up camera
            gameManager.SwitchCamera();
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Pin") && !hasCollided)
        {
            hasCollided = true;
            Debug.Log("Pin hit");
            gameManager.soundManager.PlaySound("pinCollision");
        }        
    }
}
