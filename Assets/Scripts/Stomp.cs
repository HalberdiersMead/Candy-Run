using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = -1;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        //damages the player only on collision
        if (other.CompareTag("WeakSpot"))
        {

            bool isKilled = other.GetComponent<Health>().ChangeHealth(damage);
            if (isKilled)
            {
                Destroy(other);
            }
        }
    }
}
