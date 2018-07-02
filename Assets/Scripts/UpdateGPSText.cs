using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGPSText : MonoBehaviour {
	public Text coordinates;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		coordinates.text = "Lat: "+GPS.Instance.latitude.ToString()+" Lon:"+GPS.Instance.longitude.ToString();		
	}
}
