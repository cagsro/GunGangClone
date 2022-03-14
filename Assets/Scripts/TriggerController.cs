using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public GameObject playerParent;
    private Vector3 endPosition =new Vector3(1f,0f,0f);
    public bool OnGround=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OnGround)
        {
            this.transform.localPosition=Vector3.Lerp(this.transform.localPosition,endPosition,Time.deltaTime);
        }
        
    }
    void OnCollisionEnter(Collision other)
    {
        if (this.transform.tag=="enemy"&&other.transform.tag == "Player")
        {
            Debug.Log("SetParent");
            this.transform.SetParent(playerParent.transform);
            OnGround=true;
            //this.transform.localPosition=new Vector3(1f,0f,0f);
            this.transform.rotation=Quaternion.Euler(0f,0f,0f);
            this.transform.tag="Player";
            this.GetComponent<PlayerShooting>().enabled=true;
        }
        if (this.transform.tag=="Player"&&other.transform.tag == "barrel")
        {
            Debug.Log("Barrel");
            Destroy(this.gameObject);
        }
    }
}
