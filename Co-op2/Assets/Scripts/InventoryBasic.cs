using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBasic : MonoBehaviour
{
    public GameObject[] inv = new GameObject[10];
    private GameObject goSelected;
    public string fire;

    private void Update()
    {
        Throw();
    }

    private void Throw()
    {
        float temp = 20;
        if (Input.GetKeyDown(fire))
        {

            for (int i = 19; i > inv.Length; i--)
            {
                if (inv[i- 11] != null)
                {
                    temp = i- 11;
                    break;
                }
            }
            if (temp != 20)
            {
                GameObject grenade;
                grenade = Instantiate(inv[(int)temp], transform.position, Quaternion.identity);
                grenade.GetComponent<ParbolaMove>().move = true;
                grenade.SetActive(true);
                inv[(int)temp] = null;
            }
        }
    }

    public bool Item(string itemname)
    {
        bool ispicked = false;
        goSelected = GameObject.Find(itemname);
        for (int i = 0; i < inv.Length; i++)
        {
            if (inv[i] == null)
            {
                inv[i] = Instantiate(goSelected);
                inv[i].SetActive(false);
                Destroy(goSelected);
                ispicked = true;
                break;
            }
            else{
                ispicked = false;
            }

        }
        if (ispicked == false)
        {
            print("InvFull");
            return false;
        }
        else
        {
            return true;
        }
    }
}
