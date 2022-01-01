using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageBody : Body
{
    public GameObject DeathMenuUi;
    public GameObject WinMenuUi;
    
    protected override void Start()
    {
        base.Start();
        SoundManager.GetInstance().Play("carriage", this.gameObject);
    }
    protected override void Death()
    {
        Debug.Log("GameOver");
        DeathMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.isGameOver = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FinishZone"))
        {
            Win();
        }
    }

    void Win()
    {
        Debug.Log("GameWin");
        WinMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.isGameWin = true;
    }
}
