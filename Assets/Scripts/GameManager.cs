using UnityEngine;
using TMPro;
using System.Collections;
public class GameManager : MonoBehaviour
{
    private enum player
    {
        you = 1, opponent = 2
    }

    [SerializeField]private int pointsToWin;
    [SerializeField] private Transform UI;
    private TextMeshProUGUI scoreUI;
    private int txtAlfa = 0;
    private TextMeshProUGUI messageField;

    private scoreTracker scoreTracker;
    [SerializeField]private Transform ball;
    // Start is called before the first frame update
    void Start()
    {
        messageField = UI.Find("messageField").GetComponent<TextMeshProUGUI>();
        scoreUI = UI.Find("Score").GetComponent<TextMeshProUGUI>();
        scoreUI.color = new Color(255,0,0,0);
        TextMeshProUGUI[] scoreUIs = { scoreUI, GameObject.Find("bgScore").GetComponent<TextMeshProUGUI>() };
        scoreTracker = new scoreTracker(pointsToWin, scoreUIs);

        StartCoroutine(resetGame());
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(ball.transform.position.y) > CameraHandler.playArea.height/2)
            Score(ball.transform.position.y);
    }

    private void Score(float ballHeight){
        player scoringPlayer = ballHeight > 0 ? player.you : player.opponent;

        if(scoreTracker.AddPoint((int)scoringPlayer))
            EndGame(scoringPlayer);
        else
            StartCoroutine(resetGame());

    }

    public IEnumerator resetGame(){
        ball.position = new Vector2(0, 0);
        BallMovement ballM = ball.GetComponent<BallMovement>();
        ballM.active = false;
        int timer = 3;
        for (int i = timer; i > 0; i--)
        {
            messageField.text = $"{i}";
            yield return new WaitForSeconds(1);
        }
        messageField.text = "";
        ballM.active = true;
        ballM.Reset();
    }

    private void EndGame(player winningPlayer){
        ball.gameObject.SetActive(false);

        if(winningPlayer == player.you)
            messageField.text = "You win!";
        else
            messageField.text = "you lose";
    }
}
