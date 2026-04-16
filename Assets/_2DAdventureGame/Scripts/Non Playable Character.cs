using UnityEngine;
using UnityEngine.UIElements;

public class NonPlayableCharacter : MonoBehaviour
{
    public GameObject dialogueBubble;
    public bool isQuestGiver = false;

    public string customeDialogueName = "NPC1Dialogue";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(dialogueBubble != null) {
            dialogueBubble.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Talk() {
        UIHandler.instance.DisplayCustomDialogue(customeDialogueName);
    }
}
