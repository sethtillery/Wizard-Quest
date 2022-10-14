using UnityEngine;

// Create an entry in the "Create submenu" to be able to easily create instances of the object
[CreateAssetMenu(menuName = "projectile")]

public class projectilesScript : ScriptableObject
{

    // Used for debugging, or possibly displaying the name of an item
    public string objectName;

    // Reference to the item's sprite, so it can be displayed
    public Sprite sprite;

    //basic variables for the projectile.
    public int damage;
    public float speed;
    public float time;
    public string direction;
    

    public enum projectileType { 
    
        FIREBALL,
        ARROW,
        ICE
    
    }

    public projectileType type;

    public Sprite getSprite()
    {

        return sprite;

    }

}
