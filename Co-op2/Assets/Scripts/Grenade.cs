using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("Col");
    }

    private IEnumerator Col()
    {
        yield return new WaitForSeconds(0.7f);
        this.GetComponent<BoxCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            gameObject.GetComponent<FOV>().enabled = true;
            StartCoroutine("KILL");
        }
    }

  

    private IEnumerator KILL()
    {
        yield return new WaitForSeconds(0.4f);
        GameObject.Destroy(GameObject.Find(this.name));
    }
}
