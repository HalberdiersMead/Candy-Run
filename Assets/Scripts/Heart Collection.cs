using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollection : MonoBehaviour
{
    public int healing = 1;
    // Start is called before the first frame update
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().ChangeHealth(healing);
        }
        Destroy(gameObject);
        //Play explosion animation
        //GameObject explosion= Instantiate(explosionPrefab,transform.position, transform.rotation);

    }
}
