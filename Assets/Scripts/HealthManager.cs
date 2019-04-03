using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	[SerializeField] private int _heath;
	[SerializeField] GameObject _hp;
	private int _healthMax;
	private float _hplen;
	private GameObject _helthBar;

	void Start()
	{
		_helthBar = Instantiate(_hp, transform);
		_hplen = _helthBar.transform.localScale.x;
		_healthMax = _heath;
	}

	public void Hit(int val)
	{
		_heath -= val;
		if (_heath < 0)
		{
			_heath = 0;
			if (transform.name == "TownHall")
			{
				if (transform.tag == "Human")
					Debug.Log("The Orc Team wins.");
				else
					Debug.Log("The Human Team wins.");
			}
			Destroy(gameObject);
		}
		Vector3 newScale = _helthBar.transform.localScale;
		Vector3 newPos = _helthBar.transform.position;
		newScale.x = (float)_heath / (float)_healthMax * _hplen;
		_helthBar.transform.localScale = newScale;
		Debug.Log(transform.tag + " Unit [" + _heath + "/" + _healthMax + "]HP has been attacked.");
	}
}
