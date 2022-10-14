using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : character
{
    // Amount of damage the enemy will inflict when it runs into the player
    public int damageStrength;

    private void OnEnable()
    {
        ResetCharacter();
    }

    //resets character
    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
    }

    //kills charcter
    public override void KillCharacter() {

        Destroy(gameObject);

    }

}
