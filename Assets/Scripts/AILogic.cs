using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILogic : MonoBehaviour
{
    [SerializeField] List<GameObject> bots = new List<GameObject>();
    private GameObject _enemy = null;
    private GameObject[] _orcs;
    [SerializeField] private bool _hallatacked = false;

    private void Update()
    {
        GameObject[] oldOrcs = _orcs;
        _orcs = GameObject.FindGameObjectsWithTag("Orc");
        
        if (!_hallatacked && (_enemy == null || _orcs.Length != oldOrcs.Length))
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Human");
            if (enemys != null)
            {
                _enemy = enemys[Random.Range(0, enemys.Length)];
                Atack();
            }
        }
    }

    void Atack()
    {
        if (_enemy)
            foreach (var orc in _orcs)
            {
                if (orc.GetComponent<MovingScript>())
                    orc.GetComponent<MovingScript>().Attack(_enemy.transform);
            }
    }

    public void AttackEnemy(RaycastHit2D enemy)
    {
        if (enemy.transform)
        {
            _hallatacked = true;
            _enemy = enemy.transform.gameObject;
            Atack();
        }
        else
        {
            _hallatacked = false;
        }
        
    }
}
