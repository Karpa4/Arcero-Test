using UnityEngine;
using System;
using System.Collections;

public class TargetsChecker : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private FindTargets findTargets;
    [SerializeField] private float range;
    [SerializeField] private float delay;
    private Collider[] targets;
    private bool targetsDestroy;
    private bool targetFind;

    public Transform mainTarget;
    public event Action LevelCleared;

    private void Start()
    {
        targets = new Collider[GameSettings.MaxEnemiesCount];
        findTargets.NewTargetFinded += StartCheck;
    }

    private void StartCheck(Collider[] targets, int count)
    {
        this.targets = targets;
        StartCoroutine(Checking(count));
    }

    private IEnumerator Checking(int count)
    {
        targetsDestroy = false;
        while (!targetsDestroy)
        {
            float distance = float.MaxValue;
            targetFind = false;
            targetsDestroy = true;
            for (int i = 0; i < count; i++)
            {
                if (targets[i] != null)
                {
                    targetsDestroy = false;
                    RaycastHit hitInfo;
                    Vector3 dir = targets[i].transform.position - transform.position;
                    Physics.Raycast(transform.position, dir, out hitInfo, range, mask);
                    if (true)
                    {
                        if (hitInfo.collider == targets[i] && hitInfo.distance < distance)
                        {
                            distance = hitInfo.distance;
                            targetFind = true;
                            mainTarget = targets[i].transform;
                        }
                    }
                }
            }

            if (!targetFind && !targetsDestroy)
            {
                mainTarget = null;
            }

            if (targetsDestroy)
            {
                findTargets.StartFind();
                mainTarget = null;
                LevelCleared?.Invoke();
            }

            yield return new WaitForSeconds(delay);
        }
    }

    private void OnDestroy()
    {
        findTargets.NewTargetFinded -= StartCheck;
    }
}
