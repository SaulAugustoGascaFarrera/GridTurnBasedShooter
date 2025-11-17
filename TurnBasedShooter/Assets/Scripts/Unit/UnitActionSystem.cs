using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UnitActionSystem : MonoBehaviour
{

    public event EventHandler OnSelectedVisual;

    public static UnitActionSystem Instance { get; private set; }

    [SerializeField] private Unit unit;
    [SerializeField] private bool isBusy = false;
    [SerializeField] private BaseAction selectedAction;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);

            return;
        }

        Instance = this;

        
    }


    private void Start()
    {
        GameInput.Instance.OnMoveAction += Instance_OnMoveAction;

        //SetSelectedUnit(unit);
    }



    private void Instance_OnMoveAction(object sender, EventArgs e)
    {
        
        if (TryGetSelectedUnit()  || isBusy) return;

        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(MouseManager.Instance.GetMousePosition());

       
        if(LevelGrid.Instance.IsValidGridSystemPosition(gridPosition))
        {
           
            SetBusy();
            selectedAction.TakeAction(gridPosition,ClearBusy);
        }
    }


    public bool TryGetSelectedUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out RaycastHit hit,float.MaxValue,1 << 6))
        {

            if (hit.transform.gameObject.TryGetComponent(out Unit selectedUnit))
            {
                SetSelectedUnit(selectedUnit);

                return true;

                
            }
           
        }


        return false;
    }


    private void SetSelectedUnit(Unit newUnit)
    {
        unit = newUnit;

        //selectedAction = newUnit.GetComponent<MoveAction>();

        OnSelectedVisual?.Invoke(this,EventArgs.Empty);


        
    }

    public Unit GetSelectedUnit()
    {
        return unit;
    }

    private void SetBusy()
    {
        isBusy = true;
    }

    private void ClearBusy()
    {
        isBusy = false;
    }

    public void SetSelectedAction(BaseAction newBaseAction)
    {
        this.selectedAction = newBaseAction;
    }

    public BaseAction GetBaseSAction()
    {
        return selectedAction;
    }


}
