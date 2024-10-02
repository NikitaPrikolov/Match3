using UnityEngine;
using System.Collections.Generic;

public class RaycastController : MonoBehaviour
{
    public LayerMask targetLayer;
    public float rayLength = 10f;
    private HashSet<Tile> teleportedTiles = new HashSet<Tile>();
    public TileManager tileManager;
    public ScoreManager scoreManager;

    private void Start()
    {
        InvokeRepeating("ExecuteMethods", 0f, 0.7f);
    }

    private void ExecuteMethods()
    {
        teleportedTiles.Clear();
        ShootVerticalRays();
        ShootHorizontalRays();
    }

    void ShootVerticalRays()
    {
        for (int x = 0; x < 5; x++)
        {

            Vector2 startPosition = new Vector2(x, 0f);
            RaycastHit2D[] hits = Physics2D.RaycastAll(startPosition, Vector2.up, rayLength, targetLayer);
            if (hits.Length == 5)
            {
                CheckForMatches(hits, true, x);
            }
        }
    }

    void ShootHorizontalRays()
    {
        for (int y = 0; y < 5; y++)
        {
            Vector2 startPosition = new Vector2(0f, y);
            RaycastHit2D[] hits = Physics2D.RaycastAll(startPosition, Vector2.right, rayLength, targetLayer);
            if (hits.Length == 5)
            {
                CheckForMatches(hits, false, y);
            }
        }
    }

    void CheckForMatches(RaycastHit2D[] hits, bool isVertical, int index)
    {
        if (hits.Length < 3) return;
        string currentTag = hits[0].collider?.gameObject.tag;
        int count = 1;

        for (int i = 1; i < hits.Length; i++)
        {
            if (hits[i].collider != null && hits[i].collider.gameObject.tag == currentTag)
            {
                count++;
            }
            else
            {
                if (count >= 3)
                {
                    TeleportElements(hits, isVertical, index, count, i - count);
                    CalculatePoints(count);
                }
                currentTag = hits[i].collider?.gameObject.tag;
                count = 1;
            }
        }

        if (count >= 3)
        {
            TeleportElements(hits, isVertical, index, count, hits.Length - count);
            CalculatePoints(count);
        }
    }
    private void CalculatePoints(int count)
    {
        scoreManager.AddPoints(count);
        scoreManager.AddMoves(count - 2);
    }
    void TeleportElements(RaycastHit2D[] hits, bool isVertical, int index, int count, int startingIndex)
    {
        HashSet<Tile> teleportedSet = new HashSet<Tile>();

        for (int i = 0; i < count; i++)
        {
            RaycastHit2D hit = hits[startingIndex + i];
            if (hit.collider != null)
            {
                Tile tile = hit.collider.gameObject.GetComponent<Tile>();
                if (teleportedSet.Contains(tile) || tile.isTeleporting) continue; // Проверяем состояние teleport
                tile.Teleport(5f);
                teleportedSet.Add(tile);
                teleportedTiles.Add(tile);
            }
        }
    }
}
