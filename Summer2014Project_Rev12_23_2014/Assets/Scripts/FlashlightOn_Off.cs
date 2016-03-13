using UnityEngine;
using System.Collections;

public class FlashlightOn_Off : MonoBehaviour 
{
	public AudioSource flashlightClick;
	private Light flashlight;

	void Start () 
	{
		flashlight = GetComponent<Light>();
	}

	void Update () 
	{
		if(Input.GetKeyUp (KeyCode.F))
		{
			flashlightClick.Play();
			flashlight.enabled = !flashlight.enabled;
		}
	}
}
