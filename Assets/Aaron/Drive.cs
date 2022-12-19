using UnityEngine;

public class Drive : MonoBehaviour {
    public float speed = 50.0f;
    public float rotationSpeed = 100.0f;
    private float translation;
    private float rotation;
    Animator anim;

    void Awake() {
        anim = this.GetComponent<Animator>();
    }

    void FixedUpdate() {
        translation = Input.GetAxis("Vertical") * speed;
        rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.fixedDeltaTime;
        rotation *= Time.fixedDeltaTime;

        transform.Rotate(0, rotation, 0);

        anim.SetFloat("speed", translation);

        if(Input.GetKey(KeyCode.LeftShift)) {
            anim.SetBool("isRun", true);
        } else {
            anim.SetBool("isRun", false);
        }

        if(translation != 0){
            anim.SetBool("isWalk", true);
        } else {
            anim.SetBool("isWalk", false);
            anim.SetFloat("characterSpeed", 0);
        }
    }
}
