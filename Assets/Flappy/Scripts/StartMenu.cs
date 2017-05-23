using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Button btnEasy;
    public Button btnNormal;
    private int _selectedIndex = 0;
    private List<Button> _btnList;
    // Use this for initialization
	void Start ()
    {
        _btnList = new List<Button>();
        _btnList.Add(btnEasy);
        _btnList.Add(btnNormal);
        //Debug.Log("Count: " + _btnList.Count);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!GameControl.instance.gameOver)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ScoreManager.instance.Modify = 1;
                SoundManager.instance.PlaySelectSound();
                if (_selectedIndex != 0)
                {
                    _selectedIndex--;
                }
                else
                {
                    _selectedIndex = _btnList.Count - 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ScoreManager.instance.Modify = 2;
                SoundManager.instance.PlaySelectSound();
                _selectedIndex++;
                if (_selectedIndex > _btnList.Count - 1)
                {
                    _selectedIndex = 0;
                }
            }
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                SendStarCommand(_selectedIndex);
            }
            GameControl.instance.UpdateLegends();
            DrawButtons(_selectedIndex);
        }
    }

    void DrawButtons (int selectIdx)
    {
        //Debug.Log(selectIdx);
        _btnList[selectIdx].GetComponent<Image>().color = Color.cyan;
        for (int i = 0; i <= _btnList.Count - 1; i++)
        {
            if (i != selectIdx)
            {
                //Debug.Log("White for: " + i);
                _btnList[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

    void SendStarCommand(int index)
    {
        if (index == 0)
        {
            GameControl.instance.SetDifficult(Difficult.Easy);
            GameControl.instance.StartGame();
        }
        else
        {
            GameControl.instance.SetDifficult(Difficult.Normal);
            GameControl.instance.StartGame();
        }
    }
}
