using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData PD;

    private void OnEnable()
    {
        if (PersistentData.PD == null)
        {
            PersistentData.PD = this;
        }
        else
        {
            if (PersistentData.PD != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    
}
