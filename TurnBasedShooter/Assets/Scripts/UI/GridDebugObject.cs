using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro txtMeshPro;
    private GridObject gridObject;

    public void SetGridObject(GridObject newGridObject)
    {
        this.gridObject = newGridObject;
    }


    private void Update()
    {
        txtMeshPro.text = gridObject.ToString();
    }

}
