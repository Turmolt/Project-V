using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QualityBar : MonoBehaviour
{
    public Image barImage;
    private float _quality;
    public float Quality {
        get{
            return _quality;
        } 
        set{
            _quality = value;
            barImage.fillAmount = Quality/5;
            if(gameObject.transform.parent.parent.name == "SpeechBubble"){
                if(Quality > 0  ) transform.gameObject.SetActive(true);
                else transform.gameObject.SetActive(false);
            } 
            //
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //barImage = transform.Find("Bar").GetComponent<Image>();
        //barImage.fillAmount = Quality/100;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
