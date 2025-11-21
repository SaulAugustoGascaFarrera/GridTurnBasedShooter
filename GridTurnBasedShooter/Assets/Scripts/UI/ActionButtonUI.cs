using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtMeshPro;
    [SerializeField] private Button button;
    [SerializeField] private GameObject selectedGameObject;

    private BaseAction baseAction;
    public void SetBaseAction(BaseAction newBaseAction)
    {
        txtMeshPro.text = newBaseAction.GetActionName();

        this.baseAction = newBaseAction;

        button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(newBaseAction);
        });
    }

    public void UpdateSelectedVisual()
    {
        BaseAction selectedBaseAction = UnitActionSystem.Instance.GetBaseAction();
        selectedGameObject.SetActive(selectedBaseAction == baseAction);
    }
}
