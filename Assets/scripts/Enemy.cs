using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed;
	private UIwork ui;
	private Player player;
	void Start()
	{
		ui = GameObject.Find("UI work").GetComponent<UIwork>();
		player = GameObject.Find("Player").GetComponent<Player>();
	}
	void Update()
	{
		if(player.Health <= 0)
		{
			speed = 0f;
		}
		if (ui.isButStartGame == true)
		{
			transform.position += Vector3.forward * -speed * Time.deltaTime;
			Destroy(gameObject, 20f);
		}
	}
}
