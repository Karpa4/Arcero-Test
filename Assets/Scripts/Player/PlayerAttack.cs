using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : BaseAttack
{
    [SerializeField] private PlayerMove playerMove;

    protected override void Start()
    {
        playerMove.IsMoved += ChangeState;
        base.Start();
    }

    private void ChangeState(bool isMoving)
    {
        this.isMoving = isMoving;
    }

    private void OnDestroy()
    {
        playerMove.IsMoved -= ChangeState;
    }
}
