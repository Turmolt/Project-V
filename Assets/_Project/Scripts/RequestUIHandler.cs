using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RequestUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Expand(){
        transform.DOMoveY(-500,0.5f);
    }
    public void Retract(){
        transform.DOMoveY(50,0.5f);
    }
}
