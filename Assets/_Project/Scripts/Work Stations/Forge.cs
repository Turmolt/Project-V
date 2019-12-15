using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Forge : WorkStation
{
    public Image Display;
    public Image GoodMeter;
    public Image BadMeter;
    public CanvasGroup DisplayCG;

    private float duration = 0f;

    private float target = 7f;

    private float damaged = 10f;
    private float barBuffer = 1f;

    void Start()
    {
        GoodMeter.fillAmount = 1.0f - (target / (damaged + barBuffer));
        BadMeter.fillAmount = 1.0f - (damaged / (damaged + barBuffer));
        DisplayCG.alpha = 0f;
    }

    public bool CanLoad(Item item)
    {
        return InMachine == null && item.ItemState == Item.State.Normal;
    }

    public override void LoadItem(Item item)
    {
        duration = -.5f; //slight delay
        Display.fillAmount = 0f;
        base.LoadItem(item);
        DisplayCG.DOFade(1f, 0.25f);
    }

    public override Item PopItem()
    {
        DisplayCG.DOFade(0f, 0.25f);
        if (InMachine.ItemState != Item.State.Heated) loadedSpriteRenderer.color = Color.white;
        return base.PopItem();
    }

    void Update()
    {
        if (InMachine != null)
        {
            HeatItem();
        }
    }

    void HeatItem()
    {
        duration += Time.deltaTime;

        var t = duration / (damaged+barBuffer);
        loadedSpriteRenderer.color = Color.Lerp(Color.white, Color.red, t);
        Display.fillAmount = Mathf.Clamp01(t);

        if (duration >= target && InMachine.ItemState!=Item.State.Heated)
        {
            InMachine.ItemState = Item.State.Heated;
            InMachine.SwingsLeft = 3;
            Debug.Log("[Forge]: Ding! Your fries are ready");
        }

        if (duration >= damaged)
        {
            InMachine.SetQuality( Mathf.Clamp(InMachine.Quality-0.2f,0f,10f));
        }

    }
}
