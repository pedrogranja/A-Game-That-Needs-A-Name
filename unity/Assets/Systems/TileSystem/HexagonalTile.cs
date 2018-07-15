using UnityEngine;
using System;
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

    public void FindMyNeighbours(Neighbours neighbours)
    {
        float sqrt3 = Mathf.Sqrt(3);

        neighbours.left = NearestTileTo(Vector3.left);
        neighbours.right = NearestTileTo(Vector3.right);
        neighbours.upLeft = NearestTileTo(new Vector3(-1, 0, sqrt3));
        neighbours.downLeft = NearestTileTo(new Vector3(-1, 0, -sqrt3));
        neighbours.upRight = NearestTileTo(new Vector3(1, 0, sqrt3));
        neighbours.downRight = NearestTileTo(new Vector3(1, 0, -sqrt3));
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
        //Func<string, string> pad = str => str + "<color=#b0b0b0>" + new string('_', Mathf.Max(0, 25 - str.Length)) + "</color>";
        Func<string, string> pad = str => str + "<color=#dbdbdb>" + new string('_', Mathf.Max(0, 25 - str.Length)) + "</color>";

        string hitsLog = System.Linq.Enumerable.Aggregate<RaycastHit, string>(
            source: hits,
            seed: "",
            func: (acc, hit) => acc +
                                (acc.Length == 0 ? "" : ", ") +
                                hit.transform.name +
                                " (<b>" +
                                dist(hit.transform).ToString("F2") +
                                "</b>)");

        Debug.Log(pad(name) + pad(direction.ToString()) + "[" + hitsLog + "]");

        Func<float, float> f = (x) => (x + 2) / 4;
        Color arrowColor = new Color(f(direction.x), f(direction.y), f(direction.z));
        Debug.DrawRay(transform.position, direction, arrowColor, 10, false);
    }

    private HexagonalTile NearestTileTo(Vector3 direction)
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, 2.0f);

        Func<RaycastHit, double> dist = hit => Vector3.Distance(transform.position, hit.transform.position);
        Array.Sort<RaycastHit>(hits, (t1, t2) => (int) (1000000 * (dist(t1) - dist(t2))));

        DebugRaycast(direction, hits);

        if (hits.Length > 0) {
            HexagonalTile result = hits[0].transform.GetComponentInParent<HexagonalTile>();
            return result;
        }
        return null;
    }

    // Use this for initialization
    void Start () {
        FindMyNeighbours(neighbours);
	}
	
}

