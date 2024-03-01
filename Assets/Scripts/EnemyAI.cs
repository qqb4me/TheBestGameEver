using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float viewAngel;
    public float damage = 30;
    public List<Transform> patrolPoints;
    public PlayerController player;

    private NavMeshAgent _navMashAgent;
    private bool _isPlayerNoticed;
    private PlayerHealth _playerHealth;

    
    private void Start()
    {
        InitComponentLinks();
        PickNewParolPoint();
    }

    private void InitComponentLinks()
    {
        _navMashAgent = GetComponent<NavMeshAgent>();
        _playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        NoticePlayerUpdate();
        ChaceUpdate();
        AttackUpdate();
        PatrolUpdate();
    }

    private void AttackUpdate()
    {
        if (_isPlayerNoticed)
        {
            if (_navMashAgent.remainingDistance <= _navMashAgent.stoppingDistance)
            {
                _playerHealth.DealDamage(damage * Time.deltaTime);
            }
        }
    }

    private void NoticePlayerUpdate()
    {
        var direction = player.transform.position - transform.position;
        _isPlayerNoticed = false;
        if (Vector3.Angle(transform.forward, direction) < viewAngel)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    _isPlayerNoticed = true;
                }
            }
        }
    }

    private void PatrolUpdate()
    {
        if (!_isPlayerNoticed)
        {
            if (_navMashAgent.remainingDistance <= _navMashAgent.stoppingDistance)
            {
                PickNewParolPoint();
            }
        }
    }

    private void PickNewParolPoint()
    {
        _navMashAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
    }

    private void ChaceUpdate()
    {
        if (_isPlayerNoticed)
        {
            _navMashAgent.destination = player.transform.position;
        }
    }
}
