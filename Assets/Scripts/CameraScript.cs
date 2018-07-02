using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {

	private Gyroscope gyro;
	private GameObject cameraContainer;
	private Quaternion rotation;

	private bool camAvailable;
	private WebCamTexture backCam;
	private Texture defaultBackground;

	public RawImage background;
	public AspectRatioFitter fit;
	// Use this for initialization
	void Start () 
	{
		if(!SystemInfo.supportsGyroscope)
		{
			Debug.Log("This device does not have a gyroscope");
			return;
		}
		defaultBackground = background.texture;
		WebCamDevice [] devices = WebCamTexture.devices;

		if(devices.Length == 0)
		{
			Debug.Log("no cameras detected");
			camAvailable = false;
			return;
		}
		for(int i = 0;i<devices.Length; i++)
		{
			if(!devices[i].isFrontFacing)
			{
				backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
			}
		}
		if(backCam == null)
		{
			Debug.Log("Unable to find backcamera");
			return;
		}
		cameraContainer = new GameObject("Camera Container");
		cameraContainer.transform.position = transform.position;
		transform.SetParent(cameraContainer.transform);

		gyro = Input.gyro;
		gyro.enabled = true;
		cameraContainer.transform.rotation = Quaternion.Euler(90f,0,0);
		rotation = new Quaternion (0,0,1,0);

		backCam.Play();
		background.texture = backCam;
		camAvailable = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!camAvailable)
			return;
		transform.localRotation = gyro.attitude * rotation;
		float ratio = (float) backCam.width/(float) backCam.height;
		fit.aspectRatio = ratio;
		float scaleY = backCam.videoVerticallyMirrored ? -1f: 1f;
		background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

		int orient = -backCam.videoRotationAngle;
		background.rectTransform.localEulerAngles = new Vector3(0,0,orient);
	}
}
