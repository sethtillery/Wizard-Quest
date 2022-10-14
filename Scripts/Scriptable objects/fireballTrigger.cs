using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]


public class fireballTrigger : MonoBehaviour
{
    //variables
    public bool isActive;

    //onEnable checks if it's active and makes it a lit torch
    public void OnEnable() {

        //checks if it's active and makes it a lit torch
        if (isActive)
        {

            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //checks if the collision is with a projectile and that the projectile is a fireball
        if (collision.gameObject.CompareTag("projectile") && collision.gameObject.GetComponent<projectiles>().projectilesScript.objectName == "fireball") {

            //is the torch is not active set it to active and show the fire for the lit torch
            if (!isActive) {

                isActive = true;

                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

            }
        
        }

    }
}