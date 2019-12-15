using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

        private float maxPickupDistance = 2f;

        public AudioSource SoundEffectAS;

        [Header("Audio Clips")]
        public AudioClip ItemPickupAudio;
        public AudioClip CantPickup;

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

        void PickupItem()
        {
            var rayPos = new Vector3(transform.position.x, transform.position.y, -5f) + InteractDirection();

            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 100f, LayerMask.GetMask("WorkStations"));
            if (hit.transform != null && hit.transform.CompareTag("WorkStation"))
            {
                var workStation = hit.transform.GetComponent<WorkStation>();
                if (workStation.InMachine != null)
                {
                    Inventory.PickupItem(workStation.PopItem());
                    return;
                }
            }

            hit= Physics2D.Raycast(rayPos, Vector2.zero, 100f, LayerMask.GetMask("Items"));
            if (hit.transform != null && hit.transform.CompareTag("Item"))
            {
                var item = hit.transform.GetComponent<Item>();
                Inventory.PickupItem(item);
                return;
            }

            rayPos = new Vector3(transform.position.x, transform.position.y, -5f);
            hit = Physics2D.Raycast(rayPos, Vector2.zero, 100f, LayerMask.GetMask("Items"));
            if (hit.transform != null && hit.transform.CompareTag("Item"))
            {
                var item = hit.transform.GetComponent<Item>();
                Inventory.PickupItem(item);
                return;
            }
        }

        bool CheckDirection(string dir)
        {
            return PlayerAnimator.Animator.GetCurrentAnimatorStateInfo(0).IsName(dir + " Idle") ||
                   PlayerAnimator.Animator.GetCurrentAnimatorStateInfo(0).IsName(dir + " Walk");
        } 

        public Vector3 InteractDirection()
        {
            if (CheckDirection("Back"))
            {
                return new Vector2(0,1);
            }
            if (CheckDirection("Forward"))
            {
                return new Vector2(0, -1);
            }
            if (CheckDirection("Right"))
            {
                return new Vector2(1, 0);
            }
            if (CheckDirection("Left"))
            {
                return new Vector2(-1, 0);
            }

            Debug.LogError("[PlayerController]: No interaction direction found!");
            return Vector3.zero;
        }

        
        void MouseHandler()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var wp = mainCam.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(wp, Vector2.zero, 100f, LayerMask.GetMask("WorkStations"));
                if (hit.transform != null)
                {
                    var workStation = hit.transform.GetComponent<WorkStation>();
                    if (workStation.InMachine != null)
                    {
                        Inventory.PickupItem(workStation.PopItem());
                        return;
                    }
                }

                hit = Physics2D.Raycast(wp, Vector2.zero, 100f, LayerMask.GetMask("Items"));

                if (hit.transform != null)
                {
                    if (hit.transform.CompareTag("Item"))
                    {
                        var distance = Vector3.Distance(transform.position.xy(0), hit.transform.position.xy(0));
                        if (distance < maxPickupDistance)
                        {
                            var item = hit.transform.GetComponent<Item>();
                            if (Inventory.PickupItem(item))
                            {
                                SoundManager.instance.ClipArrayVariation(ItemPickupAudio);
                            }
                            else
                            {
                                SoundManager.instance.ClipArrayVariation(CantPickup);
                            }
                        }
                        else
                        {
                            if (!Inventory.HoveringOverInventory)
                            {
                                Debug.Log($"[PlayerController]: Item too far to pick up! {distance}m away");

                                SoundManager.instance.ClipArrayVariation(CantPickup);
                            }
                        }
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                var wp = mainCam.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(wp, Vector2.zero, 100f, LayerMask.GetMask("WorkStations"));
                if (hit.transform != null && hit.transform.CompareTag("Anvil"))
                {
                    var anvil = hit.transform.GetComponent<Anvil>();
                    if (anvil.InMachine != null)
                    {
                        anvil.Hammer();
                        return;
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
