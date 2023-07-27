using System;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    private Vector3 startPlayerPoint;

    public event Action LevelIsDone;

    public void Init(Vector3 startPlayerPoint)
    {
        this.startPlayerPoint = startPlayerPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMove>(out PlayerMove move))
        {
            move.transform.position = startPlayerPoint;
            LevelIsDone?.Invoke();
        }
    }
}
