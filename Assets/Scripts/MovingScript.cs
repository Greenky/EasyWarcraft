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
        private Vector2 _target;

    private void Start()
    {
        _target = transform.position;
    }

    void Update()
    {
        Vector2 pos = transform.position;
        if (pos != _target)
            transform.position = Vector2.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        else
            _animator.SetBool("Walking", false);
    }


    public void moveTo(Vector2 target)
    {
        _audio[Random.Range(0, 4)].Play();
        Vector2 pos = transform.position;
        Vector2 dir;
        _target = target;
        dir = _target - pos;
        _animator.SetBool("Walking", true);
        if (dir.x < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;
        _animator.SetFloat("X", dir.normalized.x);
        _animator.SetFloat("Y", dir.normalized.y);
    }
}
