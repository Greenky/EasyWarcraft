using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	[SerializeField] private int _heath;
	[SerializeField] private Animator _annimator = null;
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

	private void Update()
	{
		if (transform.name == "TownHall" && transform.tag == "Orc")
		{
			RaycastHit2D hit = Physics2D.CircleCast(transform.position, 3, Vector2.up, 3, 1 << 8);
			GameObject.Find("GameLogic").GetComponent<AILogic>().AttackEnemy(hit);
		}
	}

	public void Hit(int val)
	{
		

		_heath -= val;
		if (_heath < 0)
		{
			_heath = 0;
			transform.tag = "Untagged";
			transform.gameObject.layer = 0;
			if (_annimator)
			{
				_annimator.SetBool("Dead", true);
			}
			if (transform.name == "TownHall")
			{
				if (transform.tag == "Human")
					Debug.Log("The Orc Team wins.");
				else
					Debug.Log("The Human Team wins.");
			}
			StartCoroutine(DeathTime());
		}
		Vector3 newScale = _helthBar.transform.localScale;
		Vector3 newPos = _helthBar.transform.position;
		newScale.x = (float)_heath / (float)_healthMax * _hplen;
		_helthBar.transform.localScale = newScale;
		//Debug.Log(transform.tag + " Unit [" + _heath + "/" + _healthMax + "]HP has been attacked.");
	}


	private IEnumerator DeathTime()
	{
		yield return new WaitForSeconds(1);
		Destroy(gameObject);
	}
}
