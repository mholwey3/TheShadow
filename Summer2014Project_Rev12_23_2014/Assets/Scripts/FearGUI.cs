using UnityEngine;
using System.Collections;

public class FearGUI : MonoBehaviour 
{
	void Update () 
	{
		transform.position = new Vector2(0f, 0f);
		GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}
}
