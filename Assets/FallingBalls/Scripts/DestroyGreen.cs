using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGreen : MonoBehaviour
{

    private double y;
    private AudioSource success;
    private AudioSource fail;

    // Start is called before the first frame update
    void Start()
    {
        y = transform.position.y;
        success = GameObject.Find("success").GetComponent<AudioSource>();
        fail = GameObject.Find("fail").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if(transform.position.y >= y) return;
        if(transform.position.y > y-2.8) {
            GameObject.Find("ScriptObject").GetComponent<Gamelogic>().score += 20;
            success.Play(0);
        } else {
            GameObject.Find("ScriptObject").GetComponent<Gamelogic>().score -= 10;
            fail.Play(0);
        }
        Destroy(this.gameObject);
    }
}
