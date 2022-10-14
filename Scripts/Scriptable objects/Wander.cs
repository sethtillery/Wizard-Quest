using System;
using System.Collections;
using UnityEngine;

// Ensure that the game object this is attached to has these components
// If it doesn't, they will be automatically added
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
    CircleCollider2D circleCollider;

    // Speed at which the enemy persues the player
    public float pursuitSpeed;

    // General wandering speed
    public float wanderSpeed;

    // Current speed, which is one of the previous two speeds
    float currentSpeed;

    // How often the enemy should change wandering directions
    public float directionChangeInterval;

    // Player-chasing behaviour (can turn off if characters other than enemies are created)
    public bool followPlayer;

    // Reference to the currently running movement coroutine
    Coroutine moveCoroutine;

    // Components attached to the game object
    Rigidbody2D rb2d;
    Animator animator;

    // The player's transform (position)
    Transform targetTransform = null;

    // Destination where the enemy is wandering
    Vector3 endPosition;

    // Angle is used to generate a vector which becomes the destination
    float currentAngle = 0;

    enum CharacterStates
    {

        walkRight = 1,
        walkDown = 2,
        walkLeft = 3,
        walkUp = 4,
        Idle = 5,
        idleLeft = 6,
        idleRight = 7,
        idleUp = 8

    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentSpeed = wanderSpeed;
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(WanderRoutine());
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnDrawGizmos()
    {
        if (circleCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
        }
    }

    private void Update()
    {
        Debug.DrawLine(rb2d.position, endPosition, Color.red);
    }

    public IEnumerator WanderRoutine()
    {

        // Enemy should wander indefinitely
        while (true)
        {
            // Choose a new endpoint for the enemy to move toward
            ChooseNewEndpoint();

            // If enemy is already moving, stop it before moving in a new direction
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            // Start the new move routine
            moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));

            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    private void ChooseNewEndpoint()
    {
        // Choose a random value between 0 and 360 to represent a new direction to travel toward
        currentAngle += UnityEngine.Random.Range(0, 360);

        // Effectively perform a mod operation so that currentAngle is always between 0 and 360
        currentAngle = Mathf.Repeat(currentAngle, 360);

        // Convert angle to a Vector3 and add result to endPosition
        endPosition = Vector3FromAngle(currentAngle);
    }

    // Takes an angle in degrees, converts it to radians, and returns a directional vector
    private Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        // Convert angle degrees to radians
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        // Create a normalized directional vector for the enemy direction
        return transform.position + new Vector3(Mathf.Sin(inputAngleRadians), Mathf.Cos(inputAngleRadians), 0)*10;
    
    }

    private IEnumerator Move(Rigidbody2D rigidBodyToMove, float speed)
    {
        // Retrieve the rough distance remaining between the current enemy position and the destination
        // Magnitude is a unity function to return the length of the vector
        float remainingDistance = (transform.position - endPosition).magnitude;

        while (remainingDistance > 0)
        {
            if (targetTransform != null)
            {
                // If targetTransform is set, then it's position is the player's position
                // This moves the enemy toward the player instead of toward the original endPosition
                endPosition = targetTransform.position;
            }

            if (rigidBodyToMove != null)
            {
                // Set animation parameter so animator will change the animation that's played
                if (Mathf.Abs(endPosition.x - rigidBodyToMove.position.x) > Mathf.Abs(endPosition.y - rigidBodyToMove.position.y))
                {

                    if (endPosition.x - rigidBodyToMove.position.x > 0)
                    {

                        animator.SetInteger("animationState", (int)CharacterStates.walkRight);

                    }

                    else
                    {

                        animator.SetInteger("animationState", (int)CharacterStates.walkLeft);

                    }

                }

                else if (Mathf.Abs(endPosition.x - rigidBodyToMove.position.x) < Mathf.Abs(endPosition.y - rigidBodyToMove.position.y))
                {

                    if (endPosition.y - rigidBodyToMove.position.y > 0)
                    {

                        animator.SetInteger("animationState", (int)CharacterStates.walkUp);

                    }

                    else
                    {

                        animator.SetInteger("animationState", (int)CharacterStates.walkDown);

                    }

                }

                else {

                    animator.SetInteger("animationState", (int)CharacterStates.Idle);

                }

                // Calculates the movement for a RigidBody2D
                // To make sure that object speed is independent of frame rate, multiply the speed by Time.deltaTime
                Vector3 newPosition = Vector3.MoveTowards(rigidBodyToMove.position, endPosition, speed * Time.deltaTime);

                // Move the RigidBody2D
                rb2d.MovePosition(newPosition);

                // Update the distance remaining
                remainingDistance = (transform.position - endPosition).magnitude;
            }

            // Pause execution until the next Fixed Frame Update
            yield return new WaitForFixedUpdate();
        }

        animator.SetInteger("animationState", (int)CharacterStates.Idle);
    }

    // Called when player enters the circle collider for the enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // See if the object that the enemy has collided with is the player and
        // that the enemy is supposed to be following the player
        if (collision.gameObject.CompareTag("Player") && followPlayer)
        {
            // Update the current speed
            currentSpeed = pursuitSpeed;

            // Set the targetTransform to be the player's
            targetTransform = collision.gameObject.transform;
        }

        // If enemy is moving, stop it
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        // Start the move routine with the updated information
        // i.e. to follow the player at the new speed
        moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));
    }

    // Called when player exits the circle collider for the enemy
    // Can only happen if player can move faster than the enemy
    private void OnTriggerExit2D(Collider2D collision)
    {
        // See if the object that the enemy is no longer colliding with is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Enemy is confused and pauses for a minute after losing sight of the player
            animator.SetInteger("animationState", (int)CharacterStates.Idle);

            // Slow the speed down
            currentSpeed = wanderSpeed;

            // If enemy is moving, stop it
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            // Set to null, since enemy is no longer following player
            targetTransform = null;
        }
    }
}
