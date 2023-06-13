using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeVehicleExitTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Collector collector = other.gameObject.GetComponent<Collector>();
        if(collector)
        {
            int keyCount = collector.GetItemCount("Vehicle Key");
            if(keyCount == 0)
            {
                MessageBoxUIController.GetInstance().ShowMessage("You need to find the key for the camper.");
            }
            else
            {
                GameManager.GetInstance().Win();
            }
        }
    }
}
