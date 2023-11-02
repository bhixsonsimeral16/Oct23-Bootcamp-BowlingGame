using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerMoveSpeed;

    [SerializeField] float arrowHorizontalTravel;
    [SerializeField] float throwForce = 50f;
    [SerializeField] Transform throwingArrow;
    [SerializeField] Transform ballSpawnPoint;
    [SerializeField] List<Rigidbody> ballList = new List<Rigidbody>();
    [SerializeField] Animator arrowAnimator;

    float arrowMinZ, arrowMaxZ;
    Vector3 ballOffset;
    Rigidbody bowlingBallRigidbody;
    bool animValue;
    bool ballThrown = false;
    // Start is called before the first frame update
    void Start()
    {
        ballOffset = ballSpawnPoint.position - throwingArrow.position;
        arrowMaxZ = throwingArrow.position.z + 0.4f;
        arrowMinZ = throwingArrow.position.z - 0.4f;
    }

    public void StartThrow()
    {
        Debug.Log("StartThrow");
        arrowAnimator.SetBool("Aiming", true);
        SpawnBall();
    }

    private void SpawnBall()
    {
        // Instantiate the ball at the spawn point
        if (ballList.Count == 0)
        {
            throw new System.Exception("No bowling ball in the list");
        }
        ballThrown = false;

        // Destroy the previous ball
        if (bowlingBallRigidbody != null)
        {
            Destroy(bowlingBallRigidbody.gameObject);
        }
        // Generate random number to choose a ball
        int ballIndex = Random.Range(0, ballList.Count);
        bowlingBallRigidbody = Instantiate(ballList[ballIndex], ballSpawnPoint.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        TryMoveArrow();
        TryShootBall();
    }

    void TryMoveArrow()
    {
        if(!ballThrown)
        {
        // Move the throwing arrow without bounds
        // throwingArrow.position += throwingArrow.right * Input.GetAxis("Horizontal") * playerMoveSpeed * Time.deltaTime;

        // Move the throwing arrow in bounds
        float movePostition = -1 * Input.GetAxis("Horizontal") * playerMoveSpeed * Time.deltaTime;
        throwingArrow.position = new Vector3(throwingArrow.position.x, throwingArrow.position.y,
                                             Mathf.Clamp(throwingArrow.position.z + movePostition, arrowMinZ, arrowMaxZ));

        // Move the ball with the throwing arrow
        bowlingBallRigidbody.position = throwingArrow.position + ballOffset;
        ballSpawnPoint.position = throwingArrow.position + ballOffset;
        }
    }

    void TryShootBall()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !ballThrown)
        {
            ballThrown = true;
            bowlingBallRigidbody.AddForce(throwingArrow.forward * throwForce, ForceMode.Impulse);
            arrowAnimator.SetBool("Aiming", false);
        }
    }
}
