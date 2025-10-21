using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPileActions : MonoBehaviour
{
    [SerializeField] private GameObject pickupPrefab;

    public Vector3 offset = new Vector3(-10,-6,-10);

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
        if (collision.gameObject.CompareTag("Bullet") && !collision.gameObject.CompareTag("Player"))
        {

            Destroy(gameObject);

            for (int i = 0; i < Random.Range(1, 3); i++)
            {
                Instantiate(pickupPrefab, transform.position += offset, Quaternion.identity);
            }

        }
    }
}
