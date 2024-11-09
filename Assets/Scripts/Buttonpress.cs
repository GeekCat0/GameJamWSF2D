using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Button : MonoBehaviour
{
    public GameObject Journal;

    public void OpenJournal()
    {
        if (Journal != null)
        {
            bool isActive = Journal.activeSelf;

            Journal.SetActive(!isActive);
        }
    }
}
