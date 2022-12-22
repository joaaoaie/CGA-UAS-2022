using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 50f;
    public float rotationSpeed = 100.0f;
    private float translation;
    private float rotation;
    private Animator playerAnim;
    public static GameObject controlledBy;

    void Awake() {
        playerAnim = this.GetComponent<Animator>();
    }

    void Update() {
        if(Input.GetKey(KeyCode.LeftShift)) {
            playerAnim.SetBool("isRunning", true);
        } else {
            playerAnim.SetBool("isRunning", false);
        }
        
        if (controlledBy != null)
            return;

        translation = Input.GetAxis("Vertical") * speed;
        rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.fixedDeltaTime;
        rotation *= Time.fixedDeltaTime;

        transform.Rotate(0, rotation, 0);

        playerAnim.SetFloat("characterSpeed", translation);


        if(Input.GetKey(KeyCode.Space)){
            playerAnim.SetBool("isJumping", true);
        } else {
            playerAnim.SetBool("isJumping", false);
        }

        if(translation != 0){
            playerAnim.SetBool("isWalking", true);
        } else {
            playerAnim.SetBool("isWalking", false);
            playerAnim.SetFloat("characterSpeed", 0);
        }
    }
}
