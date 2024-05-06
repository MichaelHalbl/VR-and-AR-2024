using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisWheelMain : MonoBehaviour
{
    public float rotationSpeed = 60f; // Legt fest, wie schnell das Rad sich drehen soll (in Umdrehungen pro Minute).
    public int currentRotationCount = 0;
    public float rotationPauseDuration = 20.0f; // Dauer des automatischen Pauses zwischen den Rotationen (in Sekunden)
    public float currentRotationTimer = 0.0f;
    
    void Update()  
    {
        rotationSpeed=15;
        currentRotationTimer+=Time.deltaTime;
        
        if(currentRotationTimer > 15){
            rotationSpeed=0;
            
          if(rotationPauseDuration<currentRotationTimer){
            currentRotationTimer=0;
            currentRotationCount++;
          }
        }
        transform.Rotate(Vector3.back * Time.deltaTime * rotationSpeed);
    }
}
   
    





