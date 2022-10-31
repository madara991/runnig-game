using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIwork : MonoBehaviour
{

	
	public GameObject PanelRestart;
	public GameObject PanelStart;
	private int healthPlayer;
	public Slider slider;
	public Text textDistance;
	private int score;
	public Text textScore;
	public GameObject ScoreReword;
	float timeBetween;
	float currentDistance,LastDistance;
	float CountScore;
	[HideInInspector]public bool isButStartGame;
	void Start()
	{
		isButStartGame = false;	
	}
	void Update()
	{
		WorkDistance();

	}
	public void StartGame()
	{
		PanelStart.SetActive(false);
		isButStartGame = true;
	}
	public void Restart()
	{
		isButStartGame = true;
		SceneManager.LoadScene(0);
	}
	public void showRestart()
	{
		isButStartGame = false;
		PanelRestart.SetActive(true);

	}
	public void healthBar(float health)
	{
		slider.value = health; 
	}
	void WorkDistance ()
	{
		if(isButStartGame == true)
		{
			textDistance.text =currentDistance.ToString("0");
			currentDistance += (7 * Time.deltaTime);
			if (currentDistance > LastDistance + 100f)
			{
				LastDistance += 100f;

				// Score
				showTextScore();
				Invoke("PLusInvoke", 1.15f);
			}
			
		}
		
	}
	void PLusInvoke()
	{
		textScore.text = (score+1000).ToString();
		score += 1000;
	}
	void showTextScore()
	{
		GameObject clone = Instantiate(ScoreReword, transform.position, Quaternion.identity);
		clone.transform.parent = GameObject.Find("UI work").transform;
	}
	
}
