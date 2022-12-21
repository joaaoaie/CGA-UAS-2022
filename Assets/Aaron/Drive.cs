using UnityEngine;

public class Drive : MonoBehaviour {
    public float speed = 50f;
    public float rotationSpeed = 100.0f;
    private float translation;
    private float rotation;
    public Animator playerAnim;

    void Awake() {
        playerAnim = this.GetComponent<Animator>();
    }

    void FixedUpdate() {
        translation = Input.GetAxis("Vertical") * speed;
        rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.fixedDeltaTime;
        rotation *= Time.fixedDeltaTime;

        transform.Rotate(0, rotation, 0);

        playerAnim.SetFloat("characterSpeed", translation);

        if(Input.GetKey(KeyCode.LeftShift)) {
            playerAnim.SetBool("isRunning", true);
        } else {
            playerAnim.SetBool("isRunning", false);
        }

        if(translation != 0){
            playerAnim.SetBool("isWalking", true);
        } else {
            playerAnim.SetBool("isWalking", false);
            playerAnim.SetFloat("characterSpeed", 0);
        }
    }

    // void FixedUpdate()
    // {
    //     float translation = Input.GetAxis("Vertical") * speed;
    //     float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
    //     translation *= Time.fixedDeltaTime;
    //     rotation *= Time.fixedDeltaTime;
    //     transform.Translate(0, 0, translation);
    //     transform.Rotate(0, rotation, 0);

    //     if (translation != 0 && Input.GetKey(KeyCode.LeftShift)){
    //         playerAnim.SetBool("isRunning", true);
    //         playerAnim.SetBool("isWalking", false);
    //         // playerAnim.SetBool("isJumping", false);
    //         playerAnim.SetFloat("characterSpeed", translation);
    //     }else if (translation == 0 && Input.GetKey(KeyCode.Space)){
    //         // playerAnim.SetBool("isJumping", true);
    //         playerAnim.SetBool("isWalking", false);
    //         playerAnim.SetBool("isRunning", false);
    //         playerAnim.SetFloat("characterSpeed", translation);
    //     }else if (translation != 0){
    //         playerAnim.SetBool("isWalking", true);
    //         playerAnim.SetBool("isRunning", false);
    //         // playerAnim.SetBool("isJumping", false);
    //         playerAnim.SetFloat("characterSpeed", translation);
    //     }else {
    //         playerAnim.SetBool("isRunning", false);
    //         playerAnim.SetBool("isWalking", false);
    //         // playerAnim.SetBool("isJumping", false);
    //         playerAnim.SetFloat("characterSpeed", 0);
    //     }

    // }
}
