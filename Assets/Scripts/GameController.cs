using System;
using BezierSolution;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Spawns cubes and control ball movement and color. Singleton.
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
    
    private const float MovementSpeed = 1.5f; // 
    
    public static GameController Instance;
    [SerializeField] private GameObject ball, ballParent, spline;
    [SerializeField] private BezierWalkerWithSpeed bezierWalker;
    [SerializeField] private GameObject coinCubePrefab;

    [SerializeField] private Color 
        artifactColor = Color.red, // Theta or Beta too high
        normalColor = new(0f,220f,0f,255f); //green taken from Berger et al. 2021 with ColorCop

    public enum BallState {
        OverThreshold,
        UnderThreshold,
        ArtifactsTooHigh //Beta or Theta too high
    }

    public BallState ballState = BallState.UnderThreshold;
    private GameObject[] coins;
    private Vector3[] positionList;
    private Material ballRendererMat;
    private BallRotate ballRotate;
    private Vector3 initialBallParentPosition;
    

    private void Awake()
    {
        // Initialize Variables
        var ballRenderer = ball.GetComponent<Renderer>();
        if (ballRenderer is null) Debug.LogError("Ball Renderer is null");
        ballRendererMat = ballRenderer.material;
        initialBallParentPosition = ballParent.transform.position;
        ballRotate = ball.GetComponent<BallRotate>();
        
        // set angular velocity of ball to be realistic
        float radius = ball.GetComponent<SphereCollider>().radius;
        var angularVelocity = MovementSpeed / (2 * Math.PI * Math.Pow(radius, 2));
        ballRotate.angularVelocityInDegree = Quaternion.Euler(Vector3.right * (float) angularVelocity);
        
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
        ballRotate.rotate = ballState == BallState.OverThreshold;
        bezierWalker.speed = ballState == BallState.OverThreshold ? MovementSpeed : 0f;
        ballRendererMat.color = ballState == BallState.ArtifactsTooHigh ? artifactColor : normalColor;
    }
    
    
    private void InitializeCoins()
    {
        // Initialize Coins
        var coinCubeRotation = coinCubePrefab.transform.rotation;
        coins = new GameObject[spline.transform.childCount];
        for (var i = 0; i < spline.transform.childCount; i++)
        {
            coins[i] = Instantiate(coinCubePrefab, spline.transform.GetChild(i).position,
                Quaternion.Euler(coinCubeRotation.x, Random.Range(0f, 360f), coinCubeRotation.z));
        }
    }
    
    public void ResetRound()
    {
        RespawnCoins();
        // Reset Ball Position
        ballParent.transform.position = ball.transform.position = initialBallParentPosition;
    }

    public void RespawnCoins()
    {
        // Reactivate all coins
        foreach (var coin in coins)
        {
            coin.gameObject.SetActive(true);
        }
    }
}
