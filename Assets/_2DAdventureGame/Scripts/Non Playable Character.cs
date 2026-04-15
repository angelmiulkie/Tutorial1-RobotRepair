using UnityEngine;

public class NonPlayableCharacter : MonoBehaviour
{
    public GameObject dialogueBubble;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueBubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
