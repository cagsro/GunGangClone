using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform firePosition;
    public GameObject clone;
    public GameObject bullet;
    public Rigidbody cloneRB;

    public GameObject playerParent;
    private Vector3 endPosition =new Vector3(1f,0f,0f);
    public bool OnGround=false;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 0f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(OnGround)
        {
            this.transform.localPosition=Vector3.Lerp(this.transform.localPosition,endPosition,Time.deltaTime);
        }
    }
    void Shoot()    
    {
        clone= Instantiate(bullet, firePosition.position, firePosition.rotation);
        cloneRB=clone.GetComponent<Rigidbody>();
        cloneRB.AddForce(clone.transform.forward*3000f);
        Destroy(clone.gameObject,3f); 
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 20f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "enemy")
        {
            Debug.Log("SetParent");
            other.transform.SetParent(playerParent.transform);
            OnGround=true;
            other.transform.rotation=Quaternion.Euler(0f,0f,0f);
            other.transform.tag="Player";
            other.GetComponent<Player>().enabled=true;
        }
        if (other.transform.tag == "barrel")
        {
            Debug.Log("Barrel");
            Destroy(this.gameObject);
        }
    }
}
