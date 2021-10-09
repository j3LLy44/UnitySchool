using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        // Gets the rybycontroller script
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            //Calls the function in ruby controller to change the health
            controller.ChangeHealth(-1);
        }
    }
}
