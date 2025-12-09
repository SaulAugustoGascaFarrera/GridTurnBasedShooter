using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{

    public event EventHandler OnShoot;

    private enum State
    {
        Aiming,
        Shooting,
        Cooloff
    }


    //private float totalSpinAmount = 0.0f;
    private State state;    
    [SerializeField] private int maxShootDistance = 7;
    private float stateTimer = 0.0f;

    private Unit targetUnit;
    private bool canShootBullet = false;


    private void Update()
    {
        if (!isActive) return;

        //float spinAddAmount = 360.0f * Time.deltaTime;
        //transform.eulerAngles += new Vector3(0.0f, spinAddAmount, 0.0f);

        //totalSpinAmount += spinAddAmount;

        //if(totalSpinAmount >= 360.0f)
        //{
        //    isActive = false;
        //    onActionComplete?.Invoke();
        //    //spinAddAmount = 0.0f;
        //}

        stateTimer -= Time.deltaTime;

        switch(state)
        {
            case State.Aiming:
                Vector3 aimingDirection  = targetUnit.GetWorldPositoion() - unit.GetWorldPositoion();
                float rotationSpeed = 10.0f;
                transform.forward = Vector3.Lerp(transform.forward, aimingDirection.normalized , Time.deltaTime * rotationSpeed);
                break;
            case State.Shooting:
                if(canShootBullet)
                {
                    Shoot();
                    canShootBullet = false;
                    
                }
                break;
            case State.Cooloff:
                break;
            default:
                break;
        }

        if(stateTimer <= 0.0f)
        {
            NextState();
        }
        

    }

    private void NextState()
    {
       

        switch (state)
        {
            case State.Aiming:
                state = State.Shooting;
                float shootingStateTime = 0.1f;
                stateTimer = shootingStateTime;
                break;
            case State.Shooting:
                state = State.Cooloff;
                float coolOffStateTime = 0.5f;
                stateTimer = coolOffStateTime;
                break;
            case State.Cooloff:
                ActionComplete();
                break;
            default: break;

        }

        //Debug.Log(state);
    }

    private void Shoot()
    {
        targetUnit.Damage();
        OnShoot?.Invoke(this, EventArgs.Empty);
    }

    public override string GetActionName()
    {
        return "Shoot";
    }

    public override List<GridPosition> GetValidActionAtGridPosition()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetUnitGridPosition();

        for (int x = -maxShootDistance; x <= maxShootDistance;x++)
        {
            for(int z = -maxShootDistance;z <= maxShootDistance;z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x,z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if(!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);

                if(testDistance > maxShootDistance)
                {
                    continue;
                }

                //validGridPositionList.Add(testGridPosition);
                //continue;

                if(unitGridPosition == testGridPosition)
                {
                    continue;
                }

                if(!LevelGrid.Instance.HasAnyUnitAtGridPosition(testGridPosition))
                {
                    //grid position is  empty,no unit
                    continue;
                }

                Unit targetUnit =  LevelGrid.Instance.GeUnitAtGridPosition(testGridPosition);

                if(targetUnit.IsEnemy() == unit.IsEnemy())
                {
                    //both units on same 'team'
                    continue;
                }


                validGridPositionList.Add(testGridPosition);
            }
        }



        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);

        targetUnit = LevelGrid.Instance.GeUnitAtGridPosition(gridPosition);

        //Debug.Log("Aiming");

        state  = State.Aiming;
 
        float aimingStateTime = 1.0f;
        stateTimer = aimingStateTime;

        canShootBullet = true;
    }
}
