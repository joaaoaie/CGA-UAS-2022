using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBall : MonoBehaviour
{
    public bool isSpawn = false;
    public int ballNumber = 10;
    GameObject[] ball;
    GameObject balls;

    private void Awake() {
        balls = GameObject.Find("Sphere");
    }

    private void FixedUpdate() {
        if (isSpawn) {
            ball = new GameObject[ballNumber];
            for (int i = 0; i < ballNumber; i++) {
                ball[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                ball[i].transform.position = balls.transform.position;
                ball[i].GetComponent<Renderer>().material.color = new Color(1,0.6352941176f,0,1);
                // ball[i].transform.position = new Vector3((float)Random.Range(-6.8f, -2.7f), 1.8f, (float)Random.Range(3.8f, 6.8f));
                // ball[i].transform.localScale = new Vector3(ball[i].transform.localScale.x, 0.5f, ball[i].transform.localScale.z);
                ball[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                ball[i].AddComponent<Rigidbody>();
                ball[i].GetComponent<Rigidbody>().useGravity = true;
                ball[i].GetComponent<Rigidbody>().mass = 5f;
                ball[i].tag = "sphereBall";
                // ball[i].transform.parent = this.transform;
            }
            Debug.Log("Spawned");
            isSpawn = false;
        }
    }
}
