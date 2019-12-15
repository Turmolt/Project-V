﻿using System.Collections;
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

        public GameObject Skeleton;

        public Camera IntroCamera;

        public Camera GameCamera;

        public GameObject logo;
        



       

        private IEnumerator WaitForIntro()
        {
            PlayerRenderer.enabled = false;
            yield return new WaitForSeconds(IntroClip.length);
            Travel1();
        }

        private IEnumerator WaitForTitle()
        {
            logo.SetActive(true);
            yield return new WaitForSeconds(5f);
            logo.SetActive(false);
            Travel2();
        }

        private void Awake()
        {
            StartCoroutine(WaitForIntro());
        }

        void Travel1()
        {
            Player.HasControl = false;
            FadeToBlack.DOFade(1f, 1f).OnComplete(() =>
            {
                Player.transform.position = Destination.position;
            });
            
            StartCoroutine(WaitForTitle());
        }

        void Travel2()
        {

            IntroCamera.enabled = false;
            GameCamera.enabled = true;
            SoundManager.instance.PlaySomething(MusicChoice);
            FadeToBlack.DOFade(0f, 1f).OnComplete(() =>
            {
                PlayerRenderer.enabled = true;
                Player.HasControl = true;
                Skeleton.SetActive(false);

            });

        }
    }
}