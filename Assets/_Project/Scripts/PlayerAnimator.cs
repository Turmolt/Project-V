using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackwardsCap
{
    public class PlayerAnimator : MonoBehaviour
    {
        enum AnimatorState { Left, Right, Forward, Back, Idle }

        public PlayerController Player;

        public Animator Animator;

        private AnimatorState LastStatePressed;

        void Update()
        {
            if (!Player.HasControl)
            {
                SetBools(AnimatorState.Idle);
                return;
            }
            DetectAnimationKeys();
        }

        void DetectAnimationKeys()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                LastStatePressed = AnimatorState.Back;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                LastStatePressed = AnimatorState.Forward;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                LastStatePressed = AnimatorState.Left;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                LastStatePressed = AnimatorState.Right;
            }
        }

        public void UpdatePlayerAnimator(Vector2 movement)
        {
            var left = movement.x < 0;
            var right = movement.x > 0;
            var forward = movement.y < 0;
            var back = movement.y > 0;

            if ((LastStatePressed == AnimatorState.Left && !left) ||
                (LastStatePressed == AnimatorState.Right && !right) ||
                (LastStatePressed == AnimatorState.Forward && !forward) ||
                (LastStatePressed == AnimatorState.Back && !back))
            {
                UpdateLastState(movement);
            }

            if (left && LastStatePressed == AnimatorState.Left) SetBools(AnimatorState.Left);
            else if (right && LastStatePressed == AnimatorState.Right) SetBools(AnimatorState.Right);
            else if (forward && LastStatePressed == AnimatorState.Forward) SetBools(AnimatorState.Forward);
            else if (back && LastStatePressed == AnimatorState.Back) SetBools(AnimatorState.Back);
            else SetBools(AnimatorState.Idle);

        }

        void UpdateLastState(Vector2 movement)
        {
            var left = movement.x < 0;
            var right = movement.x > 0;
            var forward = movement.y < 0;
            var back = movement.y > 0;

            if (left) LastStatePressed = AnimatorState.Left;
            if (right) LastStatePressed = AnimatorState.Right;
            if (forward) LastStatePressed = AnimatorState.Forward;
            if (back) LastStatePressed = AnimatorState.Back;
        }

        void SetBools(AnimatorState state)
        {
            Animator.SetBool("Left Walk", state == AnimatorState.Left);
            Animator.SetBool("Right Walk", state == AnimatorState.Right);
            Animator.SetBool("Back Walk", state == AnimatorState.Back);
            Animator.SetBool("Forward Walk", state == AnimatorState.Forward);
            Animator.SetBool("Idle", state == AnimatorState.Idle);
        }
    } 
}
