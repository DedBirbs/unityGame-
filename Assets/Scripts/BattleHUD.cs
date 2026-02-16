using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Slider hpSlider;
    public Unit unit;

    void Start()
    {
        hpSlider.maxValue = unit.HP;
        hpSlider.value = unit.currentHP;
    }

    void Update()
    {
        hpSlider.value = unit.currentHP;
    }
}
