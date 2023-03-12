using UnityEngine;
using TMPro;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public enum player
    {
        you = 1, opponent = 2
    }

    [SerializeField]private int pointsToWin;
    [SerializeField] private Transform UI;
    private TextMeshProUGUI messageField;
    private GameObject resetButton;
    private TextMeshProUGUI scoreUI;
    private int txtAlfa = 0;
    private scoreTracker scoreTracker;
    private BrickManager brickManager;
    [SerializeField]private Transform ball;

    private AudioSource bgm;
    // Start is called before the first frame update
    void Start()
    {
        messageField = UI.Find("messageField").GetComponent<TextMeshProUGUI>();
        scoreUI = UI.Find("Score").GetComponent<TextMeshProUGUI>();
        resetButton = UI.Find("Reset").gameObject;


        scoreUI.color = new Color(255,0,0,0);
        TextMeshProUGUI[] scoreUIs = { scoreUI, GameObject.Find("bgScore").GetComponent<TextMeshProUGUI>() };

        scoreTracker = new scoreTracker(pointsToWin, scoreUIs);
        brickManager = GetComponent<BrickManager>();
        bgm = GetComponent<AudioSource>();

        StartCoroutine(resetBall());
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(ball.transform.position.y) > CameraHandler.playArea.height/2)
            Score(ball.transform.position.y);
    }

    private void Score(float ballHeight){
        bgm.Stop();
        player scoringPlayer = ballHeight > 0 ? player.you : player.opponent;
        player loser = scoringPlayer == player.you ? player.opponent : player.you;

        brickManager.resetPlayer(loser);

        if(scoreTracker.AddPoint((int)scoringPlayer)){
            EndGame(scoringPlayer);
            return;
        }
        StartCoroutine(delayFade());
        StartCoroutine(resetBall());

    }

    /// <summary>
    /// fades the score
    /// </summary>
    private IEnumerator delayFade(){
        scoreUI.color = new Color(scoreUI.color.r, scoreUI.color.g, scoreUI.color.b, 1f);
        yield return new WaitForSeconds(2);

        while (scoreUI.color.a > 0f){
            scoreUI.color = new Color(scoreUI.color.r, scoreUI.color.g, scoreUI.color.b, scoreUI.color.a - (Time.deltaTime / 1f));
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator resetBall(){


        ball.gameObject.SetActive(true);
        ball.position = new Vector2(0, 0);
        BallMovement ballM = ball.GetComponent<BallMovement>();
        ballM.active = false;
        
        int timer = 3;
        for (int i = timer; i > 0; i--)
        {
            messageField.text = $"{i}";
            yield return new WaitForSeconds(1);
        }

        bgm.Play();

        messageField.text = "";
        ballM.active = true;
        ballM.Reset();
    }

    /// <summary>
    /// stops the game and shows if the player has won
    /// </summary>
    /// <param name="winningPlayer">the player who has won</param>
    private void EndGame(player winningPlayer){
        ball.gameObject.SetActive(false);
        ball.transform.position = new Vector2(0, 0);

        resetButton.SetActive(true);
        scoreUI.color = new Color(scoreUI.color.r, scoreUI.color.g, scoreUI.color.b, 1f);
        if(winningPlayer == player.you)
            messageField.text = "You win!";
        else
            messageField.text = "you lose";
    }

    /// <summary>
    /// resets the game back to the begin point
    /// </summary>
    public void ResetGame(){
        scoreTracker.reset();
        StartCoroutine(resetBall());
        resetButton.SetActive(false);
        scoreUI.color = new Color(scoreUI.color.r, scoreUI.color.g, scoreUI.color.b, 0f);
        brickManager.resetGrid();
    }
}
