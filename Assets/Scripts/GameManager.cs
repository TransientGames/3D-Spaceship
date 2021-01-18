using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayerDamaged()
    {
        uiManager.UpdatePlayerHealth();
    }
}
