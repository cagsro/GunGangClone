using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "barrel")
        {
            Debug.Log("carpti");
            Destroy(this.gameObject);
            BarrelHealth.instance.TakeDamage();
        }
    }
}
