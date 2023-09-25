using UnityEngine;

/// <summary>
/// Spawns cubes and controll ball movement and color. Singleton.
/// </summary>
public class GameController : MonoBehaviour
{
    /*
     * The 3D paradigm consisted of a very light-flooded forest environment,
     * which was a free Demo retrieved from the Unity Asset Store (Fantasy
     * Forest Environment from TriForge Assets).
     * Camera & World:
     * The users saw the environment
     * in first-person perspective and saw in front of them a green ball, that
     * would roll along a predefined path and collect light blue floating cubes,
     * which marked the path. When the ball was moving, the camera was moving
     * behind the ball and would rotate with the ball when taking curves, so
     * that the user would have the sensation of following the ball on its way.
     *
     * The ball would move in a constant speed every time the EEG power exceeded
     * the predefined SMR-threshold, which was set for every participant individually
     * based on an initial baseline run. The ball would stand still when the signal
     * was below this threshold. Additionally, the ball would change its color to
     * red and stand still, whenever Beta or Theta were over the individually set
     * thresholds and therefore the artifacts were too high, for example due to eye
     * movements/blinking or muscle artifacts. This means the ball would only move
     * when both the artifact and SMR values would be in the desired, predefined range.
     */
    public static GameController Instance;
    [SerializeField] private GameObject ball, spline;
    [SerializeField] private GameObject coinCubePrefab;
    private GameObject[] coins;
    private Vector3[] positionList;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        InitializeCoins();
    }

    private void Update()
    {
        SetBallMovementAndColor();
    }



    private void SetBallMovementAndColor()
    {
        
    }
    
    private void InitializeCoins()
    {
        // Initialize Coins
        var coinCubeRotation = coinCubePrefab.transform.rotation;
        coins = new GameObject[spline.transform.childCount];
        for (int i = 0; i < spline.transform.childCount; i++)
        {
            coins[i] = Instantiate(coinCubePrefab, spline.transform.GetChild(i).position,
                Quaternion.Euler(coinCubeRotation.x, Random.Range(0f, 360f), coinCubeRotation.z));
        }
    }
    
    public void ResetRound()
    {
        // Reactivate all coins
        foreach (var coin in coins)
        {
            coin.gameObject.SetActive(true);
        }
    }
    
    
}
