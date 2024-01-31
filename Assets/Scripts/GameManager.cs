using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Game Manager is a singleton class that manages the game state.
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int points;
    [SerializeField]
    private int time;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        points = 0;
    }
}
