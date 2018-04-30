using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Global_var {
	public static int int_count;
	public static bool over;
}

public class Game : MonoBehaviour {
	public Text timer;
	public Text count;
	public GameObject panel_menu;

	private int int_timer;

	void Start () {
		int_timer = 0;
		Global_var.over = false;
		Global_var.int_count = 0;
		StartCoroutine (Timer ());
	}

	void Update () {
		timer.text = int_timer.ToString ();
		count.text = Global_var.int_count.ToString ();
		if (Global_var.over) {
			timer.text = "Game Over. Press [Esc]";
		}
	}

	private IEnumerator Timer()
	{
		while (!Global_var.over) {
			yield return new WaitForSeconds (1.0f);
			int_timer++;
		}
	}
}
