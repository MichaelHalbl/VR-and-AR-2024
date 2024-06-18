using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holebehaviour : MonoBehaviour
{
    public GameObject [] Moles;
    void Start()
    {
       // Invoke("Spawn", 2f); 
        Debug.Log("Init Moles length: " + Moles.Length);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void Spawn()
    {
        if (Moles.Length > 0)
    {
        int randomIndex = Random.Range(0, Moles.Length);
        Debug.Log("Random index: " + randomIndex);
        Debug.Log("Moles length: " + Moles.Length);
        GameObject mole = Instantiate(Moles[randomIndex], transform.position, Quaternion.identity) as GameObject;
        Invoke("Spawn", Random.Range(2f, 5f));
    }
    else
    {
        Debug.Log("Moles array is empty");
    }
    }

    void OnMouseDown()
    {
        Debug.Log("Hole was clicked");
    }
}
