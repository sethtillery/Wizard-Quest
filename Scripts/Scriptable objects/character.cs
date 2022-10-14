using UnityEngine;
using System.Collections;

// Make the class abstract as it will need to be inherited by a subclass
public abstract class character : MonoBehaviour
{
    // Properties common to all characters
    public float hitPoints;
    public float maxHitPoints;
    public float startingHitPoints;

    public float currentMana;
    public float maxMana;

    public float movementSpeed;
    public Coroutine damageCoroutine;

    public enum CharacterCategory
    {
        PLAYER,
        ENEMY
    }

    public CharacterCategory characterCategory;

    public virtual void KillCharacter()
    {
        // Destroy the current game object and remove it from the scene
        Destroy(gameObject);
    }

    public abstract void ResetCharacter();

    // Cause the character to flicker red to show they've been hit
    public virtual IEnumerator FlickerCharacter()
    {
        // Tint the sprite red
        GetComponent<SpriteRenderer>().color = Color.red;

        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.05f);

        // Change the sprite back to default color
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    // Coroutine to inflict an amount of damage to the character over a period of time
    // interval = 0 to inflict a one-time damage hit
    // interval > 0 to continuously inflict damage at the set interval of time
    public IEnumerator DamageCharacter(int damage, float interval)
    {
        // Continuously inflict damage until the loop breaks
        while (true)
        {

            StartCoroutine(FlickerCharacter());

            // Inflict damage
            hitPoints = hitPoints - damage;

            // Player is dead; kill off game object and exit loop
            if (hitPoints <= 0)
            {

                KillCharacter();
                break;
            }

            if (interval > 0)
            {
                // Wait a specified amount of seconds and inflict more damage
                yield return new WaitForSeconds(interval);
            }
            else
            {
                // Interval = 0; inflict one-time damage and exit loop
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //print("test");
        //if (collision.gameObject.CompareTag("projectile"))
        //{
            
        //    damageCoroutine = StartCoroutine(this.DamageCharacter((int)collision.gameObject.GetComponent<fireball>().damage, 0));

        //}

    }
}