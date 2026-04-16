using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    EnemyController[] enemies;
    public UIHandler uiHandler;

    public static GameManager instance {get; private set;}

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
        enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
        player.OnTalkedToNPC += HandlePlayerTalkedToNPC;
    }

    // Update is called once per frame
    void Update()
    {
        // Lose Condition
        if (player.health <= 0) {
            uiHandler.DisplayLoseScreen();
            Invoke(nameof(ReloadScene), 3f);
        }

        // Win Condition
        // if (AllEnemiesFixed()) {
            // uiHandler.DisplayWinScreen();
            // Invoke(nameof(ReloadScene), 3f);
        // }
    }

    void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    bool AllEnemiesFixed() {
        foreach (EnemyController enemy in enemies) {
            if (enemy.isBroken && enemy != null) {
                return false;
            }
        }
        return true;
    }

    void HandlePlayerTalkedToNPC() {
        if (AllEnemiesFixed()) {
            uiHandler.DisplayFinishedDialogue();
            Invoke(nameof(ShowWinUI), 5f);
            Invoke(nameof(ReloadScene), 8f);
        } else {
            uiHandler.DisplayDialogue();
        }
    }

    void ShowWinUI() {
        uiHandler.DisplayWinScreen();
    }

} // END 
