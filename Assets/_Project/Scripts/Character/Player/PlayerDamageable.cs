using BackpackTeleport.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerDamageable : BaseDamageable
{
    [SerializeField] private Image energyBar;
    private const float HEALTH_BAR_INCREMENT_SIZE = 0.14f;
    private const float ENERGY_BAR_INCREMENT_SIZE = 0.5f;

    private float maxEnergy = 2;
    private float currentEnergy;

    protected override void Start()
    {
        base.Start();
        currentEnergy = maxEnergy;
        UpdateEnergyBar(currentEnergy);
    }

    private void Update()
    {
        if(Keyboard.current.mKey.wasPressedThisFrame)
        {
            AddHealth(1f);
            //AddEnergy(1f);
        }

        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            RecalculateHealth(1f);
            //RemoveEnergy(1f);
        }
    }

    public override void UpdateHealthbar(float newHealth)
    {
        if (healthBar != null)
        {
            float ratio = newHealth / maxHealth;
            float ratioInt = (ratio * 10);

            Debug.Log(ratioInt);
            healthBar.fillAmount = HEALTH_BAR_INCREMENT_SIZE * ratioInt;
        }
    }

    public void UpdateEnergyBar(float newEnergy)
    {
        if (energyBar != null)
        {
            float ratio = newEnergy / maxEnergy;
            energyBar.fillAmount = ratio;
        }
    }

    public void AddEnergy(float amount)
    {
        float newEnergy = currentEnergy + amount;
        currentEnergy = newEnergy;
        UpdateEnergyBar(currentEnergy);

        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
            UpdateHealthbar(currentEnergy);
        }
    }

    public void RemoveEnergy(float amount)
    {
        float newEnergy = currentEnergy - amount;
        currentEnergy = newEnergy;
        UpdateHealthbar(currentEnergy);

        if (currentEnergy < maxEnergy)
        {
            currentEnergy = maxEnergy;
            UpdateHealthbar(currentEnergy);
        }
    }
}
