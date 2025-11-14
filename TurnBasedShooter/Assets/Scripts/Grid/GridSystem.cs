using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GridSystem Instance {  get; private set; }

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;

    private GridObject[,] gridObjectArray;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
            return;
        }

        Instance = this;
    }

    public GridSystem(int width,int height,float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectArray = new GridObject[width,height];

        for(int x=0;x<width;x++)
        {
            for(int z=0;z<height;z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                gridObjectArray[x, z] = new GridObject(this, gridPosition);

                //Debug.DrawLine(GetWorldPosition(gridPosition), GetWorldPosition(gridPosition) + Vector3.right * 0.5f, Color.green, 9999.0f);


            }
        }

        
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }
    

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x,0.0f,gridPosition.z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(Mathf.RoundToInt(worldPosition.x / cellSize),Mathf.RoundToInt(worldPosition.z / cellSize));
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

    public void SetGridObjectDebug(Transform gridDebugTransform)
    {
        for(int x=0; x < width; x++)
        {
            for(int z=0;z<height;z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                Transform gridDebug = Instantiate(gridDebugTransform, GetWorldPosition(gridPosition), Quaternion.identity);

                GridDebugObject gridDebugObject = gridDebug.GetComponent<GridDebugObject>();

                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }

        
    }

}
