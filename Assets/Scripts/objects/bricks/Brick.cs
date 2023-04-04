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
    private IEnumerator Destroy(){
        manager.Remove(gameObject);
        GetComponent<ParticleSystem>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(!other.gameObject.CompareTag("Ball"))return;

        StartCoroutine(Destroy());
    }
}
