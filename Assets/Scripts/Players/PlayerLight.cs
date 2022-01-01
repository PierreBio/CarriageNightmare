using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    [SerializeField] GameObject lightGameObject;
    public bool isLightOn = false;
    [SerializeField] PlayerController player;


    private void Start()
    {
        lightGameObject.SetActive(isLightOn);
        LightReserveManager.OnEmptyLightReserve += ReserveEmpty;
        LightReserveManager.OnRechargeLightReserve += TurnOffLight;
    }

    void ReserveEmpty()
    {
        SetLight(false);
        Recharge();
    }

    private void Update()
    {
        if (isLightOn)
        {
            LightReserveManager.Instance.LightOn(Time.deltaTime);
        }
    }

    void SetLight(bool isOn)
    {
        isLightOn = isOn;
        lightGameObject.SetActive(isLightOn);
    }

    void TurnOffLight()
    {
        SetLight(false);
    }

    public void UpdateLightPosition(Vector3 position, bool hasHit)
    {
        lightGameObject.transform.position = position;
        lightGameObject.SetActive(hasHit && isLightOn);
    }

    public void Recharge()
    {

        if (LightReserveManager.Instance.CanRecharge())
        {
            StartCoroutine(LightRechargeCoroutine());
        }
    }

    public void LightSwitch()
    {
        if (lightGameObject.activeSelf)
        {
            SetLight(false);
            Recharge();
        }
        else
        {
            if (LightReserveManager.Instance.canLight)
                SetLight(true);
        }
        
    }

    IEnumerator LightRechargeCoroutine()
    {
        LightReserveManager.Instance.Recharge();
        player.CanPlay(false);
        yield return new WaitForSeconds(LightReserveManager.Instance.rechargeDuration);
        player.CanPlay(true);
    }
}
