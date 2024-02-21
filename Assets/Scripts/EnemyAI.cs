using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float viewAngel;

    public List<Transform> patrolPoints;

    public PlayerController player;

    private NavMeshAgent _navMashAgent;
    private bool _isPlayerNoticed;

    // Start is called before the first frame update
    void Start()
    {
        _navMashAgent = GetComponent<NavMeshAgent>();

        PickNewParolPoint();

    }

    private void Update()
    {
        NoticePlayerUpdate();

        ChaceUpdate();

        PatrolUpdate();
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
            if (_navMashAgent.remainingDistance == 0)
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
