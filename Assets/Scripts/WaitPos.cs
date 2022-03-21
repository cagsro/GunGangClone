using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitPos : MonoBehaviour
{
    public static WaitPos instance;
    public List<GameObject> enemyList;
    public GameObject[] waitPos;
    public static int j;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        j = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetWaitPos()
    {
        if (j < enemyList.Count)
        {
            Debug.Log("Foreach");
            foreach (GameObject enemy in enemyList)
            {
                if(enemy.GetComponent<Enemy>())
                {
                    enemy.GetComponent<Enemy>().enemyWaitPos = waitPos[j];
                    j++;
                }
                else 
                {
                    enemy.GetComponent<Player>().playerWaitPos= waitPos[j];
                    j++;
                }
                /*enemy.GetComponent<Enemy>().enemyWaitPos = waitPos[j];
                j++;*/
            }
        }

    }
}
