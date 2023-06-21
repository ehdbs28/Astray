using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == 6){
            GameManager.Instance.StageEnd();
        }
    }
}
