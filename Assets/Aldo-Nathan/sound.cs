using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour{
    public AudioSource panel;
    public AudioSource on;
    public AudioSource off;
    public AudioSource door;

    public void panelSound(){
        panel.Play();
    }

    public void onSound(){
        on.Play();
    }

    public void offSound(){
        off.Play();
    }

    public void doorSound(){
        door.Play();
    }
}