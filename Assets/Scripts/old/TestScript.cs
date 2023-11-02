using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    [SerializeField] string startText = "Start Game!";

    [SerializeField] Transform objectToMove;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(startText);
    }

    // Update is called once per frame
    void Update()
    {
        objectToMove.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if(Input.GetKey(KeyCode.W))
        {
            Vector3 horizontalMovement = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z);
            objectToMove.Translate(horizontalMovement * moveSpeed * Time.deltaTime, Space.World);
        }

        if(Input.GetKey(KeyCode.S))
        {
            Vector3 horizontalMovement = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z);
            objectToMove.Translate(-1 * horizontalMovement * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
