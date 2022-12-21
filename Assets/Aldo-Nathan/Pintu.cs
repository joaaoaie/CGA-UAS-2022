using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pintu : MonoBehaviour
{
    public Animator anim;

    private void Awake() {
        anim = this.GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other) {
    // kurang display ui dibagian ini
    if(Input.GetKey(KeyCode.E)) {
        if(anim.GetBool("open"))
            anim.SetBool("open", false);
        else
            anim.SetBool("open", true);
    }
}
}
