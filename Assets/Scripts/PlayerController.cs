using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Touch touch;
    public float speed = 3f;
    public float speedModifier=0.002f;
    Vector3 firstPos,endPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!FinishPad.isGameEnded)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }  

        if(Input.GetMouseButtonDown(0))
        {
            firstPos=Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            endPos=Input.mousePosition;
            float farkX=endPos.x-firstPos.x;
            transform.Translate(farkX*speedModifier, 0, 0 );
        }
        if(Input.GetMouseButtonUp(0))
        {
            firstPos=Vector3.zero;
            endPos=Vector3.zero;
        }


        /*if (Input.touchCount > 0) // Dokunma varsa;
        {
            touch = Input.GetTouch(0); // Degiskeni atama atama
            if (touch.phase == TouchPhase.Moved) // Dokunma basladiginda;
            {
                //Yeni koordinatlar bunlar olsun.
                transform.position = new Vector3(
                    transform.position.x + touch.deltaPosition.x * speedModifier,
                    transform.position.y,
                    transform.position.z);
            }
        }*/
    }
    
}
