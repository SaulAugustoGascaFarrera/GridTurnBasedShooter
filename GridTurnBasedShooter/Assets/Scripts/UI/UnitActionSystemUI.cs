using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;

    private List<ActionButtonUI> actionButtonUIList;


    private void Awake()
    {
        actionButtonUIList = new List<ActionButtonUI>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnUnitSelection += Instance_OnUnitSelection;
        UnitActionSystem.Instance.OnUnitActionChanged += Instance_OnUnitActionChanged;

        CreateUnitActionButtons();
        UpdateSelectedVisual();
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
}
