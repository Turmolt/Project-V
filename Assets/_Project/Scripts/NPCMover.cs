using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class NPCMover : MonoBehaviour
{
    public Transform TablePosition;
    public Transform Pos1;
    [CanBeNull] public Transform Pos2;
    public Transform OutPosition;

    public NPC Npc;

    private bool moving = false;

    public Animator Animate;

    public void MoveOut()
    {
        //Start at table, move to Pos 1, then maybe pos 2, then out. reset, then move back in
        if (!moving)
        {
            transform.DOMove(Pos1.position, 2f).OnComplete(() =>
            {
                if (Pos2 != null)
                {
                    transform.DOMove(Pos2.position, 2f).OnComplete(() =>
                        {
                            transform.DOMove(OutPosition.position, 2f).OnComplete(action: () => ResetAndEnter());
                        });
                }
            });
        }
    }

    public void ResetAndEnter()
    {
        
    }
}
