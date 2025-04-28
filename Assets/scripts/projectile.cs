using System;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;
    public int damage = 1;
    private ScoreUI scoreUI;
    //public GameObject playerExplosionPrefab;

    //private void Start()
    //{
    //    scoreUI = GameObject.Find("ScoreManager").GetComponent<ScoreUI>();
    //}
    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.destroyed != null)
        {
            this.destroyed.Invoke();
        }
            Destroy(this.gameObject);
    }
}
