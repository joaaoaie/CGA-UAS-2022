using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Interaction : MonoBehaviour {
    [SerializeField]
    private int oxygen = 80;

    private Animator playerAnim, doorAnim, hatchAnim;
    private Button btnGenerator, btnDoor, closePanel;
    private Image controlPanel;
    private TextMeshProUGUI uiInteract, uiTimeDate, uiOxygen, uiPaused;
    private AudioSource[] sounds;
    private Collider other;

    private string isInteracting = null;
    private bool generatorStatus = true, accessGranted = false, unlockLobby = false;
    private string[] tags = {"consolePanel", "computer", "card", "oxygenChamber"};
    
    private void Awake() {
        GameObject[] lampu = GameObject.FindGameObjectsWithTag ("Lampu");
        Light pointLight = GameObject.Find("Point Light").GetComponent<Light>();

        sounds = new AudioSource[]{
            GameObject.Find("Sound Effect/panel").GetComponent<AudioSource>(),
            GameObject.Find("Sound Effect/on").GetComponent<AudioSource>(),
            GameObject.Find("Sound Effect/off").GetComponent<AudioSource>(),
            GameObject.Find("Sound Effect/open").GetComponent<AudioSource>()
        };

        playerAnim = this.GetComponent<Animator>();

        uiInteract = GameObject.Find("interactDialog").GetComponent<TextMeshProUGUI>();
        uiTimeDate = GameObject.Find("timeDialog").GetComponent<TextMeshProUGUI>();
        uiOxygen = GameObject.Find("oxygenDialog").GetComponent<TextMeshProUGUI>();
        uiPaused = GameObject.Find("pauseDialog").GetComponent<TextMeshProUGUI>();

        controlPanel = GameObject.Find("controlPanel").GetComponent<Image>();

        btnGenerator = controlPanel.transform.Find("GeneratorButton").GetComponent<Button>();
        btnDoor = controlPanel.transform.Find("UnlockDoor").GetComponent<Button>();
        closePanel = controlPanel.transform.Find("Close").GetComponent<Button>();

        oxygen++;
        InvokeRepeating("oxygenDecrease", 1.0f, 5.0f);

        btnGenerator.onClick.AddListener(() => {
            foreach (AudioSource sound in sounds) { sound.Stop(); }
            if(generatorStatus) {
                btnGenerator.GetComponentInChildren<TextMeshProUGUI>().text = "Turn On Generator";
                generatorStatus = false;
                sounds[2].Play(0);
            } else {
                btnGenerator.GetComponentInChildren<TextMeshProUGUI>().text = "Turn Off Generator";
                generatorStatus = true;
                sounds[1].Play(0);
            }
            foreach (GameObject i in lampu){ i.SetActive(generatorStatus); }
        });

        btnDoor.onClick.AddListener(() => {
            foreach (AudioSource sound in sounds) { sound.Stop(); }
            if(unlockLobby) {
                btnDoor.GetComponentInChildren<TextMeshProUGUI>().text = "Unlock Door";
                unlockLobby = false;
                pointLight.color = Color.red;
            } else {
                btnDoor.GetComponentInChildren<TextMeshProUGUI>().text = "Lock Door";
                unlockLobby = true;
                pointLight.color = Color.green;
            }
            sounds[3].Play(0);
        });

        closePanel.onClick.AddListener(() => {
            foreach (AudioSource sound in sounds) { sound.Stop(); }
            controlPanel.gameObject.SetActive(false);
        });

        uiInteract.gameObject.SetActive(false);
        controlPanel.gameObject.SetActive(false);
        uiPaused.gameObject.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (Time.timeScale == 1) {
                Time.timeScale = 0;
                uiPaused.gameObject.SetActive(true);
            } else {
                Time.timeScale = 1;
                uiPaused.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyUp(KeyCode.E)) {
            if (isInteracting == tags[2]) {
                uiInteract.gameObject.SetActive(false);
                accessGranted = true;
                Destroy(other.gameObject);
                other = null;
                isInteracting = null;
            } else
            if (isInteracting == tags[1] && accessGranted) {
                controlPanel.gameObject.SetActive(true);
                
                foreach (AudioSource sound in sounds) { sound.Stop(); }
                sounds[0].Play(0);
            }

            if (generatorStatus) {
                if (isInteracting == "door") {
                    if(doorAnim.GetBool("open"))
                        doorAnim.SetBool("open", false);
                    else
                        doorAnim.SetBool("open", true); 
                } else
                if (isInteracting == tags[3]) {
                    oxygen = 100;
                    uiOxygen.text = "Oxygen\n" + oxygen.ToString() + "%";
                } else
                if (isInteracting == "spawnBall")
                    GameObject.Find("Hatch Wall/Console Kiri").GetComponent<SphereBall>().isSpawn = true;
            }
        }
    }

    private void FixedUpdate() {
        uiTimeDate.text = DateTime.Now.ToString("HH:mm:ss") + "\n" + DateTime.Now.ToString("dd MMMM yyyy");
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == tags[0]) {
            string parentName = other.gameObject.transform.parent.name;

            if (parentName == "Hatch Wall") {
                uiInteract.text = "Press E to spawn sphere ball";
                isInteracting = "spawnBall";
            } else {
                uiInteract.text = "Press E to open/close door";
                if (parentName == "Hatch Door") {
                    uiInteract.text = "Press E to open/close hatch";
                }
                doorAnim = other.GetComponentInParent<Animator>();
                isInteracting = "door";
            }

            if (!unlockLobby && parentName == "Lobby Door") {
                uiInteract.text = "Door Locked!";
                doorAnim = null;
                isInteracting = null;
            }
        } else
        if(other.gameObject.tag == tags[1]){
            if(accessGranted) {
                uiInteract.text = "Press E to access control panel";
                isInteracting = tags[1];
            } else {
                uiInteract.text = "Access Denied (Need Card)";
            }   
        } else
        if(other.gameObject.tag == tags[2]){
            uiInteract.text = "Press E to take card";
            isInteracting = tags[2];

            this.other = other;
        } else
        if (other.gameObject.tag == tags[3]){
            uiInteract.text = "Press E to refill oxygen";
            isInteracting = tags[3];
        }
        foreach (string tag in tags) {
            if (other.gameObject.tag == tag) {
                uiInteract.gameObject.SetActive(true);
                break;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        foreach (string tag in tags) {
            if (other.gameObject.tag == tag) {
                uiInteract.gameObject.SetActive(false);
                isInteracting = null;
                break;
            }
        }
    }

    private void oxygenDecrease() {
        oxygen--;
        uiOxygen.text = "Oxygen\n" + oxygen.ToString() + "%";
    }
}