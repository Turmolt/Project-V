using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BackwardsCap
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;

        public Text ScoreDisplay;
        public Text ServedDisplay;

        public CanvasGroup ScoreScreenCG;
        public Text ScoreScreenDisplay;

        public CanvasGroup EndScreenCG;

        public PlayerController Player;
        public CanvasGroup UICG;

        private float score;
        private int served;

        private int targetServed = 20;

        public void Awake()
        {
            if (instance == null) instance = this;
            ScoreDisplay.text = score.ToString();
            ServedDisplay.text = $"{served:00}/{targetServed}";
            EndScreenCG.alpha = 0f;
            ScoreScreenCG.alpha = 0f;
        }

        public void AddPoints(float points)
        {
            if (points == 0) return;
            served += 1;
            ServedDisplay.text = $"{served:00}/{targetServed}";
            score += points;
            ScoreDisplay.text = score.ToString();

            if (served == targetServed)
            {
                Player.HasControl = false;
                UICG.DOFade(0f,0.5f);
                ScoreScreenDisplay.text = score.ToString();
                ScoreScreenCG.DOFade(1.0f, 1.0f);


            }
        }

        IEnumerator WaitThenThank()
        {
            yield return new WaitForSeconds(5.0f);
            EndScreenCG.DOFade(1.0f, 1.0f);
        }

    } 
}
