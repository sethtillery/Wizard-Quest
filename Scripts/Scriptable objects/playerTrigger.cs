using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class playerTrigger : MonoBehaviour
{

    //variables
    public bool isActive;
    public bool isToggle;
    SpriteRenderer sprite;


    //object to activate
    public GameObject obj1;

    public void OnEnable()
    {

        //sets the spriteRenderer
        sprite = gameObject.GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //checks if the collision is with a player
        if (collision.gameObject.CompareTag("Player"))
        {

            //if its not already active
            if (!isActive)
            {

                //sets isActive to true and changes the sprite
                isActive = true;
                sprite.color = Color.gray;

                //activates the object
                if (obj1 != null)
                {

                    obj1.SetActive(true);

                }


            }

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        //checks if the collision is with a player and that its a toggle
        if (collision.gameObject.CompareTag("Player") && isToggle)
        {

            //if it is active
            if (isActive)
            {

                //sets isActive to false and changes the sprite
                isActive = false;
                sprite.color = Color.white;

                //deactivtes the object
                if (obj1 != null)
                {

                    obj1.SetActive(false);

                }

            }

        }

    }
}
