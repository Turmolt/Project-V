using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BackwardsCap
{
    public class AnvilScoreDisplay : MonoBehaviour
    {
        public Image[] HighScoreRange;
        public Image[] MidScoreRange;
        
        public void SetRange(float HighScore, float MidScore)
        {
            for (int i = 0; i < HighScoreRange.Length; i++)
            {
                HighScoreRange[i].fillAmount = .5f - HighScore;
                MidScoreRange[i].fillAmount = .5f - MidScore;
            }
        }
    } 
}
