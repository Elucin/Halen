using UnityEngine;
using System.Collections;

public class HSController : MonoBehaviour
{
    private string secretKey = "589811"; // Edit this value and make sure it's the same as the one stored on the server
    //public string addScoreURL = "ug.csit.carleton.ca/~andrewsmith4/Halen/addscore.php?"; //be sure to add a ? to your url
    //public string highscoreURL = "ug.csit.carleton.ca/~andrewsmith4/Halen/display.php";
    public string addScoreURL = "https://www.skypyre.com/scripts/HalenScore/addscore.php?"; //be sure to add a ? to your url
    public string highscoreURL = "https://www.skypyre.com/scripts/HalenScore/display.php?";
    public int position = 0;
    [SerializeField]
    UnityEngine.UI.Text[] Names;

    [SerializeField]
    UnityEngine.UI.Text[] Scores;

    [SerializeField]
    bool getHighScores = false;

    void Start()
    {
        StartCoroutine(GetScores());
    }

    // remember to use StartCoroutine when calling this function!
    public IEnumerator PostScores(int score, string name = "Halen")
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string hash = MD5Test.Md5Sum(name + score + secretKey);

        string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;

        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            print("There was an error posting the high score: " + hs_post.error);
        }
    }

    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    IEnumerator GetScores()
    {
            WWW hs_get = new WWW(highscoreURL);
            yield return hs_get;

            if (hs_get.error != null)
            {
                print("There was an error getting the high score: " + hs_get.error);
            }
            else
            {
                bool _isName = true;
                hs_get.text.TrimEnd();
                string[] nameScore = hs_get.text.Split('\t', '\n');
                System.Collections.Generic.List<string> name = new System.Collections.Generic.List<string>();
                System.Collections.Generic.List<string> score = new System.Collections.Generic.List<string>();
                foreach (string n in nameScore)
                {
                    if(_isName && n != "")
                    {
                       string nUpper = n.ToUpper();
                        name.Add(nUpper);
                    }
                    else
                    {
                        score.Add(n);
                    }
                    _isName = !_isName;
                }
            if (getHighScores)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (Names[i] != null)
                    {
                        Names[i].text = name[i];
                        Scores[i].text = score[i];
                    }
                    else
                        break;
                }
            }
            else
            {
                int _i = 1;
                foreach(string s in score)
                {
                    int hScore = 0;
                    if (s != "")
                    {
                        hScore = int.Parse(s);
                    }
                    if(Scoring.PlayerScore > hScore)
                    {
                        position = _i;
                        break;
                    }
                    position = _i;
                    _i++;
                }
            }
        }
    }

}