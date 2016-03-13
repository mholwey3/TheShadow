using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour 
{
	public float velocity;
	public bool canSprint;
	private Vector3 moveDirection = Vector3.zero;
	public float stamina;
	private const float MAX_STAMINA = 1000f;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Input.GetKeyDown(KeyCode.Escape);
		stamina = MAX_STAMINA;

		StartCoroutine("GainStamina");
		StartCoroutine("LoseStamina");
	}

	void Update ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		CharacterController controller = GetComponent<CharacterController>();
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);

		if(Input.GetButton("Sprint") && canSprint)
		{
			moveDirection *= velocity * 2;
		}
		else
		{
			moveDirection *= velocity;
		}

		controller.Move (moveDirection * Time.deltaTime);
	}

	IEnumerator LoseStamina()
	{
		while(true)
		{
			if(stamina <= 0)
			{
				canSprint = false;
				stamina = 0;
				yield return new WaitForSeconds(3.0f);
				canSprint = true;
			}

			if(Input.GetButton("Sprint") && canSprint)
			{				
				if(stamina > 0)
					stamina -= 2.0f;
			}
			yield return null;
		}
	}

	IEnumerator GainStamina()
	{
		while(true)
		{
			if(stamina < MAX_STAMINA)
				stamina += 1.0f;
			else
				stamina = MAX_STAMINA;

			yield return null;
		}
	}
}
