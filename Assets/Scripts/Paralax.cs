using UnityEngine;
using System.Collections;

public class Paralax : MonoBehaviour
{
	public Transform[] backgrounds;
	public float scale;
	public float reduction;
	public float smothness;
	[SerializeField]
	private Transform cam;
	private Vector3 previousCamPos;

	float ypos = 0f;
	private void Awake()
	{
		ypos = backgrounds [0].transform.position.x;
	}

	void Start ()
	{
		
		cam = GameObject.FindGameObjectWithTag ("Camm").transform;
		if(cam)
			previousCamPos = cam.position;
	}
		
	void Update ()
	{
		if (!cam) {
			cam = GameObject.FindGameObjectWithTag ("Camm").transform;
			return;
		}
		float p = (previousCamPos.x - cam.position.x) * scale;
		for(int i = 0; i < backgrounds.Length; i++)
		{
			float backgroundTargetPosX = backgrounds[i].position.x + p * (i * reduction + 1);
			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smothness * Time.deltaTime);
		}
		previousCamPos = cam.position;
	}
}
