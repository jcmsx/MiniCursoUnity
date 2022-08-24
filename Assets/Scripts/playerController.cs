using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	private GameController _GameController;

	private Rigidbody2D playerRb;
	private Animator playerAnimator;


	public float speed;
	public float jumpForce;

	public bool isLookLeft;

	public Transform groundCheck;
	private bool isGrounded;
	private bool isAtack;

	public Transform mao;
	public GameObject hitBoxPrefab;

	// Use this for initialization
	void Start () {
	
		playerRb = GetComponent<Rigidbody2D> ();

		playerAnimator = GetComponent<Animator> ();

		_GameController = FindObjectOfType (typeof(GameController)) as GameController;

		_GameController.playerTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {

		float h = Input.GetAxisRaw ("Horizontal");

		if (isAtack == true && isGrounded == true) 
		{
			h = 0;
		}



		if (h > 0 && isLookLeft == true) {
			Flip ();

		} else if (h < 0 && isLookLeft == false) {
			Flip ();
		}


		float speedY = playerRb.velocity.y;

		if (Input.GetButtonDown ("Jump") && isGrounded == true) {

			_GameController.playSFX (_GameController.sfxJump, 0.5f);
			playerRb.AddForce( new Vector2(0,jumpForce));

		}

		if (Input.GetButtonDown ("Fire1") && isAtack == false) {
		
			isAtack = true;
			_GameController.playSFX (_GameController.sfxAtack, 0.5f);
			playerAnimator.SetTrigger ("atack");
		}
	

		playerRb.velocity = new Vector2 (h * speed, speedY);
	
		playerAnimator.SetInteger("h", (int) h);  // casting conversao int
		playerAnimator.SetBool("isGrounded", isGrounded);
		playerAnimator.SetFloat ("speedY", speedY);
		playerAnimator.SetBool ("isAtack", isAtack);
	
	}

	void FixedUpdate(){

		isGrounded = Physics2D.OverlapCircle (groundCheck.position, 0.02f);

	}

	void OnTriggerEnter2D(Collider2D col){
		
		if (col.gameObject.tag == "Coletavel") {

			_GameController.playSFX (_GameController.sfxCoin, 2.5f);
			Destroy (col.gameObject);

		} else if (col.gameObject.tag == "damage") {

			print ("Dano");
		}

	}



	//funções operador

	void Flip()
	{


		isLookLeft = !isLookLeft;

		float x = transform.localScale.x * -1; // inverte sinal do scale x 

		transform.localScale = new Vector3 (x, transform.localScale.y, transform.localScale.z);


	}

	void OnEndAtack(){
	
		isAtack = false;
	}

	void hitBoxAtack()
	{
		GameObject hitBoxTemp = Instantiate (hitBoxPrefab, mao.position, transform.localRotation);
		Destroy (hitBoxTemp, 0.2f);

	}


	void footStep()
		{
		_GameController.playSFX(_GameController.sfxStep[Random.Range(0, _GameController.sfxStep.Length)], 1f);
	}


}




































