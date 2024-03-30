using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            LevelManager.Instance.InvokeCurrentLevelChanger();
            Debug.Log("PLAYER HEREE!!!!1");
        }
    }
}
