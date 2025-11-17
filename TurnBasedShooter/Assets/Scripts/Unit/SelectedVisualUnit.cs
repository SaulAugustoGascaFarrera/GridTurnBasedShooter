using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedVisualUnit : MonoBehaviour
{
    private Unit unit;
    [SerializeField] private MeshRenderer meshRenderer;


    private void Awake()
    {
        unit = GetComponentInParent<Unit>();
        meshRenderer = GetComponent<MeshRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        UnitActionSystem.Instance.OnSelectedVisual += Instance_OnSelectedVisual;

        UpdateVisual();
    }

    private void Instance_OnSelectedVisual(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    // Update is called once per frame
    void UpdateVisual()
    {
        if(UnitActionSystem.Instance.GetSelectedUnit() == unit)
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
