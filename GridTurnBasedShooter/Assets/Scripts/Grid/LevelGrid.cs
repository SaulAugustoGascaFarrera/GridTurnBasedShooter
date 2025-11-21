using NUnit.Framework;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    private GridSystem gridSystem;

    [SerializeField] private Transform gridObjectDebugTransform;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
            return;
        }

        Instance = this;

        gridSystem = new GridSystem(10, 10, 2.0f);

        gridSystem.CreateDebugObject(gridObjectDebugTransform);
    }

    public int GetWidth() => gridSystem.GetWidth();

    public int GetHeight() => gridSystem.GetHeight();

    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

    public GridObject GetGridObject(GridPosition gridPosition) => gridSystem.GetGridObject(gridPosition);

    public void AddUnitAtGridPosition(GridPosition gridPosition,Unit unit)
    {
        GridObject  gridObject = GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public bool HasAnyUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = GetGridObject(gridPosition);

        return gridObject.HasAnyUnit();
    }

    public void MoveUnitStGridPosition(Unit unit,GridPosition fromGridPosition,GridPosition toGridPosition)
    {
        AddUnitAtGridPosition(toGridPosition, unit);

        RemoveUnitAtGridPosition(fromGridPosition, unit);
    }
}
