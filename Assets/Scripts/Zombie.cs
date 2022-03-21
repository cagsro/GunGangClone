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
    // Start is called before the first frame update
    void Start()
    {
        zombieAnimator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FindEnemy();
        if(targetEnemy)
        {
            targetPos=targetEnemy.position;
            //targetPos.y=0.5f;
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);
            //this.transform.position=Vector3.Lerp(this.transform.position,targetPos,Time.deltaTime*speed);
        }
        if(zombieHealth<=0)
        {
            Destroy(this.gameObject);
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
            zombieHealth--;
            
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
            }
            else 
            {
                targetEnemy.gameObject.GetComponent<Player>().TakeDamage();
            }
            
        }  
    }

}
