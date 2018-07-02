using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// position script will place an object on the plane at the latitude and longitude relative to the GPS instance
public class PositionScript : MonoBehaviour {
	public float latitude;
	public float longitude;
    public string location_name;
	public Text output = null;
	// Use this for initialization
	// based on the latitude and longitude, the location of the object is changed...
	void Start () {
		// this is done in the main camera, in the GPS script... get the instance information to set the LocalOrigin
		Debug.Log("Initial lat" + GPS.Instance.latitude +"lng"+GPS.Instance.longitude);
		
		GPSEncoder.SetLocalOrigin( new Vector2(GPS.Instance.latitude, GPS.Instance.longitude));
		transform.position = GPSEncoder.GPSToUCS(latitude,longitude);
		Debug.Log("start x"+transform.position.x+':'+ transform.position.y+':'+transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Initial lat" + GPS.Instance.latitude + "lng" + GPS.Instance.longitude);

        GPSEncoder.SetLocalOrigin( new Vector2(GPS.Instance.latitude, GPS.Instance.longitude));		
		transform.position = GPSEncoder.GPSToUCS(latitude,longitude);
        float distance = GPSEncoder.CurrentDistance(latitude, longitude);

        if (output!=null){
			output.text = this.location_name+ " Lat: "+latitude.ToString()+" Lon:"+longitude.ToString() + "dis:"+distance;
            Debug.Log("output something");
        }
        else
        {
            Debug.Log(this.location_name + " Lat: " + latitude.ToString() + " Lon:" + longitude.ToString() + "dis:" + distance);
            
        }
		//Debug.Log("update x"+transform.position.x + ':'+transform.position.y);
	
	}
}
