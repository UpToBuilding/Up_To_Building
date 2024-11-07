using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    [Header("Boss Settings")] 
    public Boss _boss;

    [Header("UI Settings")] 
    public Slider _hpSlider;
    
    
    void Start()
    {
        if (_boss != null && _hpSlider != null)
        {
            _hpSlider.maxValue = _boss.MaxHp;
            _hpSlider.value = _boss.MaxHp;
        }
        else
        {
            Debug.Log("Boss 혹은 Slider 할당 안됨");
        }
    }

    void Update()
    {
        if ((object)_boss != null && (object)_hpSlider != null)
        {
            _hpSlider.value = _boss.CurrentHp;
        }
    }
}
