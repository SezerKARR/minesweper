using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEditor : MonoBehaviour
{
    public GameObject GroundCreater;
    public int numberFlag;
    public Text changingText;
    void Start()
    {

        
        


    }

    // Update is called once per frame
    void Update()
    {
        numberFlag = GroundCreater.GetComponent<GroundCreater>().howManyFlag;
        changingText.text = ""+numberFlag ;
        
    }
}
