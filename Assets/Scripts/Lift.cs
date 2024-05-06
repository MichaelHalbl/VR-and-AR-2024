﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifter : MonoBehaviour
{
  public float liftSpeed = 15f; // Legt fest, wie schnell das Rad sich drehen soll (in Umdrehungen pro Minute).
  public int rotationCount = 0;
  public int waitingCount = 4;
  public float waitduration = 0;
  public float targetYPosition = 25.0f;
  private bool paused = false;
  void Start()
    {
       lift();
    }

  void lift()
  { 
    liftSpeed=15;
     if (transform.position.y >= targetYPosition)
    {
      paused=true;
      pause();
    }
    transform.Translate(Vector3.up * Time.deltaTime * liftSpeed, Space.Self);
  }

void pause()
{
  waitduration=0;
  liftSpeed = 0;
  waitduration += Time.deltaTime;
  if (waitduration >= waitingCount)
  {
   paused=false;
   
  }
}
void down()
{
  liftSpeed=-10;
     if (transform.position.y <= 0)
    {
      paused=true;
      pause();
    }
  transform.Translate(Vector3.up * Time.deltaTime * liftSpeed, Space.Self);
}
}