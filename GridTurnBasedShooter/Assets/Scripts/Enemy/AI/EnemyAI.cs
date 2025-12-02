using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float timer = 0.0f;

    private void Start()
    {
        TurnSystem.Instance.OnTurnChanged += Instance_OnTurnChanged;
    }

    private void Instance_OnTurnChanged(object sender, System.EventArgs e)
    {
        timer = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }

        timer -= Time.deltaTime;

        if(timer <= 0.0f)
        {
            TurnSystem.Instance.NextTurn();
        }
    }
}
