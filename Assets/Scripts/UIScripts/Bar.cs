using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Image fillImage;
    private float percent = 1;

    // Start is called before the first frame update
    void Start()
    {
        fillImage.fillAmount = percent;
    }

    // Update is called once per frame
    void Update()
    {
        if(fillImage != null)
        {
            
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, percent, Time.deltaTime * 2);
            //fillImage.fillAmount = percent;
        }
    }

    public void UpdateBar(int curAmount, int maxAmount, bool startUp = false)
    {
        percent = (float)curAmount/(float)maxAmount;
        percent = Mathf.Clamp(percent, 0, 1);

        if(startUp)
            fillImage.fillAmount = percent;
    }
}
