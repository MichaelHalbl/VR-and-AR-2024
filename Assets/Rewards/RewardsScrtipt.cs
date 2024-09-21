using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsScrtipt : MonoBehaviour
{

    public GameObject table;
    public GameObject dose;
    public GameObject falling;
    public GameObject mole;
    public GameObject draht;
    public GameObject big;
    private ScoreScript scoreObject;

    // Start is called before the first frame update
    void Start()
    {
        scoreObject = GameObject.Find("ScoreObject").GetComponent<ScoreScript>();
        Material blankMat = table.GetComponent<Renderer>().material;
        if(scoreObject.DosenLevel < 3) {
            Renderer doseR = dose.GetComponent<Renderer>();
            doseR.material = blankMat;
            doseR.material.color = Color.gray;
        }
        if(scoreObject.fallingBallsLevel < 3)  {
            Renderer[] AlllRenderers = falling.GetComponentsInChildren<Renderer>();
            foreach  (Renderer r in AlllRenderers) {
                r.material = blankMat;
            }
        }
        if(scoreObject.MoleLevel < 3)  {
            Renderer moleR = mole.GetComponent<Renderer>();
            moleR.material = blankMat;
            moleR.material.color = Color.gray;
        }
        if(scoreObject.DrahtLevel < 3) {
            Renderer drahtR = draht.GetComponent<Renderer>();
            drahtR.material = blankMat;
            drahtR.material.color = Color.gray;
        }
        if(scoreObject.DosenLevel < 3 && scoreObject.fallingBallsLevel < 3 && scoreObject.MoleLevel < 3 && scoreObject.DrahtLevel < 3){
            Renderer bigR = big.GetComponent<Renderer>();
            bigR.material = blankMat;
            bigR.material.color = Color.gray;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
