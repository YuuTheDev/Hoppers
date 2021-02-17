using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class PlayerHopper : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    
    private Rigidbody2D rb;
    public Text highScoreText;
    public GameObject winGame;
    public GameObject winPoint;

    private bool isGrounded;
    private bool isHitting;
    private bool isWinning;
    
    public Transform feetPos;
    public Transform facePos;
    
    public float feetCheckRadius;
    public float faceCheckX;
    public float faceCheckY;
    
    public LayerMask whatIsGround;
    public LayerMask whatIsWin;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        highScoreText.text = PlayerPrefs.GetFloat("HighScore", 0).ToString("0");
    }
    
    bool firstPlay;

    void Awake()
    {
        if (Application.isEditor == false)
        {
            if (PlayerPrefs.GetInt("First", 1) == 1)
            {
                firstPlay = true;
                PlayerPrefs.SetFloat("HighScore", 0);
                PlayerPrefs.SetInt("First", 0);
                PlayerPrefs.Save();
            }
            else
                firstPlay = false;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, feetCheckRadius, whatIsGround);
        isHitting = Physics2D.OverlapCapsule(facePos.position, new Vector2(faceCheckX, faceCheckY),
            CapsuleDirection2D.Vertical, 0, whatIsGround);
        isWinning = Physics2D.OverlapCapsule(facePos.position, new Vector2(faceCheckX, faceCheckY),
            CapsuleDirection2D.Vertical, 0, whatIsWin);
        
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isGrounded == true)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            isJumping = false;
        }

        if (isHitting)
        {
            highScoreText.GetComponent<HighScoreCounter>().checkForHighScore(rb);
            FindObjectOfType<AudioManager>().Play("Death" + Random.Range(1, 5).ToString());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (isWinning)
        {
            winGame.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Win2");
            winPoint.SetActive(false);
        }
    }
}
