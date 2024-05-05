using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisWheelMain : MonoBehaviour
{
    public float rotationSpeed = 60f; // Legt fest, wie schnell das Rad sich drehen soll (in Umdrehungen pro Minute).
    public int currentRotationCount = 0;
    public float rotationPauseDuration = 10.0f; // Dauer des automatischen Pauses zwischen den Rotationen (in Sekunden)
    public float currentRotationTimer = 0.0f;
    
    void Update()  
    {
        rotationSpeed=15;
        currentRotationTimer+=Time.deltaTime;
        
        if(currentRotationTimer > 5 ){
            rotationSpeed=0;
            currentRotationCount++;
          if(rotationPauseDuration<currentRotationTimer){
            currentRotationTimer=0;
          }
        }
        transform.Rotate(Vector3.back * Time.deltaTime * rotationSpeed);
    }
}
   
    





