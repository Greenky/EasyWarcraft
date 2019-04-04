using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private List<MovingScript> Bots = new List<MovingScript>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.CircleCast(target, 0.1f, Vector2.up, 0.1f);
            
            if (hit.collider != null)
            {
				if (hit.transform.tag == "Human")
				{
					if (Input.GetKey(KeyCode.LeftControl) == false)
						Bots.Clear();
					//if (NotInList(hit.transform.gameObject))
					Bots.Add(hit.transform.gameObject.GetComponent<MovingScript>());
				}
				else if (hit.transform.tag == "Orc" && Bots.Count != 0)
				{
					foreach (var bot in Bots)
						bot.Attack(hit.transform);
				}
					

			}
			else if (Bots.Count != 0)
            {
				foreach (var bot in Bots)
					bot.MoveTo(target);
                
            }
        }
        else if (Input.GetMouseButtonDown(1))
            Bots.Clear();
    }

    bool NotInList(GameObject go)
    {
        foreach (var bot in Bots)
        {
            if (bot.name == go.name)
                return false;
        }
        return true;
    }
}
