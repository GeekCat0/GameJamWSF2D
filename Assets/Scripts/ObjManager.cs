using UnityEngine;

public class ObjManager : MonoBehaviour
{
    public System.Collections.Generic.List<string> questNames = new();
    public static ObjManager objManager;
     
     private void Awake()
     {
        if (objManager != null)
        {
            Destroy(gameObject);
        }

        objManager = this;
        DontDestroyOnLoad(gameObject);
     }
}