using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class MovingScript : MonoBehaviour
{
    [SerializeField]private float _speed = 2;
    [SerializeField]private Animator _animator;
    [SerializeField]private AudioSource[] _audio;        
    private Vector2 _targetPos;
    private Transform _enemy = null;
	[SerializeField] private bool _canMove = true;

	private void Start()
	{
		_enemy = null;
		_targetPos = transform.position;
	}

	void Update()
	{
		Vector2 pos = transform.position;
		if (pos != _targetPos && _canMove)
			transform.position = Vector2.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);
		else
			_animator.SetBool("Walking", false);

		if (_enemy)
			_targetPos = _enemy.position;
	}


	public void MoveTo(Vector2 target)
	{
		_audio[Random.Range(0, 4)].Play();
		_enemy = null;
		_canMove = true;
		_animator.SetBool("Atack", false);
		Vector2 pos = transform.position;
		Vector2 dir;
		_targetPos = target;
		dir = _targetPos - pos;
		_animator.SetBool("Walking", true);
		if (dir.x < 0)
			GetComponent<SpriteRenderer>().flipX = true;
		else
			GetComponent<SpriteRenderer>().flipX = false;
		_animator.SetFloat("X", dir.normalized.x);
		_animator.SetFloat("Y", dir.normalized.y);
	}


	public void Attack(Transform targetEnemy)
	{
		MoveTo(targetEnemy.position);
		_enemy = targetEnemy;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform == _enemy)
		{
			_animator.SetBool("Walking", false);
			_canMove = false;
			StartCoroutine(AttakingEvent());
		}
	}


	private IEnumerator AttakingEvent()
	{
		_animator.SetBool("Atack", true);
		yield return new WaitForSeconds(0.5f);
		if (_enemy)
		{
			_enemy.GetComponent<HealthManager>().Hit(10);
			Vector2 pos = transform.position;
			RaycastHit2D hit = Physics2D.Raycast(transform.position, _targetPos - pos, 1, 1 << 9);
			if (hit.collider != null && hit.collider.transform == _enemy)
			{
				StartCoroutine(AttakingEvent());
			}
			else
			{
				_animator.SetBool("Atack", false);
				_canMove = true;
			}
		}
		else
		{
			_animator.SetBool("Atack", false);
			_canMove = true;
		}
	}
}
