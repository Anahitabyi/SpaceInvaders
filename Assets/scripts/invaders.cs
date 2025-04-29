using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class invaders : MonoBehaviour
{
    public invader[] prefabs;

    public int rows = 5;

    public int columns = 6;

    private Vector3 _direction = Vector3.right;

    public AnimationCurve speed;

    public projectile missilePrefab;

    public float missileAttackRate = 1.0f;
    public int amountAlive => this.totalInvaders - this.amountKilled;
    public int amountKilled { get; private set; }
    public int totalInvaders => this.rows * this.columns;

    public float percentKilled => (float)this.amountKilled / (float)this.totalInvaders;

    public float horizontalSpacing = 2.5f;
    public float verticalSpacing = 2.5f;

    private void Awake()
    {
        //create the grid of invaders. 
        for (int row = 0; row < this.rows; row++)
        {
            //calculate total grid width and height for centering
            float width = horizontalSpacing * (this.columns - 1);
            float hight = verticalSpacing * (this.rows - 1);
            Vector3 centering = new Vector2(-width / 2, -hight / 2);
            //calculate starting position for this row
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * verticalSpacing), 0.0f);
            //create each invader in the row
            for (int col = 0; col < this.columns; col++)
            {
                //instantiate invader and subscribe to its killed event
                invader invader = Instantiate(this.prefabs[row], this.transform);
                if (invader != null)
                {
                    invader.killed += InvaderKilled;
                }
                //position the invader in the grid
                Vector3 position = rowPosition;
                position.x += col * horizontalSpacing;
                invader.transform.localPosition = position;

            }
        }

    }


    private void Start()
    {
        //start the regular missile attack
        InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
    }
    private void Update()
    {
        // move the entire formation based on the currect speed. (adjusted by killed percentage.)
        this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;
        // get screen boundaries.
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        //check if any invader has reached the screen edge.
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
                continue;
            // right edge check
            if (_direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f))
                AdvanceRow();
            // left edge check
            else if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }
        }
    }
    //handle movement when invaders reach the screen edge.
    private void AdvanceRow()
    {
        //reverse horizontal direction
        _direction.x *= -1.0f;

        //move formation downward
        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;

    }
    //this is called when an invader is kiiled
    private void InvaderKilled()
    {
        
        this.amountKilled++;
        //cheeck for victory condition(all invaders are dead)
        if (this.amountKilled >= this.totalInvaders)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
    private void MissileAttack()
    {
        //randomly select an anctive invader to shoot.
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            //probability based on active invaders.
            if (Random.value < 1.0f / (float)this.amountAlive)
            {
                //launch the missile from the selected invader
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                SoundEffectManager.play("invadershoot");
                break; //only one misile per attack
            }
        }
    }

}
