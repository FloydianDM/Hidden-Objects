using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hidden_Objects.Core
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _sampleObject; // to get standard dimensions of the object
        [SerializeField] private List<GameObject> _objectPool;
        [SerializeField] private Vector2[] _spawnRange = new Vector2[2];
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private int _spawnCount = 10;

        private Vector2 _objectDimensions;
        private Collider2D[] _objectBuffer = new Collider2D[1];
        private List<int> _spawnedIndexes = new();

        private void Start()
        {
            _objectDimensions = _sampleObject.GetComponent<BoxCollider2D>().size;
            SpawnObjects();
        }

        private void SpawnObjects()
        {
            int spawnedObjectCount = 0;

            while (true)
            {
                int randomPoolIndex = UnityEngine.Random.Range(0, _objectPool.Count);

                if (!_spawnedIndexes.Contains(randomPoolIndex))
                {
                    Instantiate(_objectPool[randomPoolIndex], GetSpawnPoint(), Quaternion.identity, transform.parent);
                    spawnedObjectCount ++;
                    _spawnedIndexes.Add(randomPoolIndex);
                }

                if (spawnedObjectCount == _spawnCount)
                {
                    break;
                }
            }
        }

        private Vector2 GetSpawnPoint()
        {
            float x;
            float y;

            while (true)
            {
                x = UnityEngine.Random.Range(_spawnRange[0].x, _spawnRange[0].y);
                y = UnityEngine.Random.Range(_spawnRange[1].x, _spawnRange[1].y);

                Vector2 spawnPoint = new Vector2(x, y);

                int numColliders = Physics2D.OverlapBoxNonAlloc(spawnPoint, _objectDimensions, 0, _objectBuffer, _layerMask);

                if (numColliders == 0)
                {
                    return spawnPoint;
                }
            }
        }
    }
}

