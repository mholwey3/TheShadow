using UnityEngine;
using System.Collections;

public class PickUpPages : MonoBehaviour 
{
	public AudioSource pageAudio;
	public LayerMusic layerMusic;
	public SlendermanMovements slendermanMovements;

	private byte pageNumber = 0;
	private float distance = 4.0f;
	private GameObject[] pages;
	private Transform pagePos;
	private bool showLabel = true;
	private int fontSize = 20;

	void Start()
	{
		Invoke ("ToggleLabel", 5.0f);
		layerMusic = GameObject.FindGameObjectWithTag("Player").GetComponent<LayerMusic>();
		slendermanMovements = GameObject.FindGameObjectWithTag("Slenderman").GetComponent<SlendermanMovements>();
	}

	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast (ray, out hit, distance))
			{
				if(hit.collider.tag == "Page")
				{
					showLabel = true;
					Invoke ("ToggleLabel", 5.0f);
					pageNumber++;
					pageAudio.Play ();
					StartCoroutine (layerMusic.LayerAudio(pageNumber));
					slendermanMovements.IncreaseDifficulty(pageNumber);
					Destroy (hit.collider.gameObject);
				}
			}
		}
	}

	void ToggleLabel()
	{
		showLabel = !showLabel;
	}

	void OnGUI()
	{
		GUI.skin.label.fontSize = fontSize;
		if(showLabel)
		{
			if(pageNumber == 0)
				GUI.Label (new Rect(Screen.width * 0.425f, Screen.height * 0.5f, 250, 50), "Find and collect the 8 pages");
			else if(pageNumber > 0)
				GUI.Label (new Rect(Screen.width * 0.475f, Screen.height * 0.5f, 250, 50), pageNumber + "/8 pages found");
		}
	}
}