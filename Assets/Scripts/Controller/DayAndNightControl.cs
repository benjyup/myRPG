//2016 Spyblood Games

using UnityEngine;
using System.Collections;

public class DayAndNightControl : MonoBehaviour {
	Mesh mesh;
	public GameObject StarDome;
	public int currentDay = 0;
	public string DayState;
	public Light directionalLight;
	public float SecondsInAFullDay = 120f;
	[Range(0,1)]
	public float currentTime = 0; //(0 = night, 1 = day)
	[HideInInspector]
	public float timeMultiplier = 1f; //how fast the day goes by regardless of the secondsInAFullDay var. (Simulate Seasons)
	float lightIntensity;
	Material starMat;

	// Use this for initialization
	void Start () {
		lightIntensity = directionalLight.intensity;
		//starMat = StarDome.GetComponent<MeshRenderer> ().material;
	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateLight();
		CheckTimeOfDay ();
		currentTime += (Time.deltaTime / SecondsInAFullDay) * timeMultiplier;
		if (currentTime >= 1) {
			//once we hit "midnight"; any time after that sunrise will begin.
			currentTime = 0;
			currentDay++;
		}
	}

	void UpdateLight()
	{
		StarDome.transform.Rotate (new Vector3 (0, 0, 2f * Time.deltaTime));
		directionalLight.transform.localRotation = Quaternion.Euler ((currentTime * 360f) - 90, 170, 0);
		float intensityMultiplier = 1;
		if (currentTime <= 0.23f || currentTime >= 0.75f) {
			intensityMultiplier = 0;
//			starMat.color = new Color(1,1,1,Mathf.Lerp(1,0,Time.deltaTime));
		}
		else if (currentTime <= 0.25f) {
			intensityMultiplier = Mathf.Clamp01((currentTime - 0.23f) * (1 / 0.02f));
//			starMat.color = new Color(1,1,1,Mathf.Lerp(0,1,Time.deltaTime));
		}
		else if (currentTime <= 0.73f) 
			intensityMultiplier = Mathf.Clamp01(1 - ((currentTime - 0.73f) * (1 / 0.02f)));
		directionalLight.intensity = lightIntensity * intensityMultiplier;
	}

	void CheckTimeOfDay ()
	{
		if (currentTime < 0.25f || currentTime > 1f)
			DayState = "Midnight";
		if (currentTime > 0.25f)
			DayState = "Morning";
		if (currentTime > 0.25f && currentTime < 0.5f)
			DayState = "Mid Noon";
		if (currentTime > 0.5f && currentTime < 0.75f)
			DayState = "Evening";
		if (currentTime > 0.75f && currentTime < 1f)
			DayState = "Night";
	}

	//GUI to see the days and states
	void OnGUI()
	{
		GUI.Box (new Rect (15, 15, 100, 25), "Day: " + currentDay);
		GUI.Box (new Rect (40, 40, 200, 30), "" + DayState);
	}
}
