using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Brick : MonoBehaviour
{
    BrickManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(BrickManager manager, Color color){
        this.manager = manager;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = color;
    }

    /// <summary>
    /// destroys this gameObject
    /// </summary>
    private void Destroy(){
        manager.Remove(gameObject);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(!other.gameObject.CompareTag("Ball"))return;

        Destroy();
    }
}
