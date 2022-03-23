using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    public Transform targetEnemy;
    public Vector3 targetPos;
    public Animator zombieAnimator;
    public float speed=1f;
    public float zombieHealth=10f;
    public GameObject target;

    public float timeBetweenAttacks = 0.5f;
    float timer;
    bool playerInRange;


    public Color startColor;
    public Color endColor;
    public float colorSpeed=0f;
    public float startTime=0f;
    public bool isDead;
    public Material[] mat;

    // Start is called before the first frame update
    void Start()
    {
        zombieAnimator=GetComponent<Animator>();
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

        FindEnemy();
        if(targetEnemy)
        {
            targetPos=targetEnemy.position;
            //targetPos.y=0.5f;
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);
            //this.transform.position=Vector3.Lerp(this.transform.position,targetPos,Time.deltaTime*speed);
        }
        timer += Time.deltaTime;
        if(timer >= timeBetweenAttacks && playerInRange)
        {
            Attack ();
        }
    }

    public void FindEnemy()
    {
        if(WaitPos.instance.enemyList.Count<=0)
        {
            this.zombieAnimator.SetTrigger("ZombieIdle");
            targetEnemy=null;
            return;
        }
        targetEnemy=WaitPos.instance.enemyList[0].transform;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "bullet")
        {
            Debug.Log("Zombiye Carpti");
            Destroy(other.gameObject);
            TakeDamage(other.GetComponent<Bullet>().damageAmount);
            
            
        }
        if (other.transform.tag == "Player")
        {
            //target=other.gameObject;
            playerInRange=true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.transform.tag == "Player")
        {
            playerInRange=false;
        }
    }
    void Attack ()
    {
        timer = 0f;
        if(targetEnemy)
        {
            if(targetEnemy.gameObject.GetComponent<Enemy>())
            {
                targetEnemy.gameObject.GetComponent<Enemy>().TakeDamage();
                isDead=true;
                this.zombieAnimator.SetTrigger("DieBack");
                speed=0;
                colorSpeed=0.5f;
                Destroy(this.gameObject,4f);

                
            }
            else 
            {
                targetEnemy.gameObject.GetComponent<Player>().TakeDamage();
                isDead=true;
                this.zombieAnimator.SetTrigger("DieBack");
                speed=0;
                colorSpeed=0.5f;
                Destroy(this.gameObject,4f);
            }
            
        }  
    }
    public void TakeDamage(float bulletAmount)
    {
        zombieHealth=zombieHealth-bulletAmount;
        if(zombieHealth<=0)
        {
            isDead=true;
            speed=0;
            colorSpeed=0.5f;
            this.zombieAnimator.SetTrigger("Die");
            Destroy(this.gameObject,4f);
        }
    }

}
