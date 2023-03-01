using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    //variables used in grid positions calculation
    [SerializeField] private int rows;
    [SerializeField] private int collums;
    [SerializeField] private float gridHeight;
    [SerializeField] private float offset;
    [SerializeField] private float paddingX;
    [SerializeField] private float paddingY;

    [SerializeField] private Color[] rowColors;
    [SerializeField] private GameObject brick;


    private List<GameObject> p1Bricks = new List<GameObject>();
    private List<GameObject> p2Bricks = new List<GameObject>();
    void Start()
    {
        if(gridHeight <= 0){
            gridHeight = collums * (paddingY + brick.GetComponent<Renderer>().bounds.size.y);
        }
        CreateGrid();
    }

    private void CreateGrid(){
        for (int r = 0; r < rows; r++){
            for (int c = 0; c < collums; c++){
                p1Bricks.Add(SpawnBrick(r, calculatePosition(r,c,false)));
                p1Bricks[r * c].name = "p1";
                p2Bricks.Add(SpawnBrick(r, calculatePosition(r, c, true)));
            }
        }
        
    }

    /// <summary>
    /// Creates a new brick object
    /// </summary>
    /// <param name="r">the row of the brick  </param>
    /// <param name="c">the colum of the brick</param>
    private GameObject SpawnBrick(int r, Vector2 position){
        GameObject brickObject = Instantiate(brick, position, transform.rotation);
        brickObject.GetComponent<Brick>().Init(this, rowColors[r]);

        return brickObject;
    }

    /// <summary>
    /// calculates the position for the new brick
    /// </summary>
    /// <param name="r">the row of the brick</param>
    /// <param name="c">the colum of the brick</param>
    /// <param name="top">whether the brick is for the top player or not</param>
    /// <returns></returns>
    private Vector2 calculatePosition(int r, int c, bool top){
        return new Vector2(calculateX(c, setWidth()), calculateY(r,top));
    }

    /// <summary>
    /// calculates the X position for the brick
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    private float calculateX(int c, float spacing){
        float Xoffset = CameraHandler.playArea.xMin + paddingX + 0.5f * spacing;
        return c * spacing + Xoffset;
    }

    private float setWidth(){
        float spacing = (CameraHandler.playArea.width - paddingX) / collums;

        var rectTransform = brick.GetComponent<RectTransform>();
		if (rectTransform != null)
		{
            //changes the width of the bricks so they don't overlap
            Vector3 scale = brick.transform.localScale;
            scale.x =  (spacing*10 - paddingX) / brick.GetComponent<Renderer>().bounds.size.x;
            brick.transform.localScale = scale;
        }

        return spacing;
    }

    /// <summary>
    /// calculates the Y position for the brick
    /// </summary>
    /// <param name="r">the row of the brick</param>
    /// <param name="top">whether the brick is for the top player or not</param>
    /// <returns></returns>
    private float calculateY(int r, bool top){
        float spacing = (gridHeight - paddingY) / rows;

        int director = top ? 1 : -1;
        float borderY = top ? CameraHandler.playArea.yMax : CameraHandler.playArea.yMin;

        float Yoffset = borderY - director * (offset + gridHeight);
        return r * director * spacing + Yoffset;

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
}
