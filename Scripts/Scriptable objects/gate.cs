using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class gate : MonoBehaviour
{
    //variables
    public bool isActive;
    SpriteRenderer sprite;
    public Sprite spriteOff;
    public Sprite spriteOn;
    bool bool1 = false;
    bool bool2 = false;
    bool switched = false;

    //objects that activate the gate
    public GameObject Trigger1;
    public GameObject Trigger2;

    public void OnEnable()
    {

        //sets the spriteRenderer
        sprite = gameObject.GetComponent<SpriteRenderer>();

    }

    public void FixedUpdate() {

        //checks if the first Trigger is a fireballTrigger
        if (Trigger1.GetComponent<fireballTrigger>() != null)
        {

            bool1 = Trigger1.GetComponent<fireballTrigger>().isActive;

        }

        //checks if the first Trigger is a playerTrigger
        else if (Trigger1.GetComponent<playerTrigger>() != null) {

            bool1 = Trigger1.GetComponent<playerTrigger>().isActive;

        }

        //checks if the first Trigger is a pedestalTrigger
        else if (Trigger1.GetComponent<pedestalScript>() != null)
        {

            bool1 = Trigger1.GetComponent<pedestalScript>().isFilled;

        }

        //checks if the second Trigger is a fireballTrigger
        if (Trigger2.GetComponent<fireballTrigger>() != null)
        {

            bool2 = Trigger2.GetComponent<fireballTrigger>().isActive;

        }

        //checks if the second Trigger is a playerTrigger
        else if (Trigger2.GetComponent<playerTrigger>() != null)
        {

            bool2 = Trigger2.GetComponent<playerTrigger>().isActive;

        }

        //checks if the second Trigger is a pedestalTrigger
        else if (Trigger2.GetComponent<pedestalScript>() != null)
        {

            bool2 = Trigger2.GetComponent<pedestalScript>().isFilled;

        }

        //if both triggers are active and the gate hasn't already switched
        if (bool1 && bool2 && gameObject.activeInHierarchy && !switched)
        {

            //deativate the object and set switch to true
            gameObject.SetActive(false);
            switched = true;

        }

        else if (bool1 && bool2 && !gameObject.activeInHierarchy && !switched) {

            //ativate the object and set switch to true
            gameObject.SetActive(true);
            switched = true;

        }

    }
}
