using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
   // Start is called before the first frame update
   [SerializeField] private Health playerHealth;
   [SerializeField] private Image healthEmpty;
   [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        healthEmpty.fillAmount = playerHealth.startHealth/3;
    }
    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth/3;
    }
}
