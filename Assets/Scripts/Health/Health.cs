using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int startHealth = 3;
    public int currentHealth { get; private set; }

    public int CurrentHealth
    {
        get { return currentHealth; }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
    }

    public bool ChangeHealth(int changeValue)
    {
        //optimize like we did in class
        currentHealth=Mathf.Min(startHealth, currentHealth+changeValue);
        if (currentHealth <= 0) {    
            Die();
            return true;
        }
        return false;
    }

    private void Die()
    {
        //checks to see if the player dies
        GameObject other = this.gameObject;
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("MainMenu");
        }
        Destroy(gameObject);
        
        
    }
}
