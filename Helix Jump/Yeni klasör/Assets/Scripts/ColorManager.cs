using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorManager : MonoBehaviour
{
    [SerializeField] GameObject ballObj;  // bunu aldým çünkü ball objesinde materiai (Instance) oluþuyor
    [SerializeField] Material ball, redPiece, normalPiece, cylinder, splash;
    [SerializeField] Camera mainCamera;
    public List<ColorList> colorThemes;
    public int index = 0;
    [SerializeField] float lerper;
    public int colorChageLevelFreq;

    public static ColorManager Instance;

    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {

        //index = DataRecorder.Instance.LoadTheme();
        //print(index+ " loaded");
        //LoadTheme(colorThemes[index].name, 1);
        // LoadTheme("Default Theme", 1);
    }
    private void Update()
    {
        if (ball.color != colorThemes[index].ball || normalPiece.color != colorThemes[index].normalPiece)
        {
            NextTheme();
        }
    }

   

    [ContextMenu(nameof(NextTheme))]
    public void NextTheme()
    {
        if (index > colorThemes.Count - 1)
        {
            index = 0;
        }
        ballObj.GetComponent<MeshRenderer>().material.color = Color.Lerp(ballObj.GetComponent<MeshRenderer>().material.color, colorThemes[index].ball, lerper);
        ball.color = Color.Lerp(ball.color, colorThemes[index].ball, lerper);
        splash.color = Color.Lerp(splash.color, colorThemes[index].ball, lerper);
        cylinder.color = Color.Lerp(cylinder.color, colorThemes[index].cylinder, lerper);
        normalPiece.color = Color.Lerp(normalPiece.color, colorThemes[index].normalPiece, lerper);
        redPiece.color = Color.Lerp(redPiece.color, colorThemes[index].redPiece, lerper);
        mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, colorThemes[index].backGround, lerper);
    }



    public void LoadTheme(string themeName)
    {
        foreach (var theme in colorThemes)
        {
            if (theme.name.Equals(themeName))
            {

                ballObj.GetComponent<MeshRenderer>().material.color = Color.Lerp(ballObj.GetComponent<MeshRenderer>().material.color, theme.ball, lerper);
                splash.color = Color.Lerp(splash.color, colorThemes[index].ball, lerper);
                ball.color = Color.Lerp(ball.color, theme.ball, lerper);
                cylinder.color = Color.Lerp(cylinder.color, theme.cylinder, lerper);
                normalPiece.color = Color.Lerp(normalPiece.color, theme.normalPiece, lerper);
                redPiece.color = Color.Lerp(redPiece.color, theme.redPiece, lerper);
                mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, theme.backGround, lerper);

            }
        }
    }
    public void LoadTheme(string themeName, float lerp)
    {
        foreach (var theme in colorThemes)
        {
            if (theme.name.Equals(themeName))
            {

                ballObj.GetComponent<MeshRenderer>().material.color = Color.Lerp(ballObj.GetComponent<MeshRenderer>().material.color, theme.ball, lerp);
                ball.color = Color.Lerp(ball.color, theme.ball, lerp);
                splash.color = Color.Lerp(splash.color, colorThemes[index].ball, lerp);
                cylinder.color = Color.Lerp(cylinder.color, theme.cylinder, lerp);
                normalPiece.color = Color.Lerp(normalPiece.color, theme.normalPiece, lerp);
                redPiece.color = Color.Lerp(redPiece.color, theme.redPiece, lerp);
                mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, theme.backGround, lerp);

            }
        }
    }
    public void LoadTheme(int themeIndex)
    {
        index = themeIndex;
        if (index > colorThemes.Count - 1)
        {
            index = 0;
        }
        ballObj.GetComponent<MeshRenderer>().material.color = Color.Lerp(ballObj.GetComponent<MeshRenderer>().material.color, colorThemes[index].ball, 0.5f);
        splash.color = Color.Lerp(splash.color, colorThemes[index].ball, lerper);
        ball.color = Color.Lerp(ball.color, colorThemes[index].ball, 0.5f);
        cylinder.color = Color.Lerp(cylinder.color, colorThemes[index].cylinder, 0.5f); ;
        normalPiece.color = Color.Lerp(normalPiece.color, colorThemes[index].normalPiece, 0.5f);
        redPiece.color = Color.Lerp(redPiece.color, colorThemes[index].redPiece, 0.5f);
        mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, colorThemes[index].backGround, 0.5f);
    }


    public int GetMaxIndex()
    {
        return colorThemes.Count - 1;
    }
    public void RandomIndex()
    {
        index = UnityEngine.Random.Range(0, colorThemes.Count);
    }
    public ColorList GetCurrentTheme()
    {
        return colorThemes[index];
    }
    public int GetThemeIndex() {
        print(index +" returnned"); return index; }
}

