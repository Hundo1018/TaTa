using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player _player;
    [SerializeField] private Castle _castle;
    //delegate void Spawn();
    public event Action AddMinionToQueue;
    private void Awake()
    {
        _player = GetComponent<Player>();
        AddMinionToQueue += _castle.AddMinionToQueue;
    }

    /// <summary>
    /// «ö¤U¥Í¦¨«öÁä
    /// </summary>
    public void OnSpawnButtonDown()
    {
        AddMinionToQueue.Invoke();
    }
}