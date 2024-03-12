using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCollector : MonoBehaviour
{
    [SerializeField] FillArea _fillArea;
    [SerializeField] float _CollectItemTime = 0.3f;
    [SerializeField] float _timer = 0;

    [SerializeField] float _paymentPriceForNpc = 1f;



    void FixedUpdate()
    {
        if (_fillArea.IsFilled())
        {
            _timer += Time.fixedDeltaTime;

            if ( _timer > _CollectItemTime)
            {
                CollectNPC();
                _timer = 0;
            }
        }
        else
        {
            _timer = 0;
        }
    }



    private void CollectNPC()
    {
        if (Player._Sgt.TrySellItem(_paymentPriceForNpc, out GameObject npc))
        {
            Destroy(npc);
        }
    }
}
