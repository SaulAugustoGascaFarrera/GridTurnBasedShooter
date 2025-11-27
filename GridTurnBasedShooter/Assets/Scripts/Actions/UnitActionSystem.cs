using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance {  get; private set; }

    public event EventHandler OnUnitSelection;
    public event EventHandler OnUnitActionChanged;
    public event EventHandler<bool> OnBusyStateChanged;
    public event EventHandler OnActionStarted;

    [SerializeField] private Unit unit;
    [SerializeField] private BaseAction selectedAction;
    private bool isBusy = false;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
            return;
        }

        Instance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameInput.Instance.OnActionEvent += Instance_OnActionEvent;

       SetSelectedUnit(unit);
    }

    private void Update()
    {
        
    }

    private void Instance_OnActionEvent(object sender, System.EventArgs e)
    {

        if (isBusy || !selectedAction) return;


        if(EventSystem.current.IsPointerOverGameObject())
        {
            //event to indicate if out miuse cursor is over a UI element 
            return;
        }

        if (TryGetSelectedUnit()) return;

        GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseManager.Instance.GetMousePosition());

       

        if (selectedAction.IsValidActionGridPosition(mouseGridPosition))
        {
            if(unit.TrySpendActionPointsToTakeAction(selectedAction))
            {
                SetBusy();
                selectedAction.TakeAction(mouseGridPosition, ClearBusy);
            }

            
        }

        OnActionStarted?.Invoke(this, EventArgs.Empty);

    }


    public bool TryGetSelectedUnit()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();  // 🔥 obligatorio
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPos);

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, 1 << 7))
        {
            if (hit.transform.TryGetComponent(out Unit unit))
            {
                if(unit == this.unit)
                {
                    //unit is already selected
                    return false;
                }


                if(unit.IsEnemy())
                {
                    //clicked on a Enemy Unit ,The enemy unit will work with AI 
                    return false;
                }

                SetSelectedUnit(unit);
                return true;
            }
        }

        return false;
    }


    private void SetBusy()
    {
        isBusy = true;

        OnBusyStateChanged?.Invoke(this, isBusy);
    }

    private void ClearBusy()
    {
        isBusy = false;

        OnBusyStateChanged?.Invoke(this, isBusy);
    }

    private void SetSelectedUnit(Unit selectedUnit)
    {
        unit = selectedUnit;

        // Acción por defecto
        //selectedAction = unit.GetComponent<MoveAction>();

        //selectedAction = unit.GetMoveAction();

        SetSelectedAction(unit.GetMoveAction());

        OnUnitSelection?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return unit;
    }

    public void SetSelectedAction(BaseAction newBaseAction)
    {
        selectedAction = newBaseAction;

        OnUnitActionChanged?.Invoke(this, EventArgs.Empty);
    }

    public BaseAction GetBaseAction()
    {
        return selectedAction;
    }
}
