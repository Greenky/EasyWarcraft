using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
	public Transform target;
    // Start is called before the first frame update
    void Start()
    {
		
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			transform.GetComponent<MovingScript>().Attack(target);
		}
	}
}
