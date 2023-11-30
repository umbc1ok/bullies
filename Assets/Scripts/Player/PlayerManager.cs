using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private List<GameObject> players = new List<GameObject>();

    private const int MAX_PLAYERS = 4;

    private void Awake()
    {
        // Spawn players
        for (int i = 0; i < MAX_PLAYERS; ++i)
        {
            // Choose random position within the screen
            Vector2 randomPosition = new Vector2(Random.Range(-21.0f, 21.0f), Random.Range(-10.0f, 10.0f));
            Vector3 worldPosition = randomPosition;

            // Instantiate player
            players.Add(Instantiate(playerPrefab, worldPosition, Quaternion.identity));
        }
    }
}
