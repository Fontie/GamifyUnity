using UnityEngine;
using System.Runtime.InteropServices;

public class deleteDoorOnAccessLevel : MonoBehaviour
{
    public int accessLevelNeeded = 0;
    public GameObject playerObject;

    private void Start()
    {

    }



    private void Update()
    {
        PlayerMovement playerScript = playerObject.GetComponent<PlayerMovement>();

        if (playerScript.accessLevel >= accessLevelNeeded)
        {
            gameObject.SetActive(false);
        }
    }



}
