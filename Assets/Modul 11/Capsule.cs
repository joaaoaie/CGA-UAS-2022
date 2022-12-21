using UnityEngine;

public class Capsule : MonoBehaviour {
    public int capsuleNumber = 10;
    
    GameObject[] capsule;
    void Start() {
        capsule = new GameObject[capsuleNumber];
        for (int i = 0; i < capsuleNumber; i++) {
            capsule[i] = Instantiate(Resources.Load("Capsule", typeof(GameObject))) as GameObject;
            // capsule[i] = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            capsule[i].transform.position = new Vector3((int)Random.Range(-10, 10), -1.5f, (int)Random.Range(-10, 10));
            // capsule[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            // capsule[i].AddComponent<Rigidbody>();
            // capsule[i].GetComponent<Rigidbody>().useGravity = false;
            // capsule[i].GetComponent<Rigidbody>().isKinematic = true;
            // capsule[i].AddComponent<CapsuleCollider>();
            // capsule[i].GetComponent<CapsuleCollider>().isTrigger = true;
            // capsule[i].tag = "Capsule";
        }
        // capsule = GameObject.Find("Capsule");
    }

    void FixedUpdate() {
        // Capsule float up and down
        // capsule.transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Sin(Time.time * 2) * 0.5f - 1, transform.localPosition.z);
        for (int i = 0; i < capsuleNumber; i++) {
            if (capsule[i] != null)
                capsule[i].transform.localPosition = new Vector3(capsule[i].transform.localPosition.x, Mathf.Sin(Time.time * 2) * 0.5f - 1, capsule[i].transform.localPosition.z);
        }
    }
}
