using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableNPC : MonoBehaviour
{
    [SerializeField] PlayerDistanceChecker m_DistanceChecker;
    [SerializeField] GameObject _ItemNpc;

    private void FixedUpdate()
    {
        if (m_DistanceChecker.PlayerIsClose())
        {
            if (Player._Sgt.TryReceiveItem(_ItemNpc))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
