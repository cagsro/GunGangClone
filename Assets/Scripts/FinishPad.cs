using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPad : MonoBehaviour
{
    public static bool isGameEnded;
    // Start is called before the first frame update
    void Start()
    {
        isGameEnded=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            Debug.Log("Oyun Bitti");
            isGameEnded=true; 
        }
    }
}
