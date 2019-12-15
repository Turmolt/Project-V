using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BackwardsCap
{
    public class Anvil : WorkStation
    {
        public Slider GameSlider;
        public AnvilScoreDisplay ScoreDisplay;
        public CanvasGroup DisplayCG;
        private float speed = 1.5f;

        private float highScore = .05f;
        private float highQualityPlus = 2f;
        private float midScore = .15f;
        private float midQualityPlus = 1f;
        private float cursor = 0f;

        void Start()
        {
            ScoreDisplay.SetRange(highScore,midScore);
            DisplayCG.alpha = 0f;
        }

        public override void LoadItem(Item item)
        {
            base.LoadItem(item);
            if(InMachine.ItemState == Item.State.Heated) DisplayCG.DOFade(1f, 0.25f);
        }

        public override Item PopItem()
        {
            DisplayCG.DOFade(0f, 0.25f);
            return base.PopItem();
        }

        void Update()
        {
            if (InMachine != null && InMachine.SwingsLeft > 0 && InMachine.ItemState==Item.State.Heated)
            {
                UpdateGame();
            }
        }

        public void Hammer()
        {
            if (InMachine.SwingsLeft > 0)
            {
                InMachine.SwingsLeft--;
                var c = Mathf.Abs(cursor - .5f);
                if (c <= highScore)
                {
                    Debug.Log($"[Anvil]: +{highQualityPlus}");
                    InMachine.SetQuality(InMachine.Quality + highQualityPlus);
                }
                else if (c <= midScore)
                {
                    InMachine.SetQuality(InMachine.Quality+midQualityPlus);
                    Debug.Log($"[Anvil]: +{midQualityPlus}");
                }
                else Debug.Log($"[Anvil]: Missed!");

                if (InMachine.SwingsLeft == 0)
                {
                    DisplayCG.DOFade(0f, .5f);
                    InMachine.ItemState = Item.State.Hammered;
                }
            }
        }

        void UpdateGame()
        {
            cursor = (1.0f + Mathf.Cos(Time.time * speed)) / 2f;
            GameSlider.normalizedValue = cursor;
        }
    } 
}
