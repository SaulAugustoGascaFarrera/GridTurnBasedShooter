using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 7.0f;
    [SerializeField] private float stopDistance = 0.2f;
    [SerializeField] private int maxDistanceToMove = 3;
    private Vector3 targetPosition;

    public override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
        
    }

    private void Update()
    {
        if (!isActive) return;

        Vector3 movementDirection = (targetPosition - transform.position).normalized;

        if(Vector3.Distance(targetPosition,transform.position) > stopDistance)
        {
            transform.position += movementDirection * movementSpeed * Time.deltaTime;
        }
        else
        {
            isActive = false;
            onActionComplete?.Invoke();
        }


        transform.forward += Vector3.Slerp(transform.forward, movementDirection, rotationSpeed * Time.deltaTime);
    }

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

    public override List<GridPosition> GetValidActionAtGridPosition()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetUnitGridPosition();

        for(int x=-maxDistanceToMove;x<=maxDistanceToMove;x++)
        {
            for(int z=-maxDistanceToMove;z<=maxDistanceToMove;z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x,z);

                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if(!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if(LevelGrid.Instance.HasAnyUnitAtGridPosition(testGridPosition))
                {
                    continue;
                }

                if(unitGridPosition == testGridPosition)
                {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }


        return validGridPositionList;
    }
}
