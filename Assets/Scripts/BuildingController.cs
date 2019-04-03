using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private int _heath;
    [SerializeField] private GameObject _troop;
    private bool _canSpawn = false;
    private int _healthMax;

    void Start()
    {
        _healthMax = _heath;
        if (_troop)
            StartCoroutine(SpawnTroops());
    }
    
    void Update()
    {
        if (_troop && _canSpawn)
            StartCoroutine(SpawnTroops());
    }

    public void Hit(int val)
    {
        _heath -= val;
        if (_heath < 0)
        {
            _heath = 0;
            if (_troop)
            {
                if (transform.tag == "Human")
                    Debug.Log("The Orc Team wins.");
                else
                    Debug.Log("The Human Team wins.");
            }
            Destroy(gameObject);
        }
        Debug.Log(transform.tag + "Unit [" + _heath + "/" + _healthMax + "]HP has been attacked.");
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
