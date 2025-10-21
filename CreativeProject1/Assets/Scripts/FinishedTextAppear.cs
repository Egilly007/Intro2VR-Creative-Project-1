using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishedTextAppear : MonoBehaviour
{
    public TMP_Text finishedText;

    public PickupDropOff dropOffRef;

    private bool shown = false;

    void Start()
    {
        if (finishedText != null)
            finishedText.gameObject.SetActive(false);
    }
    void Update()
    {
        if (shown)
            return;

        if (dropOffRef != null && dropOffRef.gameDone)
        {
            ShowFinished();
            shown = true;
        }
    }

    private void ShowFinished()
    {
        if (finishedText != null)
            finishedText.gameObject.SetActive(true);
    }
}
