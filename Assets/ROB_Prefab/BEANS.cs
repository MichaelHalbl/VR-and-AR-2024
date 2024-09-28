using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEANS : MonoBehaviour
{
    private AudioSource boing;

    // Start is called before the first frame update
    void Start()
    {
        boing = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        boing.Play(0);
    }
}
