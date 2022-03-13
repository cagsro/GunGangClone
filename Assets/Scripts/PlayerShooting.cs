using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    RaycastHit obj;
    public Transform firePosition;
    public GameObject clone;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 0f,1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    void Shoot()    
    {
        clone= Instantiate(bullet, firePosition.position, firePosition.rotation);
        clone.GetComponent<Rigidbody>().AddForce(clone.transform.forward*3000f);
        Destroy(clone.gameObject,3f);
        /*if (Physics.Raycast(firePosition.transform.position, firePosition.transform.forward, out obj, 20f))
        {   
            Debug.Log("Ateþ edildi");
            if (obj.transform.tag == "barrel")
            {
                Debug.Log("Barrel");
                Destroy(obj.transform.gameObject);
            }
        }*/
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 20f);
    }
}
