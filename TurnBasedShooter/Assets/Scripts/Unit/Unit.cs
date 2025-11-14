using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public static Unit Instance { get; private set; }

    private GridPosition unitGridPosition;


    private void Awake()
    {
        //if(Instance != null)
        //{
        //    Destroy(Instance);
        //    return;
        //}

        //Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //GameInput.Instance.OnMoveAction += Instance_OnMoveAction;

        unitGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        LevelGrid.Instance.AddUnitAtGridPosition(this, unitGridPosition);
    }

   

    // Update is called once per frame
    void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        if(newGridPosition != unitGridPosition)
        {
            LevelGrid.Instance.MovedAtGridPosition(this, unitGridPosition, newGridPosition);

            unitGridPosition = newGridPosition;
        }
    }

    
}
