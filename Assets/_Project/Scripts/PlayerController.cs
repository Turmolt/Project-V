using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackwardsCap
{
    public class PlayerController : MonoBehaviour
    {
        public Rigidbody2D PlayerRB;
        public PlayerAnimator PlayerAnimator;
        private float speed = 10f;

        [HideInInspector] public bool HasControl = true;

        private Vector2 movement;


        void Update()
        {
            if (!HasControl)
            {
                PlayerRB.velocity = Vector2.zero;
                return;
            }

            Movement();
        }



        private void Movement()
        {
            movement = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                movement.y += 1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movement.y -= 1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                movement.x += 1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movement.x -= 1f;
            }

            PlayerRB.velocity = movement.normalized * speed;
            PlayerAnimator.UpdatePlayerAnimator(movement);
        }



    } 
}
