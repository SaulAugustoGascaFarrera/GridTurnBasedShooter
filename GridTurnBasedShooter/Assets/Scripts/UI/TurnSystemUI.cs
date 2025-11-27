using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{

    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI turnNumberText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        TurnSystem.Instance.OnTurnChanged += Instance_OnTurnChanged;

        endTurnButton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
        });

        UpdateTurnText();
    }

    private void Instance_OnTurnChanged(object sender, System.EventArgs e)
    {
        UpdateTurnText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateTurnText()
    {
        turnNumberText.text = $"TURN :{TurnSystem.Instance.GetTurnNumber()}";
    }
}
