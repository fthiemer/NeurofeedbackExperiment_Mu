using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] private GameObject player, spline;
    private Vector3[] positionList;
    
    private void Awake()
    {
        if (GameController.Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //fill position list
        foreach (var childTransform in spline.transform.GetComponentsInChildren<Transform>())
        {
            
        }
    }


    public void ResetRound()
    {
        // Spawn all cubes on position 
        
    }
}
