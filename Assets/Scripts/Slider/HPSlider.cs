using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    [SerializeField] CarriageBody MyCarriage;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Slider>().maxValue = MyCarriage.maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Slider>().value = MyCarriage.hp;
    }
}
