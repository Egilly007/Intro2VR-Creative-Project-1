using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupActions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var mover = collision.gameObject.GetComponent<Move>();
            if (mover != null) {
                mover.AddPickup();
            }
            else
            {
                Debug.Log("Somethings Wrong :(");
            }
                Destroy(gameObject);
        }
    }
}
