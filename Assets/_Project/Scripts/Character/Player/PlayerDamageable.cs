using BackpackTeleport.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerDamageable : BaseDamageable
{
    [SerializeField] private Image energyBar;

    private float maxEnergy = 2;
    private float currentEnergy;

    protected override void Start()
    {
        base.Start();
        currentEnergy = maxEnergy;
        UpdateStatBar(energyBar, currentEnergy, maxEnergy);
    }

    private void Update()
    {
        if(Keyboard.current.vKey.wasPressedThisFrame)
        {
            SetHealth(3f);
        }

        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            TakeDamage(this.transform, 1f);
        }

        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            AddEnergy(1f);
        }

        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            RemoveEnergy(1f);
        }
    }

    public override void TakeDamage(Transform damageDealer, float amount)
    {
        base.TakeDamage(damageDealer, amount);
        Utils.ShakeCameraPosition(Camera.main.transform, 0.6f, 0.11f, 15, 0f, false);
    }

    public void AddEnergy(float amount)
    {
        float newEnergy = currentEnergy + amount;
        currentEnergy = newEnergy;

        if(currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }

        UpdateStatBar(energyBar, currentEnergy, maxEnergy);
    }

    public void RemoveEnergy(float amount)
    {
        float newEnergy = currentEnergy - amount;
        currentEnergy = newEnergy;

        if(currentEnergy <= 0)
        {
            currentEnergy = 0;
        }

        UpdateStatBar(energyBar, currentEnergy, maxEnergy);
    }

    public void IncreaseMaxEnergy(float amount)
    {
        float newMax = maxEnergy + amount;
        maxEnergy = newMax;

        UpdateStatBar(energyBar, currentEnergy, maxEnergy);
    }
}
