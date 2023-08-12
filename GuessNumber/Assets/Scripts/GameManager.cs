using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI numberText, infoText, pointText;
    public TMP_InputField guess;
    private int _number, _guess;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        Create();
    }


     // Ctrl+K   Ctrl+D

    /// <summary>
    ///  Creates a random number between 1-100 
    /// </summary>
    public void Create()
    {
        score = 100;
        pointText.text = "Points   " + score;
        infoText.text = "";
        _number = Random.Range(1, 100);

        string TenAs = new string('*', _number.ToString().Length); // 9999 
        numberText.text =TenAs;
       
    }


    /// <summary>
    /// Checks answer
    /// </summary>
    public void CheckInput()
    {

        _guess = int.Parse(guess.text);

        if (_guess == _number)
        {
            infoText.text = "!!Congrats!!";
            numberText.text = _number.ToString();
        }
        else if (_guess > _number)
        {


            score -= 5;
            if (_guess - _number >= 50)
            {
                infoText.text = "You're flying";

            }
            else if (_guess - _number >= 25)
            {
                infoText.text = "You're bit high!";

            }
            else
            {
                infoText.text = "Too close get low!";

            }
        }
        else if (_guess < _number)
        {
            score -= 5;

            if ( _number - _guess >= 50)
            {
                infoText.text = "You're crawling";

            }
            else if (_number - _guess >= 25)
            {
                infoText.text = "You're bit low!";

            }
            else
            {
                infoText.text = "Too close get high!";

            }
        }
        pointText.text = "Points " + score;
    }


    public void Exit()
    {
        Application.Quit();
    }
}