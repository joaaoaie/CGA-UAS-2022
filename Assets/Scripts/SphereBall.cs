using UnityEngine;

public class SphereBall : MonoBehaviour
{
    public bool isSpawn = false;
    public int ballNumber = 40;
    GameObject[] ball;
    GameObject balls;

    private void Awake() {
        balls = GameObject.FindWithTag("sphereBall");
    }

    private void FixedUpdate() {
        if (isSpawn) {
            ball = new GameObject[ballNumber];
            for (int i = 0; i < ballNumber; i++) {
                ball[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                ball[i].transform.position = balls.transform.position;
                ball[i].GetComponent<Renderer>().material.color = new Color(1,0.6352941176f,0,1);
                ball[i].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                ball[i].AddComponent<Rigidbody>();
                ball[i].GetComponent<Rigidbody>().useGravity = true;
                ball[i].tag = "sphereBall";
                ball[i].transform.parent = balls.transform;
            }
            isSpawn = false;
        }
    }
}
