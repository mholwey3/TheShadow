using UnityEngine;
using System.Collections;

public class RandomizePages : MonoBehaviour
{
	public GameObject[] pages;
	public GameObject[] locations;
	int rand;

    //When game first runs, randomize the page locations
	void Awake ()
	{
		for(int i = 0; i < pages.Length; i++)
		{
			rand = Random.Range (0, locations.Length);
			Instantiate(pages[i], locations[rand].transform.position, locations[rand].transform.rotation);
			ShrinkArray ();
		}
	}

    //Restructures array with remaining locations
	void ShrinkArray()
	{
		GameObject[] newLocations = new GameObject[locations.Length - 1];
		for(int n = 0; n < newLocations.Length; n++)
		{
			if(n >= rand)
				newLocations[n] = locations[n+1];
			else
				newLocations[n] = locations[n];
		}
		
		locations = newLocations;
	}
}
