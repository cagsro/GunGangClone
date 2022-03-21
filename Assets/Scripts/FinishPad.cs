using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPad : MonoBehaviour
{
    public static bool isGameEnded;
    public static bool spawn;
    public GameObject prefab;
    public int i=0;
    // Start is called before the first frame update
    void Start()
    {
        isGameEnded=false;
        spawn=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawn)
        {
            SpawnZombie();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            Debug.Log("Oyun Bitti");
            isGameEnded=true;
            this.GetComponent<BoxCollider>().enabled=false;
            spawn=true;
        }
    }

    public void SpawnZombie()
    {
        for(i=0;i<10;i++)
        {
            Instantiate(prefab,new Vector3(0f+i-4f,0.5f,40f),Quaternion.Euler(0,180,0));
        }
        spawn=false;
    }
}
