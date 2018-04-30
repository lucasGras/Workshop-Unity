using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_zone : MonoBehaviour {
	public GameObject enemy;

	private BoxCollider area;
	private Vector3 pos;
	private bool can_spawn = true;

	private void Start()
	{
		area = gameObject.GetComponent<BoxCollider> ();
	}

	private void Update()
	{
		if (can_spawn && !Global_var.over) {
			pos = new Vector3 (gameObject.transform.position.x + Random.Range (-area.size.x, area.size.x),
				gameObject.transform.position.y + Random.Range (-area.size.y, area.size.y),
				gameObject.transform.position.z + area.center.z);
			StartCoroutine (Spawn (pos));
		}
	}

	private IEnumerator Spawn(Vector3 spawn_pos)
	{
		int r = Random.Range (1, 4);

		can_spawn = !can_spawn;
		for (int i = 0; i < r; i++) {
			Instantiate (enemy, spawn_pos, Quaternion.identity);
			spawn_pos.x += (i * enemy.transform.lossyScale.x) + 2;
		}
		yield return new WaitForSeconds (3.0f);
		can_spawn = !can_spawn;
	}
}
