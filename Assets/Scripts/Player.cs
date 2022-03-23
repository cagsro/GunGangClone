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
    public float playerHealth = 5f;
    public Animator playerAnimator;

    public Color startColor;
    public Color endColor;
    public float colorSpeed=0f;
    public float startTime=0f;
    public bool isDead;
    public Material[] mat;
    public bool isShooting=true;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator=GetComponent<Animator>();
        StartCoroutine(Shoot());
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
    IEnumerator Shoot()
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
    public void TakeDamage()
    {
        playerHealth=0f;
        if(playerHealth<=0)
        {
            isDead=true;
            isShooting=false;
            colorSpeed=0.5f;
            playerAnimator.SetTrigger("Die");
            WaitPos.instance.enemyList.Remove(this.gameObject);
            Destroy(this.gameObject,4f);
            
        }
    }
}
