using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class StartFalling : MonoBehaviour
{

    private InputDevice rightHandController; // Referenz auf den rechten Controller
    private bool isInRange = false; // Ob der Spieler in Reichweite des Ticket-Standes ist
    private int start = 60;

    // Start is called before the first frame update
    void Start()
    {
        // Suche den rechten Hand-Controller
        var rightHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);
        if (rightHandDevices.Count > 0)
        {
            rightHandController = rightHandDevices[0]; // Nimm das erste gefundene Gerät
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (isInRange && rightHandController.IsPressed(InputHelpers.Button.PrimaryButton, out bool isPressedA, 0.1f) && isPressedA)
        {
            var op = SceneManager.LoadSceneAsync(scene.FallingBalls);
        }*/
        start--;
        if(start <= 0){


            var op =  SceneManager.LoadSceneAsync("FallingBallsGame");
            op.allowSceneActivation = false;
            while(op.progress < 0.9f) {
                start++;
            }
            op.allowSceneActivation = true;
        }
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true; // Spieler ist in Reichweite
        }
    }

     void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false; // Spieler ist nicht mehr in Reichweite
        }
    }
}
