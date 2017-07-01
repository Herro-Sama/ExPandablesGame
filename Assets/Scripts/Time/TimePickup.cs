using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePickup : MonoBehaviour
{
    [SerializeField]
    private float time = 10f;

    /*
    [SerializeField]
    private GameObject pickupScreen;
    */

    [SerializeField]
    private Text text;
    
    private void Awake()
    {
        text.text = time.ToString("0");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        GameManager.instance.PlaySound(1);
        Inventory.instance.AddTimer(time);
        /*
        GameObject newPickupScreen = Instantiate(pickupScreen) as GameObject;
        newPickupScreen.GetComponent<TimePickupScreen>().Activate(time);
        */
        Destroy(gameObject);
    }
}