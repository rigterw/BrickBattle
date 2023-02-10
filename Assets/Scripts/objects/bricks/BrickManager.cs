using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    [SerializeField] private int rows;
    [SerializeField] private int collums;
    [SerializeField] private Color[] rowColors;

    private List<GameObject> p1Bricks = new List<GameObject>();
    private List<GameObject> p2Bricks = new List<GameObject>();
    void Start()
    {
        CreateGrid();
    }

    private void CreateGrid(){
        p1Bricks.Add(SpawnBrick(0,0));
    }

    /// <summary>
    /// Creates a new brick object
    /// </summary>
    /// <param name="r">the row of the brick  </param>
    /// <param name="c">the colum of the brick</param>
    private GameObject SpawnBrick(int r, int c, bool top = false){
        GameObject brickObject = Instantiate(brick);
        brickObject.GetComponent<Brick>().Init(this, rowColors[r]);

        return brickObject;
    }

    public void Remove(GameObject brick){
        if(p1Bricks.Contains(brick)){
            p1Bricks.Remove(brick);
        }else if(p2Bricks.Contains(brick)){
            p2Bricks.Remove(brick);
        }else{
            Debug.LogError("brickmanager got passed a brick that wasn't in any list");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
