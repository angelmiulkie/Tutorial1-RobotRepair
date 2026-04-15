using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    // public float CurrentHealth = 0.5f;
    private VisualElement m_Healthbar;
    public static UIHandler instance {get; private set;}

    private void Awake() {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        // healthBar.style.width = Length.Percent(CurrentHealth * 100.0f);
    }

    public void SetHealthValue(float percentage) {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }
}
