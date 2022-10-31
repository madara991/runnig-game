using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respown : MonoBehaviour
{

	public Transform[] positions;
	public GameObject prefabeEnemy;
	public float TimeRespown;
	float timeBetween;
	private UIwork UI;

	private void Start()
	{
		UI = GameObject.Find("UI work").GetComponent<UIwork>();
	}
	void Update()
    {
		if(UI.isButStartGame == true)
		{
			if (timeBetween <= 0)
			{

				timeBetween = TimeRespown;
				Respown_();
			}
			else timeBetween -= Time.deltaTime;
		}	
    }
	void Respown_()
	{
		int randomTime_1 = Random.Range(0, positions.Length);
		int randomTime_2 = Random.Range(0, positions.Length);

		Quaternion FlipEnemy = new Quaternion(positions[randomTime_2].rotation.x, positions[randomTime_2].rotation.y + 180f, positions[randomTime_2].rotation.z, 1f);
		
		Vector3 PositionUpGround = new Vector3(positions[randomTime_1].position.x, positions[randomTime_1].position.y + 2f, positions[randomTime_1].position.z);
		Vector3 PositionDownGround = new Vector3(positions[randomTime_2].position.x, positions[randomTime_2].position.y - 2f, positions[randomTime_2].position.z);

		Instantiate(prefabeEnemy, PositionUpGround, Quaternion.identity);
		Instantiate(prefabeEnemy, PositionDownGround, FlipEnemy);
	}
	
}
