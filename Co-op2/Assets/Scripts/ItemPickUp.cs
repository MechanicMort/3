using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private Transform starty;
    private float startyy;
    private string PrefabName;
    // Start is called before the first frame update
    void Start()
    {
        starty = this.transform;
        startyy = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        FloatObject();
    }

    private void FloatObject()
    {
        transform.position =  new Vector3(starty.position.x, startyy += (Mathf.Sin(Time.time) * Time.deltaTime * 0.5f), starty.position.z);
        transform.Rotate(new Vector3(1,1,1));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            bool pickedup = other.GetComponent<InventoryBasic>().Item(gameObject.name);
            if (pickedup == true)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
