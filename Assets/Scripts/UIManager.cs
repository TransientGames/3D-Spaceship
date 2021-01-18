using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //public Text playerHealth;
    public Slider playerShields;
    public Slider playerHealth;

    private Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Cannot find Player in UIManager");
        }
    }

    void Start()
    {

    }

    public void UpdatePlayerHealth()
    {
        playerShields.value = player.playerShields;
        playerHealth.value = player.playerHealth;
    }

}
