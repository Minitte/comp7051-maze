using UnityEngine;

public class BallThrower : MonoBehaviour {

    /// <summary>
    /// Reference to the ball prefab.
    /// </summary>
    public GameObject ballPrefab;

    /// <summary>
    /// The amount of force to put on the ball when thrown.
    /// </summary>
    public float force;

	// Update is called once per frame
	private void Update () {
		if (Input.GetButtonDown("ThrowBall")) {
            ThrowBall();
        }
	}

    /// <summary>
    /// Throws the ball.
    /// </summary>
    private void ThrowBall() {
        // Instantiate ball object and ignore collisions with the player
        GameObject ball = Instantiate(ballPrefab);
        Physics.IgnoreCollision(ball.GetComponent<Collider>(), GetComponent<CharacterController>());

        // Place ball at player's position, and throw it in direction of camera
        ball.transform.position = transform.position;
        Vector3 direction = GetComponent<Player>().fpsCamera.transform.forward;
        ball.GetComponent<Rigidbody>().AddForce(direction * force);
    }
}
