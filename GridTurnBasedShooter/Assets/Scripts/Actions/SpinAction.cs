using System;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    [SerializeField] private float totalSpinAmount = 0.0f;

    [SerializeField] private float spinAddAmount = 0.0f;
    public override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if(!isActive)
        {
            return;
        }

        spinAddAmount = 360.0f * Time.deltaTime;

        transform.eulerAngles += new Vector3(0.0f, spinAddAmount, 0.0f);

        //Debug.Log(spinAddAmount);

        totalSpinAmount += spinAddAmount;

        if(totalSpinAmount >= 360.0f)
        {
           
            //spinAddAmount = 0.0f;
            isActive = false;
            onActionComplete?.Invoke();
        }
    }

    public override string GetActionName()
    {
        return "Spin";
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;
        totalSpinAmount = 0.0f;
    }

    public override List<GridPosition> GetValidActionAtGridPosition()
    {
        //List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetUnitGridPosition();

        return new List<GridPosition>
        {
            unitGridPosition
        };
    }

    public override int GetActionPointsCost()
    {
        return 2;
    }
}
