using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGameObject : MonoBehaviour
{
    public int id;

    private void OnDisable()
    {
        LightReserveManager.Instance.LightDisabled(id);
    }
}
