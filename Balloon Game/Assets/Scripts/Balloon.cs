using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{

    public GameObject explosion, redWarning, greenPop, bluePop;
    private bool isRed;



    private void OnMouseDown()
    {
        if (!UIManager.Instance.isRed)
        {
            PopBaloon();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
    public void DestroyWithExplosion(GameObject effect)
    {

        GameObject explosionGO = Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(explosionGO, 0.85f);
        Destroy(gameObject);
    }



    public void PopBaloon()
    {



        if (gameObject.tag == "Explosive")
        {
            ScoreManager.Instance.IncreaseScore(BalloonManager.Instance.explosionMinusScore);
            // TimerManager.Instance.DestroyBalloons();
            DestroyWithExplosion(explosion);

        }
        else if (gameObject.CompareTag("RedBalloon"))
        {
            UIManager.Instance.isRed = true;
            UIManager.Instance.MakeRedScreenActive();
            //Invoke(nameof(UIManager.Instance.MakeRedScreenInactive), 2f);
            DestroyWithExplosion(redWarning);
        }
        else if (gameObject.CompareTag("GreenBalloon"))
        {
            ScoreManager.Instance.IncreaseScore(BalloonManager.Instance.greenScore);
            DestroyWithExplosion(greenPop);
            //UIManager.Instance.MakePlayingPanelTransparent();


        }
        else if (gameObject.CompareTag("BlueBalloon"))
        {
            ScoreManager.Instance.IncreaseScore(BalloonManager.Instance.blueScore);
            DestroyWithExplosion(bluePop);
            // UIManager.Instance.MakePlayingPanelTransparent();


        }
        





    }

}


