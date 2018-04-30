using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
	public float speed;

	private GameObject target;
	private Vector3 mvt;
	private float step;
	private Animation anim;
	private bool alive = true;

	private void Start ()
	{
		anim = gameObject.GetComponent<Animation> ();
		anim ["PA_WarriorForward_Clip"].speed = 2.0f;
	}

	private void Update () 
	{
		if (alive)
			Move ();
	}

	private void Move()
	{
		target = GameObject.FindGameObjectWithTag ("Player");
		mvt = target.transform.position;
		step = speed * Time.deltaTime;
		gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, mvt, step);
		gameObject.transform.LookAt (target.transform);
	}

	public void Death()
	{
		alive = false;
		anim.Play ("PA_WarriorDeath_Clip");
		Global_var.int_count++;
		Destroy (gameObject, 5.0f);
	}

	private void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.tag == "Fire" && alive) {
			Death ();
		}
	}
}
