using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    public int value;
    public GameObject prefab;
    public GameObject parent;
    public string operation;
    // Start is called before the first frame update
    void Start()
    {
        valueText = GetComponentInChildren<TextMeshProUGUI>();
        valueText.text = value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            this.GetComponent<BoxCollider>().enabled = false;
            if(operation=="+")
            {
                int zPos = +2;
                int xPos = -1;
                for (int i = 0; i < value; i++)
                {
                    
                    GameObject clone=Instantiate(prefab, new Vector3(parent.transform.position.x+xPos, 0.5f, parent.transform.position.z +zPos), Quaternion.identity, parent.transform);
                    clone.GetComponent<Enemy>().isParent = true;
                    clone.GetComponent<Enemy>().script();
                    xPos++;
                    if (xPos >= 2) 
                    {
                        xPos = -1;
                        zPos--;
                    }
                    
                }
            }
            if (operation == "-")
            {
                for (int i = 1; i <= value; i++)
                {
                    WaitPos.instance.enemyList[i].GetComponent<Enemy>().speed=0f;
                    WaitPos.instance.enemyList[i].transform.parent=null;
                    WaitPos.instance.enemyList[i].GetComponent<Animator>().SetTrigger("Fall");
                    Destroy(WaitPos.instance.enemyList[i].gameObject,1.5f);
                    WaitPos.instance.enemyList.RemoveAt(i);
                }
            }
            if (operation == "*")
            {
                int x = (value - 1) * (WaitPos.instance.enemyList.Count);
                int zPos = +2;
                int xPos = -1;
                for (int i = 0; i < x; i++)
                {
                    GameObject clone = Instantiate(prefab, new Vector3(parent.transform.position.x+ xPos, 0.5f, parent.transform.position.z +zPos), Quaternion.identity, parent.transform);
                    clone.GetComponent<Enemy>().isParent = true;
                    clone.GetComponent<Enemy>().script();
                    xPos++;
                    if (xPos >= 2)
                    {
                        xPos = -1;
                        zPos--;
                    }
                }
            }
            if (operation == "/")
            {
                int x = (WaitPos.instance.enemyList.Count) - ((WaitPos.instance.enemyList.Count) / value);
                Debug.Log("b�lme" + x);
                for (int i = 1; i <= x; i++)
                {
                    WaitPos.instance.enemyList[i].GetComponent<Enemy>().speed=0f;
                    WaitPos.instance.enemyList[i].GetComponent<Animator>().SetTrigger("Fall");
                    WaitPos.instance.enemyList[i].transform.parent=null;
                    Destroy(WaitPos.instance.enemyList[i].gameObject,1.5f);
                    WaitPos.instance.enemyList.RemoveAt(i);
                }
            }

        }
    }
}
