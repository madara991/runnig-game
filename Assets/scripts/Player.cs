using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody rb;
	private Animator animator;
	public float speedForword;
	public float speedRightAndLeft;
	public float speedCamearaRot;
	[Range(-9.8f,-25f)]
	public float forcGravity = -9.8f;
	public int Health;
	private UIwork UI;
	[SerializeField] private GameObject bag;
	private GameObject mainCamera;

	// Movemnet values
	bool isLeft;
	bool isRight;
	bool isRotCamearaLeft;
	bool isRotCamearaRight;
	Vector3 RotVector;
	bool isOnGround;
	float Rot = 180f;

	void Start()
    {
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		UI = GameObject.Find("UI work").GetComponent<UIwork>();

		RotVector = new Vector3(0f, 0f, 180f);
		Physics.gravity = new Vector3(0f, forcGravity, 0f);

	}
	void FixedUpdate()
	{
		CheckGround();
	}
	void Update()
	{
		
		if (UI.isButStartGame == true)
		{
			if (Input.GetKey(KeyCode.A))
			{
				transform.position +=transform.TransformDirection(Vector3.left) * speedRightAndLeft * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.D))
			{
				transform.position += transform.TransformDirection(Vector3.right) * speedRightAndLeft * Time.deltaTime;
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				isRotCamearaLeft = true;
			}

			if (Input.GetKeyDown(KeyCode.E))
			{
				isRotCamearaRight = true;
			}
			
			FlipGravity();

			animator.enabled = true;
			if (isOnGround == true)
			{
				animator.SetBool("fall", false);
				

			}
			else { animator.SetBool("fall", true);  }
			
		}

		effectBag();
		Death();
	}
	void FlipGravity()
	{
		if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
		{
			Physics.gravity = new Vector3(0f, Physics.gravity.y * -1f, 0f);
		}
		if (isRotCamearaLeft == true)
		{
			isRotCamearaRight = false;
			
			testCircle();
		}
		if(isRotCamearaRight == true)
		{
			isRotCamearaLeft = false;
			
			testCircle_Riht();
			
		}
		
	}
	void testCircle()
	{
		if(Vector3.Distance(transform.eulerAngles,new Vector3(0f, 0f, Rot)) > 0.1f)
		{
			transform.Rotate(0f, 0f, -speedCamearaRot);
		}
		else
		{
			if(Rot == 180f)
			{
				Rot = 0f;
			}
			else
			{
				Rot = 180f;
			}
			isRotCamearaLeft = false;
		}
	}
	void testCircle_Riht()
	{
		if (Vector3.Distance(transform.eulerAngles, new Vector3(0f, 0f, Rot)) > 0.1f)
		{
		     transform.Rotate(0f, 0f, speedCamearaRot);
		}
		else
		{
			if (Rot == 180f)
			{
				Rot = 0f;
			}
			else
			{
				Rot = 180f;
			}
			isRotCamearaRight = false;
		}
	}
	public void TakeDamage(int damage)
	{
		Health -= damage;
		UI.healthBar(Health);
	}

	void Death()
	{
		if(Health <= 0 || transform.position.y > 40f || transform.position.y < -40f)
		{
			animator.SetBool("die", true);
			Invoke("ShowRestart", 1f);
		}
	}
	void ShowRestart()
	{
		UI.showRestart();
	}
	
	void CheckGround()
	{
		
		if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.down),0.1f))
		{
			isOnGround = true;
		}
		else
		{
			isOnGround = false;
		}
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{

			TakeDamage(10);
			animator.SetTrigger("collision");
			Destroy(collision.gameObject);
		}
		Debug.Log(collision.gameObject.name);
	}
	void effectBag()
	{
		Debug.Log(transform.eulerAngles.z);
		if(transform.eulerAngles.z >= 170f) // bug effect
		{
			bag.GetComponentInChildren<ParticleSystem>().Play(); // particl bag
			bag.transform.Find("Point Light 1").gameObject.SetActive(true);
			bag.transform.Find("Point Light 2").gameObject.SetActive(true);
		}
		else
		{
			bag.GetComponentInChildren<ParticleSystem>().Pause();
			bag.transform.Find("Point Light 1").gameObject.SetActive(false);
			bag.transform.Find("Point Light 2").gameObject.SetActive(false);
		}
	}
}
