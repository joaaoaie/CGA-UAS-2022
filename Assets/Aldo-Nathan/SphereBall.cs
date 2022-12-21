using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBall : MonoBehaviour
{
    public bool isSpawn = false;
    public int ballNumber = 10;
    GameObject[] ball;

    private void FixedUpdate() {
        if (isSpawn) {
            ball = new GameObject[ballNumber];
            for (int i = 0; i < ballNumber; i++) {
                ball[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                ball[i].transform.parent = this.transform;
                ball[i].transform.position = new Vector3((int)Random.Range(-10, 10), 0.5f, (int)Random.Range(-10, 10));
                // ball[i].AddComponent<Rigidbody>();
                // ball[i].GetComponent<Rigidbody>().useGravity = true;
                ball[i].tag = "sphereBall";
            }
            Debug.Log("Spawned");
            isSpawn = false;
        }
    }
}
