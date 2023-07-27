using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FindTargets : MonoBehaviour
{
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private float delay;
    private Vector3 halfExtents;
    private Collider[] targets;

    public event Action<Collider[], int> NewTargetFinded;

    private void Start()
    {
        targets = new Collider[GameSettings.MaxEnemiesCount];
        halfExtents = new Vector3((float)GameSettings.CellWidth / 2, 2, (float)GameSettings.CellHeight / 2);
        StartFind();
    }

    public void StartFind()
    {
        StartCoroutine(Find());
    }

    private IEnumerator Find()
    {
        int targetsCount = 0;
        while (targetsCount == 0)
        {
            yield return new WaitForSeconds(delay);
            targetsCount = Physics.OverlapBoxNonAlloc(GlobalConst.MapCenter, halfExtents, targets, 
                Quaternion.identity, targetMask);
        }
        NewTargetFinded?.Invoke(targets, targetsCount);
    }
}
