using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    [SerializeField] private Transform gridDebugObject;
    private GridSystem gridSystem;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }


        Instance = this;

        gridSystem = new GridSystem(10, 10, 2);

        gridSystem.SetGridObjectDebug(gridDebugObject);
    }

    public int GetWidth() => gridSystem.GetWidth();

    public int GetHeight() => gridSystem.GetHeight();

    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

    public GridObject GetGridObject(GridPosition gridPosition) => gridSystem.GetGridObject(gridPosition);

    public bool IsValidGridSystemPosition(GridPosition gridPosition) => gridSystem.IsValidGridSystemPosition(gridPosition);

    public void AddUnitAtGridPosition(Unit unit, GridPosition gridPosition)
    {
        GridObject gridObject = GetGridObject(gridPosition);

        gridObject.AddUnit(unit);

    }

    public void RemoveUnitAtGridPosition(Unit unit,GridPosition gridPosition)
    {
        GridObject gridObject = GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public bool HasAnyUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }

    public void MovedAtGridPosition(Unit unit,GridPosition fromGridPosition,GridPosition toGridPosition)
    {
        AddUnitAtGridPosition(unit, toGridPosition);
        RemoveUnitAtGridPosition(unit, fromGridPosition);
    }

}
