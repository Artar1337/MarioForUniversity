using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public bool isBig = false;
    public int score = 0;
    public static int lives = 3;
    public float time = 300;
    public bool timeCounting = false;
    public float scale = 4f;

    private TMPro.TMP_Text scoreText, timeText;

    private void Start()
    {
        Transform t = GameObject.Find("Main Canvas").transform;
        scoreText = t.Find("Score").GetComponent<TMPro.TMP_Text>();
        timeText = t.Find("Time").GetComponent<TMPro.TMP_Text>();
    }

    public void SetBig()
    {
        if (isBig)
        {
            isBig = false;
            scale = 4f;
            return;
        }
        isBig = true;
        scale = 6f;
    }

    public void SetUI()
    {
        scoreText.text = "SCORE: " + score.ToString();
        timeText.text = "TIME: " + ((int)time).ToString();
    }

    private void Update()
    {
        if (!timeCounting)
            return;
        time -= Time.deltaTime;
        if (time < 0)
        {
            //убиваем игрока
            gameObject.GetComponent<Animator>().SetBool("Dead", true);
            timeCounting = false;
            time = 0;
        }

        SetUI();

    }
}
