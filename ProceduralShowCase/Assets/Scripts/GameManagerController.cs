using UnityEngine;
using UnityEngine.UIElements;

public class GameManagerController : MonoBehaviour
{
    //[SerializeField]
    public GameObject expandObj, collapseObj, menuObj;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


        if (Input.GetKeyDown(KeyCode.M))
        {
            if (expandObj.activeSelf)
            {
                expandObj.SetActive(false);
                collapseObj.SetActive(true);
                menuObj.SetActive(true);
            }
            else if (collapseObj.activeSelf)
            {
                expandObj.SetActive(true);
                collapseObj.SetActive(false);
                menuObj.SetActive(false);
            }
        }

    }
}
