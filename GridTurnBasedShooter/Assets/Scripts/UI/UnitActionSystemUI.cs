using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    [SerializeField] private TextMeshProUGUI actionPointsText;

    private List<ActionButtonUI> actionButtonUIList;


    private void Awake()
    {
        actionButtonUIList = new List<ActionButtonUI>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnUnitSelection += Instance_OnUnitSelection;
        UnitActionSystem.Instance.OnUnitActionChanged += Instance_OnUnitActionChanged;
        UnitActionSystem.Instance.OnActionStarted += Instance_OnActionStarted;


        TurnSystem.Instance.OnTurnChanged += Instance_OnTurnChanged;

        Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;

        UpdateActionPoints();
        CreateUnitActionButtons();
        UpdateSelectedVisual();
    }


    private void Instance_OnActionStarted(object sender, System.EventArgs e)
    {
        UpdateActionPoints();
    }

    public void CreateUnitActionButtons()
    {
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }

        actionButtonUIList.Clear();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction baseAction in selectedUnit.GetBaseActions())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);

            //ActionButtonUI actionButtonUI = actionButtonContainerTransform.GetComponent<ActionButtonUI>();

            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();


            actionButtonUI.SetBaseAction(baseAction);

            actionButtonUIList.Add(actionButtonUI);
        }
    }

  

    private void Instance_OnUnitSelection(object sender, System.EventArgs e)
    {
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }

    private void Instance_OnUnitActionChanged(object sender, System.EventArgs e)
    {

        UpdateSelectedVisual();
    }

    private void UpdateSelectedVisual()
    {
        foreach(ActionButtonUI actionButtonUI in actionButtonUIList)
        {
            actionButtonUI.UpdateSelectedVisual();
        }
    }

    private void UpdateActionPoints()
    {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        actionPointsText.text = $"Action Points: {selectedUnit.GetCurrentActionPoints()}";
    }

    private void Instance_OnTurnChanged(object sender, System.EventArgs e)
    {
        UpdateActionPoints();
    }

    private void Unit_OnAnyActionPointsChanged(object sender, System.EventArgs e)
    {
        UpdateActionPoints();
    }
}
