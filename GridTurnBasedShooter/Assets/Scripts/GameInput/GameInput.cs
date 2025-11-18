using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnActionEvent;
    public static GameInput Instance { get; private set; }

    private InputManager inputManager;

    private void Awake()
    {
        if(Instance == null)
        {
            Destroy(Instance);
            return;
        }

        Instance = this;

        inputManager = new InputManager();

        inputManager.Player.Enable();

        
       
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputManager.Player.Action.performed += Action_performed;
    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
        OnActionEvent?.Invoke(this,EventArgs.Empty);
    }

   
}
