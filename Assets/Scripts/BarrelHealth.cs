using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarrelHealth : MonoBehaviour
{
    public static BarrelHealth instance;

    public float health;
    //public TextMesh healthText;
    private TextMeshProUGUI healthText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 2f;
        healthText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health.ToString();
        if(health<=0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage()
    {
        health -= 1f;
    }
}
