using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RequestUIHandler : MonoBehaviour
{
    private float startY;
    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Expand(){
        transform.DOMoveY(startY-520,0.5f);
    }
    public void Retract(){
        transform.DOMoveY(startY,0.5f);
    }
}
