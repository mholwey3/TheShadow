using UnityEngine;
using System.Collections;

public class SlendermanMovements : MonoBehaviour 
{
	public Transform player; //information about player transform
	public Camera camera; //camera attached to the player
	public float respawnTime; //time after which slenderman will respawn in a new location if not seen

	private float spawnDistance; //distance slenderman spawns from the player
	private float moveSpeed = 0f; //how quickly does slenderman approach the player?
	private float distance; //distance between player and slenderman
	private float timeNotSeen = 0; //keeps track of the amount of time slenderman is not seen by the player
	private NavMeshAgent agent; //allows enemy to traverse the terrain using the baked nav mesh

	void Start()
	{
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.speed = moveSpeed;
	}

	void Update()
	{
		distance = Vector3.Distance (transform.position, player.position);

		//constantly set the destination of the enemy to the player position
		agent.SetDestination (player.position);
		transform.LookAt(player.position);

		//set the speed of the enemy
		//if the player is not looking at slenderman, slenderman moves towards the player based on moveSpeed
		//else he stands still
		if(camera.WorldToScreenPoint (transform.position).z < 0 || distance > 75)
			agent.speed = moveSpeed;
		else
			agent.speed = 0;

		//slenderman will teleport to a new spot (closer to the player) if he has been far away from the player for some time
		if(distance > 75 && moveSpeed > 0)
		{
			timeNotSeen += Time.deltaTime;
			
			if(timeNotSeen > respawnTime)
			{
				RandomizeLocation (spawnDistance - 10);
				timeNotSeen = 0;
			}
		}
	}

	//increases the difficulty of evading slenderman depending on the number of pages acquired
	public void IncreaseDifficulty(byte pageNumber) 
	{
		if(pageNumber == 1)
		{
			spawnDistance = 200f;
			moveSpeed = 2.2f;
			RandomizeLocation(spawnDistance);
		}
		else if(pageNumber == 2)
		{
			spawnDistance = 175f;
			moveSpeed = 2.7f;
			RandomizeLocation (spawnDistance);
		}
		else if(pageNumber == 3)
		{
			spawnDistance = 150f;
			moveSpeed = 3.7f;
			RandomizeLocation (spawnDistance);
		}
		else if(pageNumber == 4)
		{
			spawnDistance = 125f;
			moveSpeed = 4.2f;
			RandomizeLocation (spawnDistance);
		}
		else if(pageNumber == 5)
		{
			spawnDistance = 100f;
			moveSpeed = 5.0f;
			RandomizeLocation (spawnDistance);
		}
		else if(pageNumber == 6)
		{
			spawnDistance = 60f;
			moveSpeed = 5.5f;
			RandomizeLocation (spawnDistance);
		}
		else if(pageNumber == 7)
		{
			spawnDistance = 30f;
			moveSpeed = 6.0f;
			RandomizeLocation (spawnDistance);
		}
		else if(pageNumber == 8)
		{
			transform.position = new Vector3(400f, 0.73f, 900f);
			moveSpeed = 0f;
		}
	}

	public void RandomizeLocation(float spawnDistance)
	{
		agent.enabled = false;
		transform.position = new Vector3(player.position.x + (Random.insideUnitCircle.x * spawnDistance), 0.73f, player.position.z + (Random.insideUnitCircle.y * spawnDistance));
		agent.enabled = true;
	}
}
