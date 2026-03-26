using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    string levelName = "LunarLandscape3D";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
}
