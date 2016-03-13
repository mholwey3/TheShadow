using UnityEngine;
using System.Collections;

public class playerFear : MonoBehaviour 
{
	public GUITexture fearTexture;
	public Transform slenderman;
	public Transform player;
	public CharacterMovement characterMovement;
	public Footsteps footsteps;
	public collideWithPlayer _collideWithPlayer;
	public AudioSource distortion;
	public AudioSource distortion2;

	private bool startled = false;
	private float distance; //distance between slenderman and player
	private const int FEAR_CAPACITY = 100; //total fear the player can handle before losing the game
	private float currentFearAmt = 0; //current amount of fear instilled in the player

	void Start () 
	{
		characterMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
		footsteps = GameObject.FindGameObjectWithTag("playerFeet").GetComponent<Footsteps>();
		_collideWithPlayer = GameObject.FindGameObjectWithTag("Slenderman").GetComponent<collideWithPlayer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		distance = Vector3.Distance(transform.position, slenderman.position);

		distortion.volume = currentFearAmt * 0.01f * 0.15f;
		//Fear is instilled in the player based on if slenderman is visible and how far away he stands from the player
		if(distance <= 75 && GetComponent<Camera>().WorldToScreenPoint(slenderman.position).z > 0 && !Physics.Linecast(transform.position, slenderman.position))
		{
			currentFearAmt += Time.deltaTime * 250f / distance;

			if(distance <= 15 && !startled)
			{
				StartCoroutine ("Startle");
				startled = true;
			}
		}
		else
		{
			if(currentFearAmt > 0)
			{
				currentFearAmt -= Time.deltaTime * 10f;

				if(currentFearAmt <= 0)
					currentFearAmt = 0f;
			}
		}

		fearTexture.color = new Color(fearTexture.color.r, fearTexture.color.g, fearTexture.color.b, 
		                                (currentFearAmt * 0.01f));

		if(currentFearAmt >= FEAR_CAPACITY)
		{
			Debug.Log ("End Game");
			StartCoroutine(_collideWithPlayer.EndGame(player.GetComponent<Collider>()));
		}
	}

	IEnumerator Startle()
	{
		SpeedBoost ();
		distortion2.Play ();
		distortion2.pitch = 1.0f;
		distortion2.volume = 0.1f;

		yield return new WaitForSeconds(15f);

		while(distortion2.volume > 0f)
		{
			yield return new WaitForSeconds(0.1f);
			distortion2.volume -= 0.25f;
		}
		NormalizeSpeed();

		startled = false;
	}

	void SpeedBoost()
	{
		characterMovement.velocity *= 1.5f;
		footsteps.nextFoot *= 1.2f;
	}

	void NormalizeSpeed()
	{
		characterMovement.velocity /= 1.5f;
		footsteps.nextFoot /= 1.2f;
	}
}
