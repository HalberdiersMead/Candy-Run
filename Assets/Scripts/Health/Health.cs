using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int startHealth = 3;
    public int currentHealth { get; private set; }

    

    

    // Start is called before the first frame update
    void Start()
    {
        //check if player already has hp
        if (!PlayerPrefs.HasKey("HP"))
        {
            currentHealth = startHealth;
        } 
        else
        {
            currentHealth = PlayerPrefs.GetInt("HP");
        }
        
    }
    public int CurrentHealth
    {
        
        get { return currentHealth; }
        
    }
    public bool ChangeHealth(int changeValue)
    {
        //optimize like we did in class
        currentHealth=Mathf.Min(startHealth, currentHealth+changeValue);
        PlayerPrefs.SetInt("HP", currentHealth);
        //change current value of stored if altered in scenes
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
