using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public  GameObject[] waitPos;
    public GameObject enemyWaitPos;
    public Transform firePosition;
    public GameObject clone;
    public GameObject bullet;
    public Rigidbody cloneRB;
    public GameObject playerParent;
    private Vector3 endPosition =new Vector3(0f,0.5f,-1f);

    public static int i;
    public float distance;
    public bool isParent;
    public bool OnWaitPos;
    public float speed=1f;
    public Animator enemyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        i=0;
        enemyAnimator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isParent)
        {
            if(FinishPad.isGameEnded)
            {
                this.GetComponent<Rigidbody>().isKinematic=true;
                this.GetComponent<CapsuleCollider>().enabled=false;
                this.transform.SetParent(enemyWaitPos.transform);
                speed=4f;
            }
            this.transform.localPosition=Vector3.Lerp(this.transform.localPosition,endPosition,Time.deltaTime*speed);
            distance=Vector3.Distance(this.transform.position,enemyWaitPos.transform.position);
            //Debug.Log(distance);
            if(distance<1.8f && !OnWaitPos)
            {
                Debug.Log(distance);
                OnWaitPos=true;
                enemyAnimator.SetTrigger("Idle");
            }
        }
        
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && this.transform.tag=="enemy")
        {
            Debug.Log("SetParent");
            this.transform.SetParent(playerParent.transform);
            isParent=true;
            this.transform.rotation=Quaternion.Euler(0f,0f,0f);
            this.transform.tag="Player";
            enemyAnimator.SetTrigger("Walk");
            InvokeRepeating("Shoot", 0f,1f);
            this.enemyWaitPos=waitPos[i];
            i++;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if(other.transform.tag=="barrel")
        {
            this.GetComponent<Rigidbody>().isKinematic=false;
        }
    }
    
    
    void Shoot()    
    {
        clone= Instantiate(bullet, firePosition.position, firePosition.rotation);
        cloneRB=clone.GetComponent<Rigidbody>();
        cloneRB.AddForce(clone.transform.forward*3000f);
        Destroy(clone.gameObject,0.4f); 
    }
}
