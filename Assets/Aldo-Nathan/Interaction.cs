using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Interaction : MonoBehaviour {
    public Animator playerAnim, doorAnim, hatchAnim;

    public TextMeshProUGUI uiInteract, uiDate, uiTime;
    public Image controlPanel;

    public Collider other;
    public int oxygen = 80;
    public TextMeshProUGUI uiOxygen;

    public Button btnGenerator, btnDoor, closePanel;

    private string isInteracting = null;
    // private bool isPaused = false;
    
    void Awake() {
        GameObject[] lampu= GameObject.FindGameObjectsWithTag ("Lampu");
        playerAnim = this.GetComponent<Animator>();

        uiInteract = GameObject.Find("interactDialog").GetComponent<TextMeshProUGUI>();

        uiDate = GameObject.Find("dateDialog").GetComponent<TextMeshProUGUI>();
        uiTime = GameObject.Find("timeDialog").GetComponent<TextMeshProUGUI>();

        uiOxygen = GameObject.Find("oxygenDialog").GetComponent<TextMeshProUGUI>();
        uiOxygen.text = "Oxygen\n" + oxygen.ToString() + "%";
        InvokeRepeating("oxygenDecrease", 1.0f, 5.0f);

        controlPanel = GameObject.Find("controlPanel").GetComponent<Image>();

        btnGenerator = GameObject.Find("controlPanel/GeneratorButton").GetComponent<Button>();
        btnDoor = GameObject.Find("controlPanel/UnlockDoor").GetComponent<Button>();
        closePanel = GameObject.Find("controlPanel/Close").GetComponent<Button>();

        btnGenerator.onClick.AddListener(() => {
            if(playerAnim.GetBool("generatorStatus")) {
                btnGenerator.GetComponentInChildren<TextMeshProUGUI>().text = "Turn On Generator";
                playerAnim.SetBool("generatorStatus", false);
                foreach (GameObject i in lampu){
                    i.SetActive(false);
                }
                GameObject.Find("Sound Effect/off").GetComponent<AudioSource>().Play(0);
            } else {
                btnGenerator.GetComponentInChildren<TextMeshProUGUI>().text = "Turn Off Generator";
                foreach (GameObject i in lampu){
                    i.SetActive(true);
                }
                playerAnim.SetBool("generatorStatus", true);
                GameObject.Find("Sound Effect/on").GetComponent<AudioSource>().Play(0);
            }
        });

        btnDoor.onClick.AddListener(() => {
            Light light = GameObject.Find("Point Light").GetComponent<Light>();
            if(playerAnim.GetBool("unlockLobby")) {
                btnDoor.GetComponentInChildren<TextMeshProUGUI>().text = "Unlock Door";
                playerAnim.SetBool("unlockLobby", false);
                light.color = Color.red;
            } else {
                btnDoor.GetComponentInChildren<TextMeshProUGUI>().text = "Lock Door";
                playerAnim.SetBool("unlockLobby", true);
                light.color = Color.green;
            }
        });

        closePanel.onClick.AddListener(() => {
            controlPanel.gameObject.SetActive(false);
        });

        uiInteract.gameObject.SetActive(false);
        controlPanel.gameObject.SetActive(false);
    }

    private void Update() {

        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (Time.timeScale == 1) {
                Time.timeScale = 0;
                uiInteract.text = "Game Paused";
                uiInteract.gameObject.SetActive(true);
            } else {
                Time.timeScale = 1;
                uiInteract.gameObject.SetActive(false);
            }
        }
    }

    void FixedUpdate() {
        uiDate.text = DateTime.Now.ToString("dd MMMM yyyy");
        uiTime.text = DateTime.Now.ToString("HH:mm:ss");
        
        if (Input.GetKeyUp(KeyCode.E) && playerAnim.GetBool("generatorStatus") && isInteracting == "door") {
            if(doorAnim.GetBool("open"))
                doorAnim.SetBool("open", false);
            else
                doorAnim.SetBool("open", true); 
        }

        if (Input.GetKeyUp(KeyCode.E) && isInteracting == "card") {
            playerAnim.SetBool("accessGranted", true);
            uiInteract.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }

        if (Input.GetKeyUp(KeyCode.E) && isInteracting == "computer" && playerAnim.GetBool("accessGranted")) {
            controlPanel.gameObject.SetActive(true);
            GameObject.Find("Sound Effect/panel").GetComponent<AudioSource>().Play(0);
        }

        if (Input.GetKeyUp(KeyCode.E) && playerAnim.GetBool("generatorStatus") && isInteracting == "hatch") {
            if(hatchAnim.GetBool("open"))
                hatchAnim.SetBool("open", false);
            else
                hatchAnim.SetBool("open", true);
        }

        if (Input.GetKeyUp(KeyCode.E) && playerAnim.GetBool("generatorStatus") && isInteracting == "oxygen") {
            oxygen = 100;
            uiOxygen.text = "Oxygen\n" + oxygen.ToString() + "%";
        }

        if (Input.GetKeyUp(KeyCode.E) && isInteracting == "spawnBall" && playerAnim.GetBool("generatorStatus")) {
            GameObject.Find("Hatch Wall/Console Kiri").GetComponent<SphereBall>().isSpawn = true;
            // GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            // ball.transform.position = new Vector3(0, 0, 0);
            // ball.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            // ball.AddComponent<Rigidbody>();
            // ball.GetComponent<Rigidbody>().useGravity = true;
            // ball.GetComponent<Rigidbody>().mass = 1;
            // ball.GetComponent<Rigidbody>().drag = 0.5f;
            // ball.GetComponent<Rigidbody>().angularDrag = 0.5f;
            // ball.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
            // ball.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            // ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            // ball.GetComponent<Rigidbody>().isKinematic = false;
            // ball.GetComponent<Rigidbody>().freezeRotation = false;
            // ball.GetComponent<Rigidbody>().detectCollisions = true;
            // ball.GetComponent<Rigidbody>().maxAngularVelocity = 7;
            // ball.GetComponent<Rigidbody>().solverIterations = 7;
            // ball.GetComponent<Rigidbody>().solverVelocityIterations = 1;
            // ball.GetComponent<Rigidbody>().sleepThreshold = 0.14f;
            // ball.GetComponent<Rigidbody>().maxDepenetrationVelocity = 0.2f;
            // ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            // ball.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            // ball.GetComponent<Rigidbody>().centerOfMass = new Vector3(0, 0, 0);
            // ball.GetComponent<Rigidbody>().inertiaTensor = new Vector3(0, 0, 0);
            // ball.GetComponent<Rigidbody>().inertiaTensorRotation = new Quaternion(0, 0, 0, 0);
            // ball.GetComponent<Rigidbody>().detectCollisions = true;
            // ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            // ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }

        // if(Input.GetKeyDown("escape")) {
        //     if (!isPaused) {
        //         uiInteract.text = "Game Paused";
        //         uiInteract.gameObject.SetActive(true);
        //         isPaused = true;
        //     } else {
        //         isPaused = false;
        //         uiInteract.gameObject.SetActive(false);
        //     }
        // }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "consolePanel") {
            if (playerAnim.GetBool("unlockLobby") || other.gameObject.transform.parent.name != "Lobby Door") {
                uiInteract.text = "Press E to interact";

                if (other.gameObject.transform.parent.name == "Panel Door") {
                    doorAnim = GameObject.Find("Panel Door").GetComponent<Animator>();
                isInteracting = "door";
                } else if (other.gameObject.transform.parent.name == "Lobby Door") {
                    doorAnim = GameObject.Find("Lobby Door").GetComponent<Animator>();
                isInteracting = "door";
                } else if (other.gameObject.transform.parent.name == "Hatch Wall") {
                    uiInteract.text = "Press E to spawn sphere ball";
                    isInteracting = "spawnBall";
                }
            } else {
                uiInteract.text = "Door Locked!";
            }
            uiInteract.gameObject.SetActive(true);
        }

        if(other.gameObject.tag == "hatch"){
            hatchAnim = GameObject.Find("Hatch Door").GetComponent<Animator>();
            isInteracting = "hatch";
        }
 
        if(other.gameObject.tag == "computer"){
            if(playerAnim.GetBool("accessGranted")==false){
                uiInteract.text = "Access Denied";
                uiInteract.gameObject.SetActive(true);
            }else{
                uiInteract.text = "Press E to access control panel";
                uiInteract.gameObject.SetActive(true);
                isInteracting = "computer";
            }   
        }

        if(other.gameObject.tag == "card"){
            uiInteract.text = "Press E to pick up card";
            uiInteract.gameObject.SetActive(true);
            isInteracting = "card";

            this.other = other;
        }

        if (other.gameObject.tag == "Door"){
            if (other.gameObject.transform.parent.name == "Panel Door") {
                doorAnim = GameObject.Find("Panel Door").GetComponent<Animator>();
            } else if (other.gameObject.transform.parent.name == "Lobby Door") {
                doorAnim = GameObject.Find("Lobby Door").GetComponent<Animator>();
            }
        }

        if (other.gameObject.tag == "hatch"){
            uiInteract.text = "Press E to open/close hatch";
            uiInteract.gameObject.SetActive(true);
        }

        if (other.gameObject.tag == "oxygenChamber"){
            uiInteract.text = "Press E to refill oxygen";
            uiInteract.gameObject.SetActive(true);
            isInteracting = "oxygen";
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "consolePanel" || other.gameObject.tag == "computer" || other.gameObject.tag == "card" || other.gameObject.tag == "hatch" || other.gameObject.tag == "oxygenChamber") {
            uiInteract.gameObject.SetActive(false);
            isInteracting = null;
        }
    }

    void oxygenDecrease() {
        oxygen -= 1;
        uiOxygen.text = "Oxygen\n" + oxygen.ToString() + "%";
    }
}