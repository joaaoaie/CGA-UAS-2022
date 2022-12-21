using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    // public static bool gameIsOver;
    public static bool gameIsPaused;
    public float health = 100.0f;
    private const float coef = 1f;
    private float timer ;

    [Header("Menu Objects")]
    // public GameObject IngameCanvas;
    public TextMeshProUGUI OxygenText;
    public TextMeshProUGUI textClock;
    public GameObject PauseCanvas;
    // public GameObject DateCanvas;


    // [Header("Essentials")]
    // // public SceneFader sceneFader;
    // // public DeathEffect deathEffect;
    // public string mainMenu;

    // [Header("Entity")]
    // public MainCharacter mC;
    
    void Awake() {
        // IngameCanvas.SetActive(true);
        // GameOverCanvas.SetActive(false);
        // textClock = GetComponent<TextMeshProUGUI>();
        PauseCanvas.SetActive(false);
        // DateCanvas.SetActive(false);
        
        // DateCanvas.SetActive(true);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        OxygenText = GameObject.Find("CoX").GetComponentInChildren<TextMeshProUGUI>();
        textClock = GameObject.Find("CDate").GetComponentInChildren<TextMeshProUGUI>();
        
        // gameIsOver = false;
        gameIsPaused = false;
        

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // if (gameIsOver) {
        //     return;
        // }
        

        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
        if(!gameIsPaused){
            
            GameTime();
            HealthOxy();
        }
        // if (mC.currentHealth <= 0) {
        //     deathEffect.playerIsDead();
        //     Invoke("callGameOver", 3f);
        // }
    }

    public void GameTime()
    {
        string time = System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm:ss");
        string date = System.DateTime.UtcNow.ToLocalTime().ToString("dd MMMM, yyyy");
        textClock.text = time + "<br>" + date;
    }

    public void HealthOxy()
    {
        timer = timer + Time.deltaTime;
        if(timer >= 5)
        {
            health -= coef;
            OxygenText.text = "Oxygen: %" + health;
            timer = 0;
        }
        
    }
    

    


    string LeadingZero (int n){
        return n.ToString().PadLeft(2, '0');
    }

    void EndGame() {
        Time.timeScale = 0f;
    }
    
    // public void RestartGame() {
    //     GameOverCanvas.SetActive(false);
    //     Time.timeScale = 1f;

    //     EnemyChar.numOfEnemies = 0;
    //     sceneFader.FadeTo(SceneManager.GetActiveScene().name);

    //     Cursor.visible = false;
    //     Cursor.lockState = CursorLockMode.Locked;
    // }

    // public void callGameOver() {
    //     // gameIsOver = true;
    //     // GameOverCanvas.SetActive(true);

    //     EndGame();

    //     Cursor.visible = true;
    //     Cursor.lockState = CursorLockMode.None;
    // }

    public void TogglePause() {
        PauseCanvas.SetActive(!PauseCanvas.activeSelf);
        
        if (PauseCanvas.activeSelf) {
            gameIsPaused = true;
            Time.timeScale = 0f;
            Time.fixedDeltaTime = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            gameIsPaused = false;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 1f;
        }
    }

    // public void goToMenu() {
    //     Time.timeScale = 1f;
    //     sceneFader.FadeTo(mainMenu);
    // }
}
