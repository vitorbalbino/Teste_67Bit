using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsPile : MonoBehaviour
{
    [SerializeField] GameObject _pileRoot;
    [SerializeField] float _pileGrowtStep;

    List<GameObject> _items = new List<GameObject> (8);

    Vector3[] _pileRootPosPerFrame;
    float[] _pileRootYRotPerFrame;

    int _animationFrames = 50;
    int _itemsAnimatedPerSecond = 5;
    int _LastPositionIndex = 0;

    int _pileLimit = 5;


    private void Awake()
    {
        _pileRootPosPerFrame = new Vector3[1];
        _pileRootYRotPerFrame = new float[1];
    }


    public bool TryAddItemToPile(GameObject item)
    {
        if (_items.Count >= _pileLimit) { return false; }

        if (! _items.Contains(item))
        {
            item.transform.parent = null;

            item.transform.position = (_items.Count == 0) ? _pileRoot.transform.position : (_items.Last().transform.position + new Vector3(0, _pileGrowtStep, 0));

            _items.Add(item);

            var pilePosSize = (_animationFrames * _items.Count / _itemsAnimatedPerSecond);

            if (_pileRootPosPerFrame.Length < pilePosSize)
            {
                var newPileRootPosPerFrame = new Vector3[pilePosSize];
                _pileRootPosPerFrame.CopyTo(newPileRootPosPerFrame, 0);
                _pileRootPosPerFrame = newPileRootPosPerFrame;


                var newPileRootYRotPerFrame = new float[pilePosSize];
                _pileRootYRotPerFrame.CopyTo(newPileRootYRotPerFrame, 0);
                _pileRootYRotPerFrame = newPileRootYRotPerFrame;
            }

            return true;
        }

        return false;
    }



    public void FixedUpdate()
    {
        {
            _LastPositionIndex ++;

            if (_LastPositionIndex >= _pileRootPosPerFrame.Count())
            {
                _LastPositionIndex = 0;
            }

            _pileRootPosPerFrame[_LastPositionIndex] = _pileRoot.transform.position;
            _pileRootYRotPerFrame[_LastPositionIndex] = _pileRoot.transform.rotation.eulerAngles.y;
        }


        {
            if (_items.Count == 0) return;

            _items[0].transform.position = _pileRoot.transform.position;
            Vector3 rotation = _items[0].transform.rotation.eulerAngles;
            _items[0].transform.rotation = Quaternion.Euler(rotation.x, _pileRootYRotPerFrame[_LastPositionIndex], rotation.z);

            var posIndex = _LastPositionIndex - 1;

            for (int i = 1; i < _items.Count; i++)
            {
                posIndex--;
                if (posIndex < 0)
                {
                    posIndex = _pileRootPosPerFrame.Count() - 1;
                }

                _items[i].transform.position = _pileRootPosPerFrame[posIndex] + new Vector3(0, _pileGrowtStep * i, 0);

                rotation = _items[i].transform.rotation.eulerAngles;
                _items[i].transform.rotation = Quaternion.Euler(rotation.x, _pileRootYRotPerFrame[_LastPositionIndex], rotation.z);
            }
        }
    }



    internal bool TakeItemFromPile(out GameObject npc)
    {
        if (_items.Count > 0)
        {
            npc = _items.Last();
            _items.Remove(npc);
        }
        else
        {
            npc = null;
        }


        return npc != null;
    }



    internal void PoweUpUpdate(int qnt)
    {
        _pileLimit = 5 + qnt;
    }



    internal int GetPileLimit()
    {
        return _pileLimit;
    }
}
