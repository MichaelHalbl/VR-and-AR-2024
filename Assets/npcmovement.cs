using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor.Rendering;
using UnityEngine;

public class npcmovement : MonoBehaviour
{
    public GameObject NPC;
    private Transform[] Movepoints;
    private GameObject Move;
    private int index = 0;
    public GameObject LocationCordsall;
    public Transform[] LocationCords;
    public float speed = 4f;

    void Start()
    {
        Movepoints = new Transform[LocationCordsall.transform.childCount];
        for (int i = 0; i < LocationCordsall.transform.childCount; i++)
        {
            Movepoints[i] = LocationCordsall.transform.GetChild(i);
        }
        Move = Instantiate(NPC, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
    }

    void Update()
    {
        if (index > Movepoints.Length - 2)
        {
            index = 0;
        }

        float step = speed * Time.deltaTime;
        Vector3 targetPosition = Movepoints[index].position;

        // Berechne die Richtung zur nächsten Position
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Bewege den NPC in die Richtung der nächsten Position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // Rotiere den NPC in die Bewegungsrichtung
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
        }

        // Update die Position des Instanzierten NPCs
        Move.transform.position = transform.position;
        Move.transform.rotation = transform.rotation;

        // Prüfe, ob der NPC das Ziel erreicht hat
        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            index++;
        }
    }
}

