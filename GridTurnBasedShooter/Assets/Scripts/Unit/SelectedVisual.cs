using UnityEngine;

public class SelectedVisual : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Unit unitSelected;


    private void Awake()
    {
        unitSelected = GetComponentInParent<Unit>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UnitActionSystem.Instance.OnUnitSelection += Instance_OnUnitSelection;

        UpdateVisual();
    }

    private void Instance_OnUnitSelection(object sender, System.EventArgs e)
    {
       UpdateVisual();
    }


    private void UpdateVisual()
    {
        if (UnitActionSystem.Instance.GetSelectedUnit() == unitSelected)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        meshRenderer.enabled = true;
    }

    private void Hide()
    {
        meshRenderer.enabled = false;
    }
}
