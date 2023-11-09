using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speedPerSecond = 10;
    public int damage = -1;
    private void Start()
    {

    }
    // Update is called once per physics update cycle
    void FixedUpdate()
    {
        transform.Translate(new Vector3(speedPerSecond * Time.fixedDeltaTime, 0, 0));
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("Player"))
        {
            //Debug.Log("Pew Pew, hit an asteroid");
            //Destroy(collision.gameObject);
            bool isKilled = other.GetComponent<Health>().ChangeHealth(damage);
            if (isKilled)
            {
                Destroy(other);
            }
        }
        Destroy(gameObject);
        //Play explosion animation
        //GameObject explosion= Instantiate(explosionPrefab,transform.position, transform.rotation);

    }
}