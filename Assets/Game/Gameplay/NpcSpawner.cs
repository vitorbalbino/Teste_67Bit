using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class NpcSpawner : MonoBehaviour
{
    [SerializeField] Transform _spawnField_1, _spawnField_2;

    [SerializeField] GameObject _npcPrefab;

    [SerializeField] float _spawnTime = 2f;
    float _timer = 0f;

    bool _SpawnedFrom1Last = false;
    float _spawnAreaSize = 10f;



    float SpawnRange()
    {
        return (Random.value * _spawnAreaSize - 5);
    }



    private void SpawnNPC()
    {
        Vector3 Start = new Vector3(SpawnRange(), 0, SpawnRange()), End = new Vector3(SpawnRange(), 0, SpawnRange());

        if (_SpawnedFrom1Last)
        {
            Start += _spawnField_2.position;
            End += _spawnField_1.position;
        }
        else
        {
            Start += _spawnField_1.position;
            End += _spawnField_2.position;
        }

        _SpawnedFrom1Last = !_SpawnedFrom1Last;

        Vector3 walkDirection = End - Start;
        var newNPC = Instantiate(_npcPrefab, Start, Quaternion.LookRotation(walkDirection, Vector3.up), null);

        if (newNPC.TryGetComponent(out CharNpc npc))
        {
            npc.SetEndPoint(End);
        }
    }



    private void FixedUpdate()
    {
        if (_timer < _spawnTime)
        {
            _timer += Time.fixedDeltaTime;
        }
        else
        {
            SpawnNPC();

            _timer = 0f;
        }
    }
}
