using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Image fillImage;
    [SerializeField] private float percent = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fillImage != null)
        {
            percent = Mathf.Clamp(percent, 0, 1);
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, percent, Time.deltaTime * 2);
        }
    }
}
