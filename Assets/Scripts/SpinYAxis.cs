using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s : MonoBehaviour
{
    public float spinSpeed = 60f; // Legt fest, wie schnell das Rad sich drehen soll (in Umdrehungen pro Minute).
    public int rotationCount = 0;
    public float rotationResetTime = 20.0f; // Dauer des automatischen Pauses zwischen den Rotationen (in Sekunden)
    public float rotationDuration = 0.0f;
    
    void Update()  
    {
      spinSpeed=15;
       rotationDuration+=Time.deltaTime;
        
        if (rotationDuration > 15){
            spinSpeed=0;
            
          if(rotationResetTime< rotationDuration){
           rotationDuration=0;
            rotationCount++;
          }
        }
        transform.Rotate(Vector3.up * Time.deltaTime * spinSpeed *-1);
    }
}
   
    





 