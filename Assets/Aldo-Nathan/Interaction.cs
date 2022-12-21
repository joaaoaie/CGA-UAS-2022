using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Interaction : MonoBehaviour {
    public Animator playerAnim, doorAnim, hatchAnim;

    public TextMeshProUGUI uiInteract;
    public Image controlPanel;
    private string isInteracting = null;

    public Collider other;

    public Button btnGenerator, btnDoor, closePanel;

    void Awake() {
        playerAnim = this.GetComponent<Animator>();

        uiInteract = GameObject.Find("interactDialog").GetComponent<TextMeshProUGUI>();

        controlPanel = GameObject.Find("controlPanel").GetComponent<Image>();

        btnGenerator = GameObject.Find("controlPanel/GeneratorButton").GetComponent<Button>();
        btnDoor = GameObject.Find("controlPanel/UnlockDoor").GetComponent<Button>();
        closePanel = GameObject.Find("controlPanel/Close").GetComponent<Button>();

        btnGenerator.onClick.AddListener(() => {
            if(playerAnim.GetBool("generatorStatus")) {
                playerAnim.SetBool("generatorStatus", false);
                // btnGenerator.GetComponentInChildren<TextMeshProUGUI>().text = "Turn On Generator";
            } else {
                playerAnim.SetBool("generatorStatus", true);
                // btnGenerator.GetComponentInChildren<TextMeshProUGUI>().text = "Turn Off Generator";
            }
        });

        btnDoor.onClick.AddListener(() => {
            if(playerAnim.GetBool("unlockLobby"))
                playerAnim.SetBool("unlockLobby", false);
            else
                playerAnim.SetBool("unlockLobby", true);
        });

        closePanel.onClick.AddListener(() => {
            controlPanel.gameObject.SetActive(false);
        });

        uiInteract.gameObject.SetActive(false);
        controlPanel.gameObject.SetActive(false);
    }

    void FixedUpdate() {
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
        }

        if (Input.GetKeyUp(KeyCode.E) && playerAnim.GetBool("generatorStatus") && isInteracting == "hatch") {
            if(hatchAnim.GetBool("open"))
                hatchAnim.SetBool("open", false);
            else
                hatchAnim.SetBool("open", true);
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
                } else if (other.gameObject.transform.parent.name == "Hatch Door") {
                    doorAnim = GameObject.Find("Hatch Door").GetComponent<Animator>();
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
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "consolePanel" || other.gameObject.tag == "computer" || other.gameObject.tag == "card" || other.gameObject.tag == "hatch") {
            uiInteract.gameObject.SetActive(false);
            isInteracting = null;
        }
    }
}
