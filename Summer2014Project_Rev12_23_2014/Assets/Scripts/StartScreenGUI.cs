using UnityEngine;
using System.Collections;

public class StartScreenGUI : MonoBehaviour 
{
	void Start()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void StartGame()
	{
		Application.LoadLevel (1);
	}

	public void ToControls()
	{
		Application.LoadLevel (3);
	}

	public void ToCredits()
	{
		Application.LoadLevel (2);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}
}
