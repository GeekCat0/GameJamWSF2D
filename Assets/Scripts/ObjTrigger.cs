using UnityEngine;

public class ObjTrigger : MonoBehaviour
{
    [SerializeField] private string correctWeapon;
    [SerializeField] private GameObject notification;
    private bool questAdded = false;

    public void CreateQuest()
    {
        if (correctWeapon != null && !questAdded)
        {
            questAdded = !questAdded;
            ObjManager.objManager.questNames.Add(correctWeapon);
        }

        if (notification != null && !questAdded)
        {
            notification.SetActive(true);
        }
    }
}
