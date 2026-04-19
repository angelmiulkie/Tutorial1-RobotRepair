using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    // public float CurrentHealth = 0.5f;
    private VisualElement m_Healthbar;
    public static UIHandler instance {get; private set;}

    public float displayTime = 4.0f;
    private VisualElement m_NonPlayerDialogue;
    private VisualElement m_NonPlayerFinishedDialogue;
    private VisualElement m_CurrentActiveDialogue;
    private float m_TimerDisplay;

    // Win and Lose Screens
    private VisualElement m_WinScreen;
    private VisualElement m_LoseScreen;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        // healthBar.style.width = Length.Percent(CurrentHealth * 100.0f);
        SetHealthValue(1.00f);
        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        m_NonPlayerFinishedDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCFinishedDialogue");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_NonPlayerFinishedDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;

        m_LoseScreen = uiDocument.rootVisualElement.Q<VisualElement>("LoseScreenContainer");
        m_WinScreen = uiDocument.rootVisualElement.Q<VisualElement>("WinScreenContainer");
    }

    public void SetHealthValue(float percentage) {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }

    private void Update() {
        if (m_TimerDisplay > 0) {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0) {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
                m_NonPlayerFinishedDialogue.style.display = DisplayStyle.None;
                if (m_CurrentActiveDialogue != null) {
                    m_CurrentActiveDialogue.style.display = DisplayStyle.None;
                }
            }
        }
    }

    public void DisplayDialogue() {
        m_NonPlayerFinishedDialogue.style.display = DisplayStyle.None; 
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }

    public void DisplayFinishedDialogue() {
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_NonPlayerFinishedDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }

    public void DisplayWinScreen() {
        m_WinScreen.style.opacity = 1.0f;
    }

    public void DisplayLoseScreen() {
        m_LoseScreen.style.opacity = 1.0f;
    }

    public void DisplayCustomDialogue(string elementName) {
    UIDocument uiDocument = GetComponent<UIDocument>();
    VisualElement root = uiDocument.rootVisualElement;
    
    // Find the box by the name the NPC gave us
    VisualElement customBox = root.Q<VisualElement>(elementName);
    
    if (customBox != null) {
        // Hide standard ones to prevent overlap
        if (m_CurrentActiveDialogue != null) {
            m_CurrentActiveDialogue.style.display = DisplayStyle.None;
        }

        customBox.style.display = DisplayStyle.Flex;
        m_CurrentActiveDialogue = customBox;

        m_TimerDisplay = displayTime;
    } else {
        Debug.LogWarning("UIHandler: Couldn't find a UI element named " + elementName);
    }
}
    
}
