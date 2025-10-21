using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PickupDropOff : MonoBehaviour
{
    public bool gameDone = false;
    
    private void Update()
    {
        endGame();
    }

    public int totalDropoffs = 0;


    public TMP_Text dropoffText;


    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        var mover = collision.gameObject.GetComponent<Move>();

        int playerPickups = mover.pickupCount;

        if (playerPickups <= 0)
        {
            Debug.Log("Player has no pickups to drop off.");
            return;
        }


        totalDropoffs += playerPickups;


        mover.AddPickup(-playerPickups);

        UpdateDropoffUI();

        Debug.Log($"Dropped off {playerPickups} pickups. Total dropoffs: {totalDropoffs}");
    }

    private void UpdateDropoffUI()
    {
        if (dropoffText != null)
        {
            dropoffText.text = "Dropped Off: " + totalDropoffs;
        }
    }

    void endGame()
    {
        if (totalDropoffs >= 10)
        {
            gameDone = true;
            Debug.Log("Good job! You have dropped off 10 pickups.");
            Invoke("resetGame", 3f);
        }
    }

    void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
