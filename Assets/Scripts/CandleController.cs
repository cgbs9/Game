using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CandleController : MonoBehaviour
{
    public GameObject[] candles;  // Array to hold the candle GameObjects
    public Sprite litCandleSprite;  // Sprite for the lit candle
    public Sprite unlitCandleSprite;  // Sprite for the unlit candle
    public GameObject phoenix;  // The Phoenix GameObject
    public Vector3 phoenixTargetPosition;  // Target position for the Phoenix to fly to
    public float phoenixMoveDuration = 5.0f;  // Duration for the Phoenix to reach the target position
    public GameObject featherPrefab;  // Prefab of the feather GameObject
    public string nextSceneName = "LS_2";  // Name of the scene to switch to

    private bool[] candlesLit;  // Array to track the state of each candle
    private List<int> litOrder;  // List to track the order of lit candles
    private Animator phoenixAnimator;
    private bool featherDropped = false;

    // Define the correct order here (0-based index)
    public int[] correctOrder = { 0, 1, 2 };

    void Start()
    {
        candlesLit = new bool[candles.Length];
        litOrder = new List<int>();
        phoenixAnimator = phoenix.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LightNearbyCandles();
        }
    }

    void LightNearbyCandles()
    {
        for (int i = 0; i < candles.Length; i++)
        {
            GameObject candle = candles[i];
            float distance = Vector2.Distance(transform.position, candle.transform.position);
            if (distance <= 2f && !candlesLit[i])  // You can adjust this distance as needed
            {
                ToggleCandle(candle, i);
                break;  // Ensure only one candle is lit per key press
            }
        }
    }

    void ToggleCandle(GameObject candle, int index)
    {
        SpriteRenderer sr = candle.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            candlesLit[index] = !candlesLit[index];
            sr.sprite = candlesLit[index] ? litCandleSprite : unlitCandleSprite;

            if (candlesLit[index])
            {
                litOrder.Add(index);
                CheckOrder();
            }
            else
            {
                litOrder.Remove(index);  // If candle is unlit, remove from the order
            }
        }
    }

    void CheckOrder()
    {
        if (litOrder.Count == correctOrder.Length)
        {
            for (int i = 0; i < correctOrder.Length; i++)
            {
                if (litOrder[i] != correctOrder[i])
                {
                    Debug.Log("Incorrect order! Resetting...");
                    ResetCandles();
                    return;
                }
            }

            Debug.Log("Correct order! Puzzle solved!");
            TriggerPhoenixFlyAway();
        }
    }

    void ResetCandles()
    {
        for (int i = 0; i < candles.Length; i++)
        {
            candlesLit[i] = false;
            SpriteRenderer sr = candles[i].GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = unlitCandleSprite;
            }
        }
        litOrder.Clear();
    }

    void TriggerPhoenixFlyAway()
    {
        // Trigger the fly away animation
        phoenixAnimator.SetTrigger("FlyAwayTrigger");

        // Drop the feather if it hasn't been dropped already
        if (!featherDropped)
        {
            DropFeather();
            featherDropped = true;
        }

        // Move the Phoenix to the target position
        StartCoroutine(MovePhoenixToTarget());
    }

    void DropFeather()
    {
        // Instantiate the feather at the Phoenix's position
        Instantiate(featherPrefab, phoenix.transform.position, Quaternion.identity);
    }

    IEnumerator MovePhoenixToTarget()
    {
        float elapsed = 0;
        Vector3 startPosition = phoenix.transform.position;

        while (elapsed < phoenixMoveDuration)
        {
            phoenix.transform.position = Vector3.Lerp(startPosition, phoenixTargetPosition, elapsed / phoenixMoveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        phoenix.transform.position = phoenixTargetPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && featherDropped)
        {
            Debug.Log("Player collided with feather and featherDropped is true.");

            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("F key pressed. Loading next scene.");
                SceneManager.LoadScene("LS_2");
            }
        }
    }

}
