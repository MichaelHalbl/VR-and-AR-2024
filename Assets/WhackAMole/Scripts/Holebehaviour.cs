using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holebehaviour : MonoBehaviour
{
    public GameObject [] moles;
    void Start()
    {
        Invoke("Spawn", 2f); 
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void Spawn()
    {
        int randomIndex = Random.Range(0, moles.Length);
        GameObject mole = Instantiate(moles[randomIndex], transform.position, Quaternion.identity) as GameObject;
        Invoke("Spawn", Random.Range(3f, 7f));
    }

    void OnMouseDown()
    {
        Debug.Log("Hole was clicked");
    }
}
