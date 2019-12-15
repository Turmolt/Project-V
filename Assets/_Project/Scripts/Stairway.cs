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

        public AudioSource MusicSource;

        public AudioClip MusicChoice;
        private Manager _manager;

        private void Awake() {
            _manager = GameObject.FindObjectOfType<Manager>();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                StartCoroutine(FadeAudioSource.StartFade(MusicSource, 1f, 0f));
                Travel();
            }
        }


        void Travel()
        {
            Player.HasControl = false;
            if(Destination.name.Contains("Dungeon")){
                _manager.SpawnNewItems();
            }
            
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
