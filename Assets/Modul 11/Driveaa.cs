using UnityEngine;
using TMPro;

public class Driveaa : MonoBehaviour {
    public float speed = 3.0f;
    public float rotationSpeed = 100.0f;
    Animator anim;
    int score;

    void Awake() {
        anim = GetComponent<Animator>();
        score = 0;
    }

    void FixedUpdate() {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.fixedDeltaTime;
        rotation *= Time.fixedDeltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        if (translation != 0) {
            anim.SetBool("isWalking", true);
            anim.SetFloat("characterSpeed", translation < 0 ? -1 : 1);
        } else {
            anim.SetBool("isWalking", false);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Capsule")) {
            Destroy(other.gameObject);
            TextMeshProUGUI scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
            score++;
            scoreText.text = "Counter : " + score;
        }
    }
}
