using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace BackwardsCap
{
    public class IntroControl : MonoBehaviour
    {
        public Transform Destination;

        public PlayerController Player;

        public SpriteRenderer PlayerRenderer;

        public CanvasGroup FadeToBlack;

        public AudioClip MusicChoice;

        public AudioClip IntroClip;

       

        private IEnumerator WaitForIntro()
        {
            PlayerRenderer.enabled = false;
            yield return new WaitForSeconds(IntroClip.length);
            Travel();
        }

        private void Awake()
        {
            StartCoroutine(WaitForIntro());
        }

        void Travel()
        {
            Player.HasControl = false;
            FadeToBlack.DOFade(1f, 1f).OnComplete(() =>
            {
                Player.transform.position = Destination.position;
                SoundManager.instance.PlaySomething(MusicChoice);
                FadeToBlack.DOFade(0f, 1f).OnComplete(() =>
                {
                    PlayerRenderer.enabled = true;
                    Player.HasControl = true;

                });
            });
        }
    }
}