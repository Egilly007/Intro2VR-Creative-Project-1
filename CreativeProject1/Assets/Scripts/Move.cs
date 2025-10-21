using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Move : MonoBehaviour
{
    public float speed = 6.0f;
    public float rotationSpeed = 100.0f;

    Rigidbody rb;
    public GameObject bullet;

    public int pickupCount = 0;
    public TMP_Text pickupText;
    public TMP_Text totalDropped;

    public PickupDropOff dropOffref;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1, 0);

        if (dropOffref == null)
        {
            dropOffref = FindObjectOfType<PickupDropOff>();
        }

        UpdatePickupUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10.0f;
        }
        else
        {
            speed = 6.0f;
        }

        movement();
        resetCharacter();
        shoot();
    }

    void movement()
    {
        if (transform.up.y < 0.5f)
        {
            return;
        }

        float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float rotate = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        Vector3 movement = transform.forward * move;
        rb.MovePosition(rb.position + movement);

        Quaternion turn = Quaternion.Euler(0f, rotate, 0f);
        rb.MoveRotation(rb.rotation * turn);

    }

    void resetCharacter()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(0, 5, 0);
            transform.rotation = Quaternion.identity;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Quaternion bulletRotation = transform.rotation * Quaternion.Euler(90, 0, 0);
            Instantiate(bullet, transform.position + transform.forward * 2 + transform.up * 2, bulletRotation);
        }
    }

    public void AddPickup(int amount = 1)
    {
        pickupCount += amount;
        UpdatePickupUI();
        Debug.Log("Pickups: " + pickupCount);
    }

    private void UpdatePickupUI()
    {
        if (pickupText != null)
        {
            pickupText.text = "Pickups: " + pickupCount;
        }
        
        int total = (dropOffref != null) ? dropOffref.totalDropoffs : 0;

        if (totalDropped != null)
        {
            totalDropped.text = "Total: " + total;
        }
    }
}
