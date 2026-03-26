using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPileActions : MonoBehaviour
{
    public GameObject panel;
    [SerializeField] private GameObject pickupPrefab;

    public Vector3 offset = new Vector3(-10, -6, -10);
    public bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.Q))
        {
            if (pickupPrefab != null)
            {
                Instantiate(pickupPrefab, transform.position + offset, Quaternion.identity);
            }

            panel.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        panel.SetActive(true);
        inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        panel.SetActive(false);
        inRange = false;
    }
}
