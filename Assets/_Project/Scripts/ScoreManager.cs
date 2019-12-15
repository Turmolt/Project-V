using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BackwardsCap
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;

        public Text ScoreDisplay;
        public Text ServedDisplay;

        private float score;
        private int served;

        private int targetServed = 20;

        public void Awake()
        {
            if (instance == null) instance = this;
            ScoreDisplay.text = score.ToString();
            ServedDisplay.text = $"{served:00}/{targetServed}";
        }

        public void AddPoints(float points)
        {
            served += 1;
            ServedDisplay.text = $"{served:00}/{targetServed}";
            score += points;
            ScoreDisplay.text = score.ToString();
        }

    } 
}
