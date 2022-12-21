using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class DateTimeaa : MonoBehaviour
{
    // Start is called before the first frame update
    // public TextMeshProUGUI DateTimeText;
    // void Start()
    // {
    //     string time = System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm:ss");
    //     string date = System.DateTime.UtcNow.ToLocalTime().ToString("dd MMMM, yyyy");
    //     DateTimeText.text = time + "<br>" + date;
    // }



    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    // public static bool gameIsPaused;
    // public GameObject PauseCanvas;
    public TextMeshProUGUI textClock;

    void Awake() {
        // IngameCanvas.SetActive(true);
        // GameOverCanvas.SetActive(false);
        // textClock = GetComponent<TextMeshProUGUI>();
        // PauseCanvas.SetActive(false);
        
        // DateCanvas.SetActive(true);
    }

    void Start (){
        textClock = GetComponentInChildren<TextMeshProUGUI>();
        // gameIsPaused = false;
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update (){
        string time = System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm:ss");
        string date = System.DateTime.UtcNow.ToLocalTime().ToString("dd MMMM, yyyy");
        textClock.text = time + "<br>" + date;
        //  if (Input.GetKeyDown(KeyCode.Escape)) {
        //     TogglePause();

        // }

    }

    // public void TogglePause() {
    //     PauseCanvas.SetActive(!PauseCanvas.activeSelf);

    //     if (PauseCanvas.activeSelf) {
    //         gameIsPaused = true;
    //         Time.timeScale = 0f;

    //         Cursor.visible = true;
    //         Cursor.lockState = CursorLockMode.None;
    //     } else {
    //         Cursor.visible = false;
    //         Cursor.lockState = CursorLockMode.Locked;

    //         gameIsPaused = false;
    //         Time.timeScale = 1f;
    //     }
    // }

    string LeadingZero (int n){
        return n.ToString().PadLeft(2, '0');
    }
}
