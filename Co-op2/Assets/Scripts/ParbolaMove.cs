using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParbolaMove : MonoBehaviour
{

    public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;
    public float blastradius;
    private GameObject[] playersarray = new GameObject[2];
    public bool move = false;
    private bool movin = false;

    public Transform Projectile;
    private Transform myTransform;

    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
        playersarray = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine("Col");
    }

    void StartProj()
    {

    }


    private void Update()
    {
        if (move == true && movin == false)
        {
            StartCoroutine("SimulateProjectile");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            move = false;
            movin = false;
            for (int i = 0; i < playersarray.Length; i++)
            {
                if ( Vector3.Distance(playersarray[i].transform.position,this.transform.position) < blastradius)
                {
                    playersarray[i].GetComponent<PlayerMoveBasic>().Damage(10);
                }
            }
        }
        else if (other.tag == "Player" && move == false)
        {
            bool pickedup = other.GetComponent<InventoryBasic>().Item(gameObject.name);
            if (pickedup == true)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private IEnumerator Col()
    {
        yield return new WaitForSeconds(0.7f);
        this.GetComponent<BoxCollider>().enabled = true;
    }

    IEnumerator SimulateProjectile()
    {
        movin = true;

        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(0.5f);

        // Move projectile to the position of throwing object + add some offset if needed.
        Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(Projectile.position, Target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime*2, Vx * Time.deltaTime*2);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}
