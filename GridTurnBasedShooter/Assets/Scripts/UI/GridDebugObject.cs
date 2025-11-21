using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    private GridObject gridObject;
    [SerializeField] private TextMeshPro txtMesh;

    public void SetGridObject(GridObject newGridObject)
    {
        this.gridObject = newGridObject;
    }

    private void Update()
    {
        txtMesh.text = gridObject.ToString();
    }

}
