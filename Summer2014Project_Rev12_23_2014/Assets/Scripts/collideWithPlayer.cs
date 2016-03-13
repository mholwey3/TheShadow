using UnityEngine;
using System.Collections;

public class collideWithPlayer : MonoBehaviour 
{

	public Transform player;
	public MouseLook mouseLook1;
	public MouseLook mouseLook2;
	public CharacterMovement characterMovement;
	public Footsteps footsteps;

	void Start()
	{
		mouseLook1 = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>();
		mouseLook2 = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
		characterMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
		footsteps = GameObject.FindGameObjectWithTag("playerFeet").GetComponent<Footsteps>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			StartCoroutine(EndGame (other));
		}
	}

	public IEnumerator EndGame(Collider player)
	{
		mouseLook1.enabled = false;
		mouseLook2.enabled = false;
		characterMovement.enabled = false;
		footsteps.enabled = false;
		gameObject.GetComponent<SlendermanMovements>().enabled = false;

		yield return new WaitForSeconds(1.5f);
		player.transform.LookAt(new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z));
		yield return new WaitForSeconds(5.0f);
		Application.LoadLevel (0);
	}
}
