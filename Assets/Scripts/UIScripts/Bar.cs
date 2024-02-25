using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Image fillImage;
    private int maxHealth = 1;
    private float barHealth = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fillImage != null)
        {
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, barHealth, Time.deltaTime * 2);
        }
    }

    public void UpdateBar(int health)
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        barHealth = health/maxHealth;
        barHealth = Mathf.Clamp(barHealth, 0.000000001f, 1);
    }

    public void SetBarVariables(int maxAmount, int currentHealth = 0)
    {
        maxHealth = maxAmount;
        UpdateBar(currentHealth != 0 ? currentHealth : maxHealth);
    }
}
