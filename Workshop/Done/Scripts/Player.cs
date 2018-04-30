using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	public GameObject bullet;
	public Transform bullet_spawn;
	public GameObject[] weapons;
	public AudioSource weapons_sound;

	private GameObject weapon_active;
	private bool can_shoot = true;

	private Camera child_camera;
	private RaycastHit	hit;
	private Ray ray;
	private int layer = 1 << 8;

	private void Start()
	{
		weapon_active = weapons [0];
		child_camera = gameObject.GetComponentInChildren<Camera> ();
		Debug.Log (child_camera.name);
	}

	private void Update()
	{
		if (Input.GetKeyDown (KeyCode.A)) {
			Switch_weapon ();
		}
		if (Input.GetKey (KeyCode.Mouse0) && can_shoot) {
			StartCoroutine(Fire ());
		}
		if (Input.GetKeyDown (KeyCode.Escape) && Global_var.over) {
			SceneManager.LoadScene ("Menu");
		}
	}

	private void Switch_weapon()
	{
		if (weapon_active.name == "Gun") {
			weapon_active = weapons [1];
			weapons [0].SetActive (false);
		} else if (weapon_active.name == "Gun2") {
			weapon_active = weapons [0];
			weapons [1].SetActive (false);
		}
		weapon_active.SetActive (true);
	}

	private IEnumerator Fire()
	{
		/* ------------- Rigibody technique (with bullet gravity)
		GameObject bullet_tmp = Instantiate (bullet, bullet_spawn.position, bullet_spawn.rotation) as GameObject;

		bullet_tmp.GetComponent<Rigidbody> ().velocity = bullet_tmp.transform.forward * 100;
		can_shoot = !can_shoot;
		weapons_sound.Play ();
		weapon_active.GetComponent<Animation> ().Play ();
		yield return new WaitForSeconds ((weapon_active.name == "Gun") ? 0.75f : 0.1f);
		can_shoot = !can_shoot;
		Destroy (bullet_tmp, 4.0f);
		*/
		//-------------- Raycasting technique (powerfull)
		can_shoot = !can_shoot;
		weapons_sound.Play ();
		weapon_active.GetComponent<Animation> ().Play ();
		ray = child_camera.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, layer))
			hit.collider.gameObject.GetComponent<enemy> ().Death ();
		yield return new WaitForSeconds ((weapon_active.name == "Gun") ? 0.75f : 0.1f);
		can_shoot = !can_shoot;
	}

	private void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.tag == "Finish") {
			Global_var.over = true;
		}
	}
}
