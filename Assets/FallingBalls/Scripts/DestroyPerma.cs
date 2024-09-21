using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPerma : MonoBehaviour
{

    private double y;

    // Start is called before the first frame update
    void Start()
    {
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if(transform.position.y >= y) return;
        Destroy(this.gameObject);
    }
}
