using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public Transform playerTransform;

	public float speedCam;

	private Camera cam;

	public Transform limiteCamEsq, limiteCamDir, limiteCamSup, limiteCamBaixo;

	// Use this for initialization
	void Start () {

		cam = Camera.main;

	}
	
	// Update is called once per frame
	void Update () {


	}

	void LateUpdate()
	{
		//Vector3 posCam = new Vector3 (playerTransform.position.x, playerTransform.position.y, cam.transform.position.z);
		//cam.transform.position = posCam;
		CamController();

	}



	void CamController()
	{

		float posCamX = playerTransform.position.x;
		float posCamY = playerTransform.position.y;

		if (cam.transform.position.x < limiteCamEsq.position.x && playerTransform.position.x < limiteCamEsq.position.x)
		{
			posCamX = limiteCamEsq.position.x;
		}


		else if (cam.transform.position.x > limiteCamDir.position.x && playerTransform.position.x > limiteCamDir.position.x)
		{
			posCamX = limiteCamDir.position.x;
		}



		if (cam.transform.position.y < limiteCamBaixo.position.y && playerTransform.position.y < limiteCamBaixo.position.y)
		{
			posCamY = limiteCamBaixo.position.y;
		}


		else if (cam.transform.position.y > limiteCamSup.position.y && playerTransform.position.y > limiteCamSup.position.y)
		{
			posCamY = limiteCamSup.position.y;
		}



		Vector3 posCam = new Vector3 (posCamX, posCamY, cam.transform.position.z);

		cam.transform.position = Vector3.Lerp (cam.transform.position, posCam, speedCam * Time.deltaTime);

		//parou aula 8

	}


}





































