using TMPro;

public class scoreTracker
{
    private int[] scores = {0,0};
    private int pointsToWin;

    TextMeshProUGUI[] scoreTexts;

    public int P1Score {get { return scores[0]; } }
    public int P2Score {get { return scores[1]; } }

    public scoreTracker(int pointsToWin, TextMeshProUGUI[] scoreTXTs){
        this.pointsToWin = pointsToWin;
        this.scoreTexts = scoreTXTs;
        reset();
    }

    /// <summary>
    /// adds a point to a players score
    /// </summary>
    /// <param name="player">the player id, 1 for bottom, 2 for top</param>
    /// <returns>if the player has won</returns>
    public bool AddPoint(int player){
        player--;

        scores[player]++;
        UpdateUIs();

        return scores[player] >= pointsToWin;
    }

    private void UpdateUIs(){
        foreach (TextMeshProUGUI txt in scoreTexts)
            txt.text = $"{scores[0]} - {scores[1]}";
    }

    public void reset(){
        scores = new int[] {0,0};
        UpdateUIs();
    }

}
