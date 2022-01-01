using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Camera mainCamera;

    [SerializeField] RectTransform reticleUI;
    [SerializeField] float reticleSpeed = 10;
    [SerializeField] RectTransform reticleCanvas;

    Vector2 onMoveInput;

    [SerializeField] PlayerLight playerLight;
    [SerializeField] float lightPositionOffset = 1;

    [SerializeField] int damage_lycan = 10;
    [SerializeField] int damage_vampire = 10;

    [SerializeField] float fireCoolDown = 0.5f;

    bool canPlay = true;
    bool canFire = true;

    [SerializeField] int playerId;

    private void Start()
    {
        mainCamera = GameManager.Instance.mainCamera;
        InitializeReticlePosition();
    }

    private void OnMove(InputValue inputValue)
    {
            onMoveInput = inputValue.Get<Vector2>();
    }

    void OnFire()
    {
        if (canPlay && canFire)
        {
            StartCoroutine(FireCoolDownCoroutine());
            SoundManager.GetInstance().Play("Shoot", this.gameObject);

            Vector2 canvasDim = reticleCanvas.rect.size;
            Vector3 screenReticlePosition = reticleUI.anchoredPosition / canvasDim + Vector2.one * 0.5f;
            screenReticlePosition = screenReticlePosition * mainCamera.pixelRect.size;
            screenReticlePosition.z = 1;
            Vector3 projectedReticlePosition = mainCamera.ScreenToWorldPoint(screenReticlePosition);
            Vector3 direction = (projectedReticlePosition - mainCamera.transform.position).normalized;

            //int groundLayerMask = 1 << 3;
            int enemyLayerMask = 1 << 6;

            RaycastHit hitInfo;
            float maxDist = 1000;

            if (Physics.Raycast(mainCamera.transform.position, direction,
                out hitInfo, maxDist, enemyLayerMask))
            {
                Debug.Log("Hit");
                Debug.Log(hitInfo.collider.gameObject.tag);
                if (hitInfo.collider.gameObject.CompareTag("Lycan"))
                {
                    hitInfo.collider.gameObject.GetComponent<EnemyBody>().TakeDamage(damage_lycan, playerId);
                }
                if (hitInfo.collider.gameObject.CompareTag("Vampire"))
                {
                    hitInfo.collider.gameObject.GetComponent<EnemyBody>().TakeDamage(damage_vampire, playerId);
                   
                }
                
                
            }
        }
    }

    void OnLight()
    {
        if (canPlay)
        {
            playerLight.LightSwitch();
        }
    }

    void OnRecharge()
    {
        if (canPlay)
        {
            playerLight.Recharge();
        }
    }


    void Update()
    {
        if (canPlay)
        {
            UpdateReticlePosition();
        }
        Vector3 lightPosition;
        if (playerLight.isLightOn)
        {
            bool hasHit = GetLightPosition(out lightPosition);
            playerLight.UpdateLightPosition(lightPosition, hasHit);
        }
    }

    IEnumerator FireCoolDownCoroutine()
    {
        canFire = false;
        yield return new WaitForSeconds(fireCoolDown);
        canFire = true;
    }

    public void CanPlay(bool canPlayInput)
    {
        canPlay = canPlayInput;
    }

    bool GetLightPosition(out Vector3 lightPosition)
    {
        Vector2 canvasDim = reticleCanvas.rect.size;
        Vector3 screenReticlePosition = reticleUI.anchoredPosition/canvasDim + Vector2.one * 0.5f;
        screenReticlePosition = screenReticlePosition * mainCamera.pixelRect.size;
        screenReticlePosition.z = 1;
        Vector3 projectedReticlePosition = mainCamera.ScreenToWorldPoint(screenReticlePosition);
        Vector3 direction = (projectedReticlePosition - mainCamera.transform.position).normalized;

        int groundLayerMask = 1 << 3;
        //int enemyLayerMask = 1 << 6;

        RaycastHit hitInfo;
        float maxDist = 1000;

        if (Physics.Raycast(mainCamera.transform.position, direction,
            out hitInfo, maxDist, groundLayerMask))
        {
            lightPosition = hitInfo.point + lightPositionOffset * Vector3.up;
            return true;
        }
        lightPosition = Vector3.zero;
        return false;
    }

    void UpdateReticlePosition()
    {
        Vector2 canvasDim = reticleCanvas.rect.size;
        Vector2 newReticlePosition = reticleUI.anchoredPosition + onMoveInput * reticleSpeed * Time.deltaTime / canvasDim;
        newReticlePosition.x = Mathf.Clamp(newReticlePosition.x, -canvasDim.x/2, canvasDim.x/2);
        newReticlePosition.y = Mathf.Clamp(newReticlePosition.y, -canvasDim.y/2, canvasDim.y/2);
        reticleUI.anchoredPosition = newReticlePosition;
    }

    public Vector3 GetReticlePosition()
    {
        Vector2 canvasDim = reticleCanvas.rect.size;
        Vector3 screenReticlePosition = reticleUI.anchoredPosition / canvasDim + Vector2.one * 0.5f;
        screenReticlePosition = screenReticlePosition * mainCamera.pixelRect.size;
        screenReticlePosition.z = 1;

        Vector3 projectedReticlePosition = mainCamera.ScreenToWorldPoint(screenReticlePosition);
        Vector3 direction = (projectedReticlePosition - mainCamera.transform.position).normalized;

        int groundLayerMask = 1 << 3;
        RaycastHit hitInfo;
        float maxDist = 1000;

        Physics.Raycast(mainCamera.transform.position, direction, out hitInfo, maxDist, groundLayerMask);
        return hitInfo.point + lightPositionOffset * Vector3.up;
    }

    void InitializeReticlePosition() {
        reticleUI.anchoredPosition = Vector2.zero;
    }
}