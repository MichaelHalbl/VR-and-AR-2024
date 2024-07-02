using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyYellow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if(transform.position.y >= 3.5) return;
        if(transform.position.y > 0.5) {
            GameObject.Find("ScriptObject").GetComponent<Gamelogic>().score += 30;
        }
        Destroy(this.gameObject);
    }
}
