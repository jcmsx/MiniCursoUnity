using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeIA : MonoBehaviour {

	private GameController _GameController;
	private Rigidbody2D slimeRb;
	private Animator slimeAnimator;

	public float speed;
	public float timeToWalk;
	public bool isLookLeft;

	public GameObject hitBox;


	private int h;



	// Use this for initialization
	void Start () {
		_GameController = FindObjectOfType (typeof(GameController)) as GameController;

		slimeRb = GetComponent<Rigidbody2D> ();
		slimeAnimator = GetComponent<Animator> ();
		StartCoroutine ("slimeWalk");

	}
	
	// Update is called once per frame
	void Update () {
		

		if (h > 0 && isLookLeft == true) 
		{
			Flip ();

		} else if (h < 0 && isLookLeft == false) {
			Flip ();
		}

		slimeRb.velocity = new Vector2 (h * speed, slimeRb.velocity.y);


		if (h != 0) 
		{
			slimeAnimator.SetBool ("IsWalk", true);
		} 

		else {
			slimeAnimator.SetBool ("IsWalk", false);
		}


	}

	void OnTriggerEnter2D(Collider2D col){

		if (col.gameObject.tag == "hitBox")
		{
			Destroy (hitBox);

			_GameController.playSFX (_GameController.sfxEnemyDead, 0.5f);

			slimeAnimator.SetTrigger ("dead");
		}



	}

	IEnumerator slimeWalk()
	{
		int rand = Random.Range (0, 100);

		if (rand < 33) 
		{
			h = -1;

		} 

		else if (rand < 66)
		
		{
			h = 0;

		} 

		else if(rand < 100)
		{
			h = 1;
		}



		yield return new WaitForSeconds (timeToWalk);
		StartCoroutine ("slimeWalk");
	}
	// destroi o inimigo

	void OnDead()
	{
		Destroy (this.gameObject);
	}

	void Flip()
	{


		isLookLeft = !isLookLeft;

		float x = transform.localScale.x * -1; // inverte sinal do scale x 

		transform.localScale = new Vector3 (x, transform.localScale.y, transform.localScale.z);


	}
}
