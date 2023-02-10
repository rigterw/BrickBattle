using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    [SerializeField] private int rows;
    [SerializeField] private int collums;

    private List<GameObject> p1Bricks = new List<GameObject>();
    private List<GameObject> p2Bricks = new List<GameObject>();
    void Start()
    {
        CreateGrid();
    }

    private void CreateGrid(){
        p1Bricks.Add(Instantiate(brick));
        Debug.Log(p1Bricks.Count());
        Destroy(p1Bricks[0]);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(p1Bricks[0]);
    }
}
