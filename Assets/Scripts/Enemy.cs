using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Material[] mat;
    
    public Color startColor;
    public Color endColor;
    public float colorSpeed=0f;
    public float startTime=0f;
    public bool isDead;

    public bool isShooting=true;


    public GameObject enemyWaitPos;
    public Transform firePosition;
    public GameObject clone;
    public GameObject bullet;
    public Rigidbody cloneRB;
    public GameObject playerParent;
    private Vector3 endPosition =new Vector3(0f,0.5f,+0.3f);

    public static int i;
    public float distance;
    public bool isParent;
    public bool OnWaitPos;
    public float speed=1f;
    public Animator enemyAnimator;
    public float enemyHealth=5;
    // Start is called before the first frame update
    void Start()
    {
        colorSpeed=0f;
        playerParent = GameObject.Find("Player");
        i =0;
        distance = 5f;
        enemyAnimator =GetComponent<Animator>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().materials;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (isDead)
        {
            for(int i=0;i<10;i++)
            {
                float t=(Time.time-startTime)*colorSpeed;
                mat[i].color=Color.Lerp(startColor,endColor,t);
            }
        }
        
        if(isParent)
        {
            if(FinishPad.isGameEnded)
            {
                WaitPos.instance.SetWaitPos();

                this.GetComponent<Rigidbody>().isKinematic=true;
                this.GetComponent<CapsuleCollider>().enabled=false;
                this.transform.SetParent(enemyWaitPos.transform);
                speed=4f;
                distance = Vector3.Distance(this.transform.position, enemyWaitPos.transform.position);
                //Debug.Log("Mesafe "+distance);

            }
            this.transform.localPosition=Vector3.Lerp(this.transform.localPosition,endPosition,Time.deltaTime*speed);
            if(distance<1.8f && !OnWaitPos)
            {
                
                OnWaitPos=true;
                enemyAnimator.SetTrigger("Idle");
            }
        }
        
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && this.transform.tag=="enemy")
        {
            script();
            /*Debug.Log("SetParent");
            this.transform.SetParent(playerParent.transform);
            isParent=true;
            this.transform.rotation=Quaternion.Euler(0f,0f,0f);
            this.transform.tag="Player";
            enemyAnimator.SetTrigger("Walk");
            InvokeRepeating("Shoot", 0f,1f);
            WaitPos.instance.enemyList.Add(this.gameObject);*/
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if(other.transform.tag=="barrel")
        {
            this.GetComponent<Rigidbody>().isKinematic=false;
        }
    }
    public IEnumerator Shoot()    
    {

        while (isShooting)
        {
            yield return new WaitForSeconds(0.7f);

            clone= Instantiate(bullet, firePosition.position, firePosition.rotation);
            cloneRB=clone.GetComponent<Rigidbody>();
            cloneRB.AddForce(clone.transform.forward*3000f);
            Destroy(clone.gameObject,0.4f);
        }
         
    }
    public void script()
    {
        //Debug.Log("SetParent");
        playerParent = GameObject.Find("Player");
        this.transform.SetParent(playerParent.transform);
        isParent = true;
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        this.transform.tag = "Player";
        enemyAnimator = GetComponent<Animator>();
        enemyAnimator.SetTrigger("Walk");
        StartCoroutine(Shoot());
        WaitPos.instance.enemyList.Add(this.gameObject);
    }
    public void TakeDamage()
    {
        enemyHealth=0f;
        if(enemyHealth<=0)
        {
            isShooting=false;
            isDead=true;
            colorSpeed=0.5f;
            enemyAnimator.SetTrigger("Die");
            WaitPos.instance.enemyList.Remove(this.gameObject);
            Destroy(this.gameObject,4f);
            
        }
    }

}
