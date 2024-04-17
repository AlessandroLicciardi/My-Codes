using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{   
    public Slider Health;

    public void UpdateHud(Player _player)
    {
        Health.value = _player.CurrentHealth;
    }
}
