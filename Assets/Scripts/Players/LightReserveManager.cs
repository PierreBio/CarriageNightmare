using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReserveManager : MonoBehaviour
{
    static LightReserveManager _instance;
    public static LightReserveManager Instance { get { return _instance; } }


    [SerializeField] float _duration = 10; // Max duration
    [SerializeField] float _remainingDuration; // How much duration is remaining
    [SerializeField] float _rechargeDuration = 3; // How long does recharge take
    
    public float rechargeDuration { get { return _rechargeDuration; }}

    public delegate void LightReserveEmptyAction();
    public static event LightReserveEmptyAction OnEmptyLightReserve;

    public delegate void LightReserveRechargeAction();
    public static event LightReserveRechargeAction OnRechargeLightReserve;

    public delegate void LightDisabledAction(int i);
    public static event LightDisabledAction OnLightDisabled;

    bool isRecharging = false;


    public void LightDisabled(int id)
    {
        if (OnLightDisabled != null)
            OnLightDisabled(id);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        //DontDestroyOnLoad(gameObject);
    } 


    private void Start()
    {
        _remainingDuration = _duration;
        isRecharging = false;
        GameManager.OnGameRestart += Restart;
    }

    void Restart()
    {
        System.Delegate[] clientList = OnRechargeLightReserve.GetInvocationList();
        foreach (var d in clientList)
            OnRechargeLightReserve -= (d as LightReserveRechargeAction);

        clientList = OnEmptyLightReserve.GetInvocationList();
        foreach (var d in clientList)
            OnEmptyLightReserve -= (d as LightReserveEmptyAction);
        _remainingDuration = _duration;
        isRecharging = false;
    }

    public int fillRate // For the UI
    {
        get { return (int)((_remainingDuration * 100) / _duration); }
    }

    public bool canLight { get { return (_remainingDuration > 0) && (!isRecharging); } }
    
    public void LightOn(float timeDelta)
    {
        _remainingDuration -= timeDelta;
        if (_remainingDuration <= 0)
            OnEmptyLightReserve();
    }

    public bool CanRecharge()
    {
        return (!isRecharging && (_remainingDuration < _duration));
    }
    public void Recharge()
    {
        OnRechargeLightReserve();
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        isRecharging = true;
        float toRecharge = _duration - _remainingDuration;
        while (true)
        {
            _remainingDuration += Time.deltaTime * toRecharge / rechargeDuration;
            if (_remainingDuration > _duration)
            {
                _remainingDuration = _duration;
                isRecharging = false;
                yield break;
            }
            yield return null;
        }
    }
}
