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
        for (int row = 0; row < this.rows; row++)
        {

            float width = horizontalSpacing * (this.columns - 1);
            float hight = verticalSpacing * (this.rows - 1);
            Vector3 centering = new Vector2(-width / 2, -hight / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * verticalSpacing), 0.0f);

            for (int col = 0; col < this.columns; col++)
            {

                invader invader = Instantiate(this.prefabs[row], this.transform);
                if (invader != null)
                {
                    invader.killed += InvaderKilled;
                }
                Vector3 position = rowPosition;
                position.x += col * horizontalSpacing;
                invader.transform.localPosition = position;

            }
        }

    }


    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
    }
    private void Update()
    {
        this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
                continue;

            if (_direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f))
                AdvanceRow();

            else if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }
        }
    }
    private void AdvanceRow()
    {
        _direction.x *= -1.0f;
        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;

    }
    private void InvaderKilled()
    {
        
        this.amountKilled++;

        if (this.amountKilled >= this.totalInvaders)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
    private void MissileAttack()
    {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (Random.value < 1.0f / (float)this.amountAlive)
            {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                SoundEffectManager.play("invadershoot");
                break;
            }
        }
    }

}
