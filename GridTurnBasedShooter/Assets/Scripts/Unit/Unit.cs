
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] private bool isEnemy = false;

    private MoveAction moveAction;
    private SpinAction spinAction;
    private BaseAction[] baseActionsArray;
    private GridPosition unitGridPosition;

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
}
