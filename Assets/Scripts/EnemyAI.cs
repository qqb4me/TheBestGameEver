using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float viewAngel;
    public float damage = 30;
    public float attackDistance = 1f;
    public List<Transform> patrolPoints;
    public Animator animator;

    private NavMeshAgent _navMashAgent;
    private bool _isPlayerNoticed;
    private PlayerHealth _playerHealth;
    private EnemyHealth _enemyHealth;
    private PlayerController _player;


    private void Start()
    {
        InitComponentLinks();
        PickNewParolPoint();
    }

    private void InitComponentLinks()
    {
        _navMashAgent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<PlayerController>(true);
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        NoticePlayerUpdate();
        ChaceUpdate();
        AttackUpdate();
        PatrolUpdate();
    }

    public bool IsAlife()
    {
        return _enemyHealth.IsAlife();
    }

    private void AttackUpdate()
    {
        if (_isPlayerNoticed)
        {
            if (_navMashAgent.remainingDistance <= _navMashAgent.stoppingDistance)
            {
                animator.SetTrigger("attack");
            }
        }
    }

    public void AttackDamage()
    {
        if (!_isPlayerNoticed) return;
        if (_navMashAgent.remainingDistance > (_navMashAgent.stoppingDistance + attackDistance)) return;
        _playerHealth.DealDamage(damage);
    }

    private void NoticePlayerUpdate()
    {
        _isPlayerNoticed = false;
        if (!_playerHealth.IsAlife()) return;

        var direction = _player.transform.position - transform.position;

        if (Vector3.Angle(transform.forward, direction) < viewAngel)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
            {
                if (hit.collider.gameObject == _player.gameObject)
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
            _navMashAgent.destination = _player.transform.position;
        }
    }
}
