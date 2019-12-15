using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] frameArray;
    private int currentFrame;
    private float timer;
    private float persistantTimer;
    private AudioClip radioClip;


    private void Awake()
    {
        radioClip = GetComponent<AudioSource>().clip;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        persistantTimer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer -= 1f;
            currentFrame = (currentFrame + 1) % frameArray.Length;
            gameObject.GetComponent<SpriteRenderer>().sprite = frameArray[currentFrame];
        }
        if (radioClip.length < persistantTimer)
        {
            this.gameObject.SetActive(false);
        }
    }
}
