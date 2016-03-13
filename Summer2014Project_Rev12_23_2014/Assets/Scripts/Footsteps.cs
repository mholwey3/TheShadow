//Mimicks subtle bobble movements when walking/sprinting

using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour
{
    #region variables & references

    public AudioClip[] footsteps;
	public CharacterController controller;
	public Transform player;
    public float nextFoot = 0.38f;

	private bool moveUp = true;
	private float lowPoint = 0.9f;
	private float highPoint = 1f;
	private int sprint = 2;
	private CharacterMovement characterMovement;

    #endregion

    void Start()
	{
		characterMovement = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterMovement>();
	}

	void Update() 
	{
		if(controller.velocity.magnitude > 0.7f)
		{
			if(!moveUp) //coming down and landing foot on ground
			{
				if(Input.GetButton("Sprint") && characterMovement.canSprint) //sprinting
					player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - (nextFoot*sprint*Time.deltaTime), player.transform.position.z);
				else  //walking
					player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - (nextFoot*Time.deltaTime), player.transform.position.z);

				if(player.transform.position.y <= lowPoint)
				{
					moveUp = true;
					GetComponent<AudioSource>().PlayOneShot (footsteps[Random.Range (0, footsteps.Length)]);
				}
			}
			else //bouncing back up/picking up next foot
			{
				if(Input.GetButton("Sprint") && characterMovement.canSprint) //sprinting
					player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + (nextFoot*sprint*Time.deltaTime), player.transform.position.z);
				else  //walking
					player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + (nextFoot*Time.deltaTime), player.transform.position.z);
				
				if(player.transform.position.y >= highPoint)
					moveUp = false;
			}
		}
	}
}
