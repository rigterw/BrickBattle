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
            Debug.LogError("No gridHeight given!!!");
            gridHeight = 2.5f;
        }

        setSize();
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

    public void resetPlayer(GameManager.player player){
        List<GameObject> temp = new List<GameObject>();
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < collums; c++)
            {
                temp.Add(SpawnBrick(r, calculatePosition(r,c,player == GameManager.player.opponent)));
            }
        }
        if(player != GameManager.player.opponent){
            foreach (GameObject brick in p1Bricks){
                Destroy(brick);
            }
            p1Bricks = temp;
        }else{
            foreach (GameObject brick in p2Bricks){
                Destroy(brick);
            }
            p2Bricks = temp;
        }
    }

    public void resetGrid(){
        foreach (GameObject brick in p2Bricks){
            Destroy(brick);
        }
        foreach (GameObject brick in p2Bricks){
            Destroy(brick);
        }

        CreateGrid();
    }

    /// <summary>
    /// Creates a new brick object
    /// </summary>
    /// <param name="r">the row of the brick  </param>
    /// <param name="c">the colum of the brick</param>
    private GameObject SpawnBrick(int r, Vector2 position){
        GameObject brickObject = Instantiate(brick, position, transform.rotation);
        brickObject.GetComponent<Brick>().Init(this, rowColors[r]);
        brickObject.transform.SetParent(transform);
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
        return new Vector2(calculateX(c), calculateY(r,top));
    }

    /// <summary>
    /// calculates the X position for the brick
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    private float calculateX(int c){
        float spacing = (CameraHandler.playArea.width - paddingX) / collums;
        float Xoffset = CameraHandler.playArea.xMin + paddingX + 0.5f * spacing;
        return c * spacing + Xoffset;
    }

    /// <summary>
    /// sets the size of the bricks
    /// </summary>
    private void setSize(){
        float width = (CameraHandler.playArea.width - paddingX) / collums;
        float height = (gridHeight - paddingY) / rows;
        Vector2 size = new Vector2(width, height);

        //sets the image size
        SpriteRenderer spriteRenderer = brick.GetComponent<SpriteRenderer>();
        spriteRenderer.size = size;

        //sets the collider size
        BoxCollider2D collider = brick.GetComponent<BoxCollider2D>();
        collider.size = size;

    }

    /// <summary>
    /// calculates the Y position for the brick
    /// </summary>
    /// <param name="r">the row of the brick</param>
    /// <param name="top">whether the brick is for the top player or not</param>
    /// <returns></returns>
    private float calculateY(int r, bool top){
        float spacing = (gridHeight - paddingY) / rows;

        //whether the grid is build from middle to up or down
        int director = top ? 1 : -1;

        float borderY = top ? CameraHandler.playArea.yMax : CameraHandler.playArea.yMin;

        //the starting point to build the grid from
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
