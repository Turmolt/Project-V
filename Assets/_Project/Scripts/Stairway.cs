using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace BackwardsCap
{
    public class Stairway : MonoBehaviour
    {
        public Transform Destination;

        public PlayerController Player;

        public CanvasGroup FadeToBlack;

        public AudioClip MusicChoice;

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                Travel();
            }
        }


        void Travel()
        {
            Player.HasControl = false;
            FadeToBlack.DOFade(1f, 1f).OnComplete(() =>
            {
                Player.transform.position = Destination.position;
                if(MusicChoice)SoundManager.instance.PlaySomething(MusicChoice);
                
                FadeToBlack.DOFade(0f, 1f).OnComplete(() =>
                 {
                     Player.HasControl = true;

                 });
            });
        }
    } 
}
