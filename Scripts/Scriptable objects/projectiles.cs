using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectiles : MonoBehaviour
{

    //initialize variables
    public projectilesScript projectilesScript;
    public string direction;
    Coroutine damageCoroutine;
    public Animator animator;

    Vector2 movement = new Vector2();
    Rigidbody2D rb2D;

    void OnEnable() {

        //sets the rigidBody and sets a timer on the projectile to destroy itself 
        rb2D = GetComponent<Rigidbody2D>();
        Object.Destroy(gameObject, projectilesScript.time);
        animator = this.GetComponent<Animator>();

    }

    void FixedUpdate() {

        move();
    
    }

    public void move()
    {
        //sets movement based on the direction of the projectile
        if (direction == "right")
        {

            movement.x = 1;

        }

        else if (direction == "left")
        {

            movement.x = -1;

        }

        else if (direction == "up")
        {

            movement.y = 1;

        }

        else if (direction == "down")
        {

            movement.y = -1;

        }

        // keeps projectile moving at the same rate of speed, no matter which direction they are moving in
        movement.Normalize();

        // set velocity of RigidBody2D and move it
        rb2D.velocity = movement * projectilesScript.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //checks if the collision is with a trigger or not
        if (!collision.isTrigger)
        {

            //checks it the collision is with an enemy
            if (collision.gameObject.CompareTag("enemy"))
            {

                //destroys the projectile and deals damage
                Object.Destroy(gameObject);
                collision.gameObject.GetComponent<character>().damageCoroutine = StartCoroutine(collision.gameObject.GetComponent<character>().DamageCharacter(projectilesScript.damage, 0));

            }

            //checks if the collision is with a wall
            else if (collision.gameObject.CompareTag("wall"))
            {

                //destroys the projectile
                Object.Destroy(gameObject);

            }

        }
    }

}
