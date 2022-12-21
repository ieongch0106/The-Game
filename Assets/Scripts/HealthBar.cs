using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health_bar;

    public void UpdateHealthBar(float maxHealth, float currentHealth){
        health_bar .fillAmount = currentHealth / maxHealth;
    }
}
