using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanatoryAlerts : MonoBehaviour
{
    [SerializeField] private AlertsSystem _alertsSystem;

    public void AlertNeutral(string text) 
    {
        // Eh... Unity buttons system is so rigid
        _alertsSystem.PushAlert(new AlertsSystem.Alert(text, _alertsSystem.NEUTRAL_ALERT_COLOR, 2f));
    }
    
}
