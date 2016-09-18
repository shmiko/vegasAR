using UnityEngine;
using System.Collections;
using Vuforia;

public class ScaleVideoPlane : MonoBehaviour, ITrackableEventHandler {

	public Transform cameraObj;
	public float minSize;
	public float maxSize;
	public GameObject imageTarget;

	private float startDist = 0;
	private float currentDist;
	private float scale; // take only x axis
	private float xyz; //for all axis (x, y, z)

	private TrackableBehaviour mTrackableBehaviour;

	// Use this for initialization
	void Start () {
		mTrackableBehaviour = imageTarget.GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
		scale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(startDist != 0)
		{
			currentDist =  Vector3.Distance(cameraObj.position, transform.position);
			float percent = (100 * currentDist)/startDist;
			xyz = (percent*scale)/100;
			if(xyz > minSize && xyz < maxSize)
			{
				transform.localScale = new Vector3(xyz, xyz, 0.5625f*xyz);
			}
		}
	}

	public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			//when target is found
			startDist =  Vector3.Distance(cameraObj.position, transform.position);
		}
	}   
}
