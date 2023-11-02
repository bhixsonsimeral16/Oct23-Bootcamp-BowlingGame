using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody ballRigidbody;
    [SerializeField] Collider ballCollider;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ballRigidbody = GetComponent<Rigidbody>();
        ballCollider = GetComponent<Collider>();
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
            gameManager.SetNextThrow();

            Destroy(gameObject);
        }      
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Pin"))
        {
            Debug.Log("Pin hit");
        }        
    }
}
