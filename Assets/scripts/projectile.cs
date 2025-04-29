using System;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed; //action event that gets triggered when the projectile gets destroyed.
    public int damage = 1;
    private ScoreUI scoreUI;
    //public GameObject playerExplosionPrefab;

    //private void Start()
    //{
    //    scoreUI = GameObject.Find("ScoreManager").GetComponent<ScoreUI>();
    //}
    private void Update()
    {
        //move the projectile
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //check if there are any subscribers to the destroyed event.
        if (this.destroyed != null)
        {
            //invoke the destroyed event to notify the subscribers
            this.destroyed.Invoke();
        }
            Destroy(this.gameObject);
    }
}
