using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] private float movementSpeed = 7.5f;
    [SerializeField] private float rotationSpeed = 7.0f;
    [SerializeField] private float distanceToMove = 0.2f;
    private Vector3 targetPosition;
    public override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (!isActive)
        {
            
            return;
        }

        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        if(Vector3.Distance(targetPosition,transform.position) > distanceToMove )
        {
            transform.position += moveDirection * movementSpeed * Time.deltaTime;
        }
        else
        {
        
            isActive = false;
            onActionComplete?.Invoke();

        }

        transform.forward += Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
    }


    //public void Move(GridPosition gridPosition)
    //{
    //    targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
    //    isActive = true;
    //}

    public override string GetActionName()
    {
        return "Move";
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        isActive = true;
        this.onActionComplete = onActionComplete;
    }
}
