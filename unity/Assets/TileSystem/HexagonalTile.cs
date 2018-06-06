using UnityEngine;
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
    }

    private struct ArrowPointing
    {
        public Transform arrow;
        public HexagonalTile target;

        public ArrowPointing(Transform arrow, HexagonalTile target)
        {
            this.arrow = arrow;
            this.target = target;
        }
    }

    public Neighbours neighbours;
    public Transform arrowPrefab;

    private List<ArrowPointing> arrowsToTiles = new List<ArrowPointing>(6);

    public void FindMyNeighbours(Neighbours neighbours)
    {
        float sqrt3 = Mathf.Sqrt(3);

        neighbours.left = NearestTileTo(2 * Vector3.left);
        neighbours.right = NearestTileTo(2 * Vector3.right);
        neighbours.upLeft = NearestTileTo(new Vector3(-1, 0, sqrt3));
        neighbours.downLeft = NearestTileTo(new Vector3(-1, 0, -sqrt3));
        neighbours.upRight = NearestTileTo(new Vector3(1, 0, sqrt3));
        neighbours.downRight = NearestTileTo(new Vector3(1, 0, -sqrt3));
    }

    private HexagonalTile NearestTileTo(Vector3 direction)
    {
        direction.y -= 0.5f;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, 2.0f);

        System.Func<float, float> f = (x) => (x + 2) / 4;
        Color arrowColor = new Color(f(direction.x), 0, f(direction.z));

        if (hits.Length > 0) {
            HexagonalTile result = hits[0].transform.GetComponentInParent<HexagonalTile>();
            if (result == this && hits.Length > 1)
                result = hits[1].transform.GetComponentInParent<HexagonalTile>();
            if (result != this)
            {
                Transform arrow = Instantiate<Transform>(arrowPrefab, transform);
                arrow.GetComponentInChildren<MeshRenderer>().material.color = arrowColor;
                arrowsToTiles.Add(new ArrowPointing(arrow, result));
                return result;
            }
        }
        return null;
    }

    // Use this for initialization
    void Start () {
        FindMyNeighbours(neighbours);
        GetComponentInChildren<TextMesh>().text = name.Trim("GroundTilecvybt ()".ToCharArray());
	}
	
	// Update is called once per frame
	void Update () {
        foreach (ArrowPointing arrowToTile in arrowsToTiles)
        {
            arrowToTile.arrow.LookAt(arrowToTile.target.transform, Vector3.up);
            float distance = Vector3.Distance(arrowToTile.arrow.position, arrowToTile.target.transform.position);
            arrowToTile.arrow.localScale = new Vector3(distance, distance, distance);
        }
	}

}

