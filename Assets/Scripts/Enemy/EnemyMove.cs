using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float delay;

    private float halfWidth;
    private float halfHeight;

    private void Start()
    {
        
        halfWidth = (float)GameSettings.CellWidth / 2;
        halfHeight = (float)GameSettings.CellHeight / 2;
        agent.SetDestination(GetNewPoint());
        StartCoroutine(CheckPathComplete());
    }

    private Vector3 GetNewPoint()
    {
        Vector3 result = Vector3.zero;
        result.x = Random.Range(-halfWidth, halfWidth);
        result.z = Random.Range(-halfHeight, halfHeight);
        return result;
    }

    private IEnumerator CheckPathComplete()
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                agent.SetDestination(GetNewPoint());
            }
        }
    }
}
