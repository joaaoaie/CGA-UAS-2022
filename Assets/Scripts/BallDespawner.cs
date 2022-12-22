using UnityEngine;

public class BallDespawner : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "sphereBall"){
            Destroy(other.gameObject);
        }
    }
}
