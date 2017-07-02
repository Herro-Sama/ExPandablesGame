using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCamera : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;

	public float minX = 0;
	public float maxX = 0;

    private void Update()
    {
        float horMov = Input.GetAxis("Horizontal");
        //float verMov = Input.GetAxis("Vertical");
        horMov *= Time.deltaTime * speed;
        //verMov *= Time.deltaTime * speed;
        transform.Translate(horMov, /*verMov*/0, 0);
		transform.position = new Vector3 (Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);

    }
}
