using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            foreach (Transform go in gameObject.transform)
                go.gameObject.SetActive(!go.gameObject.activeSelf);

            GetComponent<Image>().enabled = !GetComponent<Image>().enabled;
        }
    }
}
