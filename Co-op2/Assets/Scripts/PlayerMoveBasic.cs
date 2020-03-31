using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveBasic : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public string left;
    public string right;
    public string up;
    public string down;
    private float x;
    private float z;
    [SerializeField] private float hp = 100;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void Damage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }      
    

    private void Movement()
    {
        if (Input.GetKey(left))
        {
            x = -1;
        }
        else if (Input.GetKey(right))
        {
            x = 1;
        }
        else
        {
            x = 0;
        }
        if (Input.GetKey(up))
        {
            z = 1;
        }
        else if (Input.GetKey(down))
        {
            z = -1;
        }
        else
        {
            z = 0;
        }
        Vector3 movement = new Vector3(x, 0f, z) * moveSpeed * Time.deltaTime;
        Vector3 newPosition = rb.position + transform.TransformDirection(movement);
        rb.MovePosition(newPosition);
    }
}