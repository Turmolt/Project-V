using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BackwardsCap
{
    public class Anvil : WorkStation
    {
        public Slider GameSlider;
        public AnvilScoreDisplay ScoreDisplay;
        private float speed = 1.5f;

        private float highScore = .025f;
        private float midScore = .1f;


        void Start()
        {
            ScoreDisplay.SetRange(highScore,midScore);
        }


        void Update()
        {
            
        }

        void UpdateGame()
        {
            GameSlider.normalizedValue = (1.0f + Mathf.Cos(Time.time * speed)) / 2f;
        }
    } 
}
