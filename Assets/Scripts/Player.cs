using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform firePosition;
    public GameObject clone;
    public GameObject bullet;
    public Rigidbody cloneRB;
    public GameObject playerWaitPos;
    private Vector3 endPosition =new Vector3(0f,0.5f,0.3f);
    public float distance;
    public bool OnWaitPos;
    public float speed=1f;
    public Animator playerAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator=GetComponent<Animator>();
        InvokeRepeating("Shoot", 0f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(FinishPad.isGameEnded)
        {
            speed=4f;
            this.transform.SetParent(playerWaitPos.transform);
            this.transform.localPosition=Vector3.Lerp(this.transform.localPosition,endPosition,Time.deltaTime*speed);
        }
        distance=Vector3.Distance(this.transform.position,playerWaitPos.transform.position);
        if(distance<1.9f && !OnWaitPos)
        {
            Debug.Log(distance);
            OnWaitPos=true;
            playerAnimator.SetTrigger("Idle");
        }
    }
    void Shoot()    
    {
        clone= Instantiate(bullet, firePosition.position, firePosition.rotation);
        cloneRB=clone.GetComponent<Rigidbody>();
        cloneRB.AddForce(clone.transform.forward*3000f);
        Destroy(clone.gameObject,0.4f); 
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 20f);
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.tag == "barrel")
        {
            Debug.Log("Barrel");
            Destroy(this.gameObject);
        }
    }
}
