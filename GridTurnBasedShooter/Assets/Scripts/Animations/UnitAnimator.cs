using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string SHOOT = "Shoot";

    private Animator animator;
    

    private void Awake()
    {
        
        animator = GetComponentInChildren<Animator>();
        

        if(TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoving += MoveAction_OnStartMoving;

            moveAction.OnStopMoving += MoveAction_OnStopMoving;
        }

        if(TryGetComponent<ShootAction>(out ShootAction shootAction))
        {
            shootAction.OnShoot += ShootAction_OnShoot;
        }
    }

    private void ShootAction_OnShoot(object sender, System.EventArgs e)
    {
        animator.SetTrigger(SHOOT);
    }

    private void MoveAction_OnStartMoving(object sender, System.EventArgs e)
    {
        animator.SetBool(IS_WALKING, true);
    }


    private void MoveAction_OnStopMoving(object sender, System.EventArgs e)
    {
        animator.SetBool(IS_WALKING, false);
    }

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
