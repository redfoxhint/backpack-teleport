using BackpackTeleport.Character;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;
    [SerializeField] private CharacterBase characterBase;
    [SerializeField] private BaseDamageable baseDamageable;
    [SerializeField] private Rigidbody2D rBody2D;
    [SerializeField] private ParticleSystem stunnedParticleSystem;
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private float stunTime = 2f;

    private float currentStunTime = 0;

    Task damageRoutine;

    // Start is called before the first frame update
    void Start()
    {
        baseDamageable = GetComponent<BaseDamageable>();
        rBody2D = GetComponent<Rigidbody2D>();
        baseDamageable.onTookDamage += OnTookDamage;
        baseDamageable.onDie += OnDie;
        stunnedParticleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        characterBase.SetAnimatorParameters(aiPath.velocity);

        if(CanChase())
        {
            aiPath.enabled = true;
        }
        else
        {
            aiPath.enabled = false;
        }
    }

    private void OnTookDamage()
    {
        if (damageRoutine == null || !damageRoutine.Running)
        {
            damageRoutine = new Task(DamageRoutine());
            currentStunTime = stunTime;
            aiPath.enabled = false;
            stunnedParticleSystem.Play();
            rBody2D.velocity = Vector2.zero;
        }
        else
        {
            currentStunTime = stunTime;
        }
    }

    private IEnumerator DamageRoutine()
    {
        while (currentStunTime > 0)
        {
            currentStunTime -= Time.deltaTime;
            yield return null;
        }

        aiPath.enabled = true;
        stunnedParticleSystem.Stop();

        yield break;
    }

    private void OnDie()
    {
        if(damageRoutine != null || damageRoutine.Running)
        {
            damageRoutine.Stop();
            damageRoutine = null;
        }
    }

    private bool CanChase()
    {
        return Vector2.Distance(transform.position, aiPath.destination) < attackRange && currentStunTime <= 0;
    }
}
