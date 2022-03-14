using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Barrel : MonoBehaviour
{
    public float health;

    public GameObject barrel;

    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        health = 3f;
        healthText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health.ToString();
        if(health<=0)
        {
            barrel.transform.localScale=Vector3.Lerp(barrel.transform.localScale,new Vector3(1.5f,0f,1.5f),Time.deltaTime*2);
            if(barrel.transform.localScale.y<=0.2f)
            {
                Destroy(barrel.gameObject);
            }
            
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
    }
}
