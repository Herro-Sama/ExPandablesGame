using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject icon;

    [SerializeField]
    private GameObject prompt;

    public static Inventory instance;

    private Queue<float> pickups = new Queue<float>();

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(instance.gameObject);
        instance = this;
    }

    public void AddTimer(float seconds)
    {
        pickups.Enqueue(seconds);
        GameObject newpickupIcon = Instantiate(icon);
        newpickupIcon.transform.SetParent(transform, false);
        newpickupIcon.transform.SetAsFirstSibling();
        newpickupIcon.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        newpickupIcon.transform.GetChild(1).GetComponent<Text>().text = seconds.ToString("0");
        prompt.SetActive(true);
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            UseAbility();
        }
    }

    private void UseAbility()
    {
        if(pickups.Count == 0)
        {
            return;
        }

        GameManager.instance.TakeTimeToLevel(pickups.Dequeue());
        Destroy(transform.GetChild(transform.childCount-1).gameObject);
        GameManager.instance.PlaySound(7);

        if(pickups.Count == 0)
        {
            prompt.SetActive(false);
        }
    }

    public void TurnPowerupsIntoPool()
    {
        if(pickups.Count >= 1)
        {
            GameManager.instance.GiveTimeToPool(pickups.Dequeue());
            TurnPowerupsIntoPool();
        }
    }
}
