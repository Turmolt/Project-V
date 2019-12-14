using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] frameArray;
    private int currentFrame;
    private float timer;

    private void Update()
    {
        Debug.Log(currentFrame);
        Debug.Log(timer);
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer -= 1f;
            currentFrame = (currentFrame + 1) % frameArray.Length;
            gameObject.GetComponent<SpriteRenderer>().sprite = frameArray[currentFrame];

        }
    }
}
