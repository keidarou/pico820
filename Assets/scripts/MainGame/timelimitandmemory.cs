using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class timelimitandmemory : MonoBehaviour
{

    public int clearcount = 0;
    public Texture2D tex;
    public Font f;
    private GUIStyle labelStyle;
    public string scenename;
    private GUIStyle Highscore;
    public bool maingame;
    public bool gameoverflag = false;
    float timer = 0;
    public bool pauseorquitflag = false;
    movetheballautomatic script;
    public GameObject movetheballl;
    float zenkaiclear = 0;
    float saichouclear = 0;
    public string code;
    string nannido;
    public void gameovernisaseru()
    {
        gameoverflag = true;
    }
    public void zenkaivoid()
    {
        clearcount++;
    }
    //同じオブジェクトが何個も作られるのを防ぐ！！
    static timelimitandmemory _instance = null;
    static timelimitandmemory instance
    {
        get { return _instance ?? (_instance = FindObjectOfType<timelimitandmemory>()); }
    }


    public void goalupdate(int level)
    {
        if (timer - zenkaiclear > saichouclear)
        {
            saichouclear = timer - zenkaiclear;
            /*ここにスクリーンショットを保存するコードを書いて！！*/



            GameObject codemake = GameObject.Find("mapgenerator");
            codemaker script = codemake.GetComponent<codemaker>();
            int width = 0;
            if (level == 1) { width = 10; nannido = "easymode"; }
            if (level == 2) { width = 16; nannido = "normalmode"; }
            if (level == 3) { width = 24; nannido = "hardmode"; }

            code = script.codegenerate(width);
        }
        zenkaiclear = timer;
    }


    void Awake()
    {
        if (this != instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }

    void OnDestroy()
    {
        if (this == instance) _instance = null;

    }
    // Use this for initialization
    void Start()
    {

        this.labelStyle = new GUIStyle();
        this.labelStyle.fontSize = Screen.height / 20;
        this.labelStyle.normal.textColor = Color.white;
        Highscore = new GUIStyle();
        Highscore.fontSize = Screen.height / 15;
        Highscore.normal.textColor = Color.red;
        labelStyle.font = f; Highscore.font = f;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title") { Destroy(gameObject); }
        if (maingame) { return; }
        
        movetheballl = GameObject.Find("MoveTheBall");
        script = movetheballl.GetComponent<movetheballautomatic>();
        if (script.pauseflag == false)
        {
            if (timer > 100 || gameoverflag) { gameoverflag = true; }
            else
            {
                timer += Time.fixedDeltaTime;
            }
        }

    }
    void OnGUI()
    {
        if (!maingame)
        {
            string s = string.Format("{0}Seconds Left", 100 - (int)timer);
            string str = string.Format("Score : {0}", (int)clearcount);
            GUI.Label(new Rect(Screen.width - 300, Screen.height - 50, 100, 30), str, labelStyle);
            GUI.Label(new Rect(20, Screen.height - 50, 100, 30), s, labelStyle);
            if (gameoverflag == true)
            {
                int imamadenomax = PlayerPrefs.GetInt(scenename, 0);
                   Debug.Log(imamadenomax);
                Debug.Log(clearcount);
                if (imamadenomax < clearcount)//もしハイスコアなら
                {
                    GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 160, 200, 30), "Highscore!!", Highscore);//表示されない
                    string sscore = string.Format("Highscore : {0}    Your Score : {0}", (int)clearcount);
                    // Debug.Log(sscore);
                    GUI.Label(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 50, 100, 30), sscore, labelStyle);
                }
                else
                {
                    string sscore = string.Format("Highscore : {0}    Your Score : {1}", (int)imamadenomax, (int)clearcount);
                    GUI.Label(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 50, 100, 60), sscore, labelStyle);
                }
                script.gameoverflag = true;
                if (pauseorquitflag)
                {
                    SceneManager.LoadScene(scenename);
                    PlayerPrefs.SetInt(scenename, clearcount);
                    timer = 0; clearcount = 0;
                    pauseorquitflag = false;
                    gameoverflag = false;
                }
            }
        }
    }

    public string tweetTextGenerate()
    {
        string koubun = string.Format("I tooked {0}scores in {1}! Can you beat me?", clearcount, nannido);
        if (clearcount == 0) { return koubun; }
        string mapcode = string.Format("\nThis is the map code!  【{0}】 ", code);
        return koubun + mapcode;
    }
}
