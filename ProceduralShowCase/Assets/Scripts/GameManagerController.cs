using UnityEngine;
using UnityEngine.UIElements;

public class GameManagerController : MonoBehaviour
{
    // Menu buttons
    public GameObject expandObj, collapseObj, menuObj;
    public Camera cam;
    

    // Update is called once per frame
    void Update()
    {
        // Quit application with escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Expand/collapse menu with key M
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (expandObj.activeSelf) // Collapse menu
            {
                expandObj.SetActive(false);
                collapseObj.SetActive(true);
                menuObj.SetActive(true);
            }
            else if (collapseObj.activeSelf) // Expand menu
            {
                expandObj.SetActive(true);
                collapseObj.SetActive(false);
                menuObj.SetActive(false);
            }
        }

        // Activate/deactivate camera rotator
        if (menuObj.activeSelf)
        {
            cam.GetComponent<CameraBehaviour>().enabled = false;
        } else
        {
            cam.GetComponent<CameraBehaviour>().enabled = true;
        }

    }
}
