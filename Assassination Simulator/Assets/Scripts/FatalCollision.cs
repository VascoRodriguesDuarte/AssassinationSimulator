using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FatalCollision : MonoBehaviour
{
    [SerializeField] private Death death = default;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

   
    private void OnTriggerEnter2D(Collider2D trap)
    {
        if (trap.isTrigger == false)
        {
            return;
        }
        if(trap.CompareTag("Fatal"))
        {
            string message = trap.gameObject.GetComponent<TextMeshProUGUI>().text;
            death.Dead(message);
        }
    }
}
