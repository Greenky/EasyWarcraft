using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private GameObject _troop;
    private bool _canSpawn = false;

    void Start()
    {
        if (_troop)
            StartCoroutine(SpawnTroops());
    }
    
    void Update()
    {
        if (_troop && _canSpawn)
            StartCoroutine(SpawnTroops());
    }

    private IEnumerator SpawnTroops()
    {
        _canSpawn = false;
        yield return new WaitForSeconds(10);
        GameObject newTroop = Instantiate(_troop, transform);
        newTroop.transform.localPosition = new Vector3(0.9f, -1.3f, 0);
        newTroop.transform.parent = null;
        _canSpawn = true;
    }
}
