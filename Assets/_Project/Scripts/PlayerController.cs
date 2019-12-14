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
        private Camera mainCam;

        [HideInInspector] public bool HasControl = true;

        public Inventory Inventory;

        private Vector2 movement;

        public void Start()
        {
            mainCam = Camera.main;
        }

        void Update()
        {
            if (!HasControl)
            {
                PlayerRB.velocity = Vector2.zero;
                return;
            }

            MouseHandler();

            Movement();
        }

        void MouseHandler()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var wp = mainCam.ScreenToWorldPoint(Input.mousePosition);
                Ray r = mainCam.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(wp, Vector2.zero);

                if (hit.transform != null)
                {
                    if (hit.transform.CompareTag("Item"))
                    {
                        var item = hit.transform.GetComponent<Item>();
                        Inventory.PickupItem(item);
                    }
                }
            }
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
