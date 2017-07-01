using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCamera : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;

    private void Update()
    {
        float horMov = Input.GetAxis("Horizontal");
        float verMov = Input.GetAxis("Vertical");
        horMov *= Time.deltaTime * speed;
        verMov *= Time.deltaTime * speed;
        transform.Translate(horMov, verMov, 0);


    }
}
