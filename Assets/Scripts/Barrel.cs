using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Barrel : MonoBehaviour
{
    public float health;
    public float lerpValue;
    //public Material mat;
    
    

    public GameObject barrel;

    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        health = 5f;
        healthText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health.ToString();
        lerpValue=1f-(health/5f);
        barrel.transform.localScale=Vector3.Lerp(new Vector3(1.5f,0.75f,1.5f),new Vector3(1.5f,0f,1.5f),lerpValue);

        if(barrel.transform.localScale.y<=0.05f)
        {
            Destroy(barrel.gameObject);
        }

    }
    public void TakeDamage()
    {
        health -= 1f;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "bullet")
        {
            Debug.Log("carpti");
            Destroy(other.gameObject);
            health--;
        }
        if (other.transform.tag == "Player")
        {
            //mat=other.GetComponentInChildren<Material>();
            WaitPos.instance.enemyList.Remove(other.gameObject);
            Debug.Log("Barrel");
            Destroy(other.gameObject);
        }
    }
}
