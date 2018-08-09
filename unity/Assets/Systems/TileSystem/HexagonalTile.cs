using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class HexagonalTile : MonoBehaviour {

    [System.Serializable]
    public class Neighbours
    {
        public HexagonalTile left;
        public HexagonalTile right;
        public HexagonalTile upLeft;
        public HexagonalTile upRight;
        public HexagonalTile downLeft;
        public HexagonalTile downRight;
        public HexagonalTile up;
        public HexagonalTile down;
    }

    public Neighbours neighbours;
    public bool snapNeighbours = false;
    public HexagonalTile translucidPrefab;
    public LayerMask transparentBlocksLayerMask;

    public void FindMyNeighbours(Neighbours neighbours)
    {
        float sqrt3 = Mathf.Sqrt(3);

        neighbours.left = NearestTileTo(1.732f * Vector3.left);
        neighbours.right = NearestTileTo(1.732f * Vector3.right);
        neighbours.upLeft = NearestTileTo(1.732f * new Vector3(-1, 0, sqrt3).normalized);
        neighbours.downLeft = NearestTileTo(1.732f * new Vector3(-1, 0, -sqrt3).normalized);
        neighbours.upRight = NearestTileTo(1.732f * new Vector3(1, 0, sqrt3).normalized);
        neighbours.downRight = NearestTileTo(1.732f * new Vector3(1, 0, -sqrt3).normalized);
        neighbours.up = NearestTileTo(Vector3.up);
        neighbours.down = NearestTileTo(Vector3.down);
    }

    /// <summary>
    /// Prints a line in the form
    /// "game object name         raycast direction      [tiles hit (distance)]"
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="hits"></param>
    private void DebugRaycast(Vector3 direction, RaycastHit[] hits)
    {
        Func<Transform, double> dist = t => Vector3.Distance(transform.position, t.position);
        Func<string, string> pad = str => str + "<color=#b0b0b0>" + new string('_', Mathf.Max(0, 25 - str.Length)) + "</color>";
        //Func<string, string> pad = str => str + "<color=#dbdbdb>" + new string('_', Mathf.Max(0, 25 - str.Length)) + "</color>";

        string hitsLog = System.Linq.Enumerable.Aggregate<RaycastHit, string>(
            source: hits,
            seed: "",
            func: (acc, hit) => acc +
                                (acc.Length == 0 ? "" : ", ") +
                                hit.transform.name +
                                " ↔ <b>" +
                                dist(hit.transform).ToString("F2") +
                                "</b>");

        //Debug.Log(pad(name) + pad(direction.ToString()) + "[" + hitsLog + "]");

        Func<float, float> f = (x) => (x + 2) / 4;
        Color arrowColor = new Color(f(direction.x), f(direction.y), f(direction.z));
        Debug.DrawRay(transform.position, direction, arrowColor, 1, false);
    }

    private HexagonalTile NearestTileTo(Vector3 direction)
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, 2.0f, ~transparentBlocksLayerMask);

        Func<RaycastHit, double> dist = hit => Vector3.Distance(transform.position, hit.transform.position);
        Comparison<RaycastHit> byDist = (t1, t2) => (int)(1000000 * (dist(t1) - dist(t2)));
        Array.Sort<RaycastHit>(hits, byDist);

        DebugRaycast(direction, hits);

        if (hits.Length > 0) {
            HexagonalTile neighbour = hits[0].transform.GetComponentInParent<HexagonalTile>();
            if (snapNeighbours) {
                neighbour.transform.position = transform.position;
                neighbour.transform.Translate(direction);
            }
            return neighbour;
        }

        // Check if there isn't a translucidTile here already
        hits = Physics.RaycastAll(transform.position, direction, 2.0f, transparentBlocksLayerMask);
        Array.Sort<RaycastHit>(hits, byDist);

        if (hits.Length > 0) {
            return hits[0].transform.GetComponentInParent<HexagonalTile>();
        }

        // No translucidTile found, create a new one
        HexagonalTile translucidTile = Instantiate<HexagonalTile>(translucidPrefab, transform.parent);
        translucidTile.transform.position = transform.position;
        translucidTile.transform.Translate(direction);
        return translucidTile; 
        
    }

    // Use this for initialization
    void Start () {
        if (translucidPrefab != null) {
            FindMyNeighbours(neighbours);
        }
	}
	
}

