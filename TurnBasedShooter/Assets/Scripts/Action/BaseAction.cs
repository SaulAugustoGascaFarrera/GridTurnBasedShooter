using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{

    protected Unit unit;

    [SerializeField] protected bool isActive = false;

    protected Action onActionComplete;

    public virtual void Awake()
    {
        unit = GetComponent<Unit>();    
    }

    public abstract string GetActionName();

    public abstract void TakeAction(GridPosition gridPosition,Action onActionComplete);

    
}
