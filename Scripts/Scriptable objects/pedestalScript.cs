using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedestalScript : MonoBehaviour
{
    //variables
    public bool isFilled;
    Inventory inventory;
    public Item display;

    //a cooldown to prevent it from taking more than one item
    float pickupRate = .05f;
    float pickupCooldown;

    //objects to activate when the pedestal it filled
    public GameObject obj1;
    public GameObject obj2;

    private void OnTriggerEnter2D(Collider2D collision) {

        //if the collison is with a player, it's not already filled, and the cooldown is not active
        if (collision.gameObject.CompareTag("Player") && !isFilled && Time.time > pickupCooldown) {

            //gets the inventory of the player
            inventory = collision.gameObject.GetComponent<player>().inventory;

            //if the player has the desired item, it takes it from the inventory
            if (inventory.RemoveItem(display)) {

                //sets isFilled and shows the object
                isFilled = true;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

                //activates the objects if they aren't null
                if (obj1 != null) {

                    obj1.SetActive(true);

                }

                if (obj2 != null)
                {

                    obj2.SetActive(true);

                }

            }

        }

        //updates the cooldown
        pickupCooldown = Time.time + pickupRate;

    }

}
