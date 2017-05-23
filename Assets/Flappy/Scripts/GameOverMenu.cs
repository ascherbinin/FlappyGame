using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum PlaceColors 
{
	Gold = 1,
	Silver,
	Bronze
}

public class GameOverMenu : MonoBehaviour
{
    public GameObject ContentPanel;
   // public GameObject Grid;
    public GameObject ListItemPrefab;
    private bool isListShowed = false;
   
    public ScrollRect scrollRect;

	List<GameObject> ItemsList = new List<GameObject>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        if (GameControl.instance.gameOver)
        {
            if(!isListShowed)
            {
                PopulateList();
                isListShowed = true;
            }

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.JoystickButton0))
            {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
            }

			if (Input.GetKeyDown (KeyCode.Alpha5) || Input.GetKeyDown (KeyCode.JoystickButton5)) 
			{
				RepopulateList ();
			}

            if (Input.GetKey(KeyCode.DownArrow))
            {
                scrollRect.verticalNormalizedPosition -= 0.01f;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                scrollRect.verticalNormalizedPosition += 0.01f;
            }
        }
    }

    void PopulateList ()
    {
        var scoreList = ScoreManager.instance.GetScoresList();
		int place = 1;
        foreach (Score score in scoreList)
        {
            if (place > 6) return;
            GameObject newScore = Instantiate(ListItemPrefab) as GameObject;
			newScore.GetComponent<Image> ().color = ColoredListItem (place); 
            ListItemController controller = newScore.GetComponent<ListItemController>();
            controller.Value.text = score.Value.ToString();
            controller.Date.text = score.Date.ToString();
            newScore.transform.SetParent(ContentPanel.transform);
            newScore.transform.localScale = Vector3.one;
			ItemsList.Add (newScore);
			place++;
        }
    }

	void RepopulateList()
	{
        ScoreManager.instance.ClearScoreList();
		foreach (var item in ItemsList) {
			Destroy (item);
		}
		PopulateList ();
	}

	Color ColoredListItem(int idx)
	{
		switch (idx) 
		{
		case 1:
			return new Color32 (254, 215, 0, 255);
		case 2:
			return new Color32 (192, 192, 192, 255);
		case 3:
			return new Color32 (205, 127, 0, 255);
		default:
			return new Color32 (255, 255, 255, 255);
		}
	}
}
