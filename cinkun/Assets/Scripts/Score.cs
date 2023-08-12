using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{

    public Text Gem;
    [SerializeField] Text Cherry;
    private int gem_count = 0;
    private int cherry_count = 0;

    // Update is called once per frame
    void Update()
    {
        Gem.text = "  " + gem_count;
        Cherry.text = "  " + cherry_count;
    }

    public void increaseGem()
    {
        this.gem_count++;
    }
    public void increaseCherry()
    {
        this.cherry_count++;
    }



}
