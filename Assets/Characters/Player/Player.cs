using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance => instance;

    public int MaxHealth;
    public int Health;

    public Slider HealthBar;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        HealthBar.maxValue = MaxHealth;
        UpdateBar();
    }

    public void IncreaseHealth(int health)
    {
        Health += health;
        if (Health < MaxHealth)
        {
            Health = MaxHealth;
        }
        UpdateBar();
        return;
    }

    public void DecreaseHealth(int health)
    {
        Health -= health;
        if (Health <= 0)
        {
            Health = 0;
            //TODO die
        }
        UpdateBar();
    }

    private void UpdateBar()
    {
        HealthBar.value = Health;
    }

}
