
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    [SerializeField] private List<Unit> unitListArray;

    public GridObject(GridSystem gridSystem,GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;

        unitListArray = new List<Unit>();
    }


    public override string ToString()
    {
        string unitString = string.Empty;

        if(HasAnyUnit())
        {
            foreach(Unit unit in unitListArray)
            {
                unitString += unit.ToString() + "\n";

                return gridPosition.ToString() + "\n" + unitString + "\n";
            }
        }


        return gridPosition.ToString();
    }

    public void AddUnit(Unit unit)
    {
        unitListArray.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        unitListArray.Remove(unit);
    }

    public List<Unit> GetUnitList()
    {
        return unitListArray;
    }

    public bool HasAnyUnit()
    {
        return unitListArray.Count > 0;
    }
}
