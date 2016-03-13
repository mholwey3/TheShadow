using UnityEngine;
using System.Collections;

public class LayerMusic : MonoBehaviour 
{
	public AudioSource audio1;
	public AudioSource audio2;
	public AudioSource audio3;
	public AudioSource audio4;
	public AudioSource audio5;
	
	private float volume3 = 0.03f;
	private float volume4 = 0.04f;
	private float volume5 = 0.05f;
	
	public IEnumerator LayerAudio(byte pageNumber) 
	{
		if(pageNumber == 1)
		{
			audio1.mute = false;
			audio2.mute = false;
		}
		else if(pageNumber == 3)
		{
			while(audio3.volume < volume3)
			{
				yield return new WaitForSeconds(0.1f);
				audio3.volume += 0.001f;
			}
		}
		else if(pageNumber == 5)
		{
			while(audio4.volume < volume4)
			{
				yield return new WaitForSeconds(0.1f);
				audio4.volume += 0.005f;
			}
		}
		else if(pageNumber == 7)
		{
			while(audio5.volume < volume5)
			{
				yield return new WaitForSeconds(0.1f);
				audio5.volume += 0.005f;
			}
		}
		else if(pageNumber == 8)
		{
			while(audio1.volume > 0f || audio2.volume > 0f || audio3.volume > 0f || audio4.volume > 0f || audio5.volume > 0f)
			{
				yield return new WaitForSeconds(0.1f);
				audio1.volume -= 0.005f;
				audio2.volume -= 0.005f;
				audio3.volume -= 0.005f;
				audio4.volume -= 0.005f;
				audio5.volume -= 0.005f;
			}

			yield return new WaitForSeconds(5f);
			Application.LoadLevel (0);
		}
	}
}
