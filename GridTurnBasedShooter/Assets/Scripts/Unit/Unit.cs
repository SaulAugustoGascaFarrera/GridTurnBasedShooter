
using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private const int ACTION_POINT_MAX = 2;

    public static event EventHandler OnAnyActionPointsChanged;

    [SerializeField] private bool isEnemy = false;

    private MoveAction moveAction;
    private SpinAction spinAction;
    private BaseAction[] baseActionsArray;
    private GridPosition unitGridPosition;
    [SerializeField] private int actionPoints = ACTION_POINT_MAX;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActionsArray = GetComponents<BaseAction>();
    }

    private void Start()
    {
        unitGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        LevelGrid.Instance.AddUnitAtGridPosition(unitGridPosition,this);

        TurnSystem.Instance.OnTurnChanged += Instance_OnTurnChanged;

    }


    private void Update()
    {
        GridPosition newtGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        if(newtGridPosition != unitGridPosition)
        {
            LevelGrid.Instance.MoveUnitStGridPosition(this,unitGridPosition,newtGridPosition);

            unitGridPosition = newtGridPosition;
        }
    }


    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public SpinAction GetSpinAction()
    {
        return spinAction;
    }

    public BaseAction[] GetBaseActions()
    {
        return baseActionsArray;
    }

    public GridPosition GetUnitGridPosition()
    {
        return unitGridPosition;
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public bool  TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if(CanSpendActionPointsToTakeAction(baseAction))
        {
            SpendActionPoints(baseAction.GetActionPointsCost());
            return true;
        }

        return false;
    }

    public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
    {
        return actionPoints >= baseAction.GetActionPointsCost();
    }

    private void SpendActionPoints(int amount)
    {
        actionPoints -= amount;

        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetCurrentActionPoints()
    {
        return actionPoints;
    }

    private void Instance_OnTurnChanged(object sender, System.EventArgs e)
    {
        actionPoints = ACTION_POINT_MAX;

        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }
}
