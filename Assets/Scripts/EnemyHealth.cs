using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float value = 100;
    public Animator animator;

    private PlayerProgress playerProgress;

    public Explosion explosionPrefab;

    private void Start()
    {
        playerProgress = FindObjectOfType<PlayerProgress>(true);
    }
    public bool IsAlife() 
    { 
        return value > 0;
    }
    public void DealDamage(float damage)
    {
        playerProgress.AddExperience(damage);

        value -= damage;    
        if (value <= 0)
        {
            OnDeath();
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }

    private void OnDeath()
    {
        
        animator.SetTrigger("death");
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
    }
}
