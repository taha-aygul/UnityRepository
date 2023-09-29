using UnityEngine;
using DG.Tweening;

public class Gun : MonoBehaviour
{

    public Animator animator;
    public ParticleSystem muzzle;
    public GameObject bulletStartPos;
    public GameObject bulletPrefab;
    public Vector3 move;
   /* public float shakeDuration, strenght = 1;
    public int vibrato = 10;
    public float randomness = 90;
    public bool snapping = false, fadeOut = true;
    public ShakeRandomnessMode shakeRandomnessMode = ShakeRandomnessMode.Full;*/
    private bool isEnable;
    private Vector3 _gunStartPos, _gunStartRot;


    private void Start()
    {
        GetGunPosAndRot();
    }
    private void GetGunPosAndRot()
    {
        _gunStartPos = transform.localPosition;
        _gunStartRot = transform.localEulerAngles;
    }
    public void FireBullet()
    {
        /* transform.DOShakePosition(shakeDuration,strenght, vibrato, randomness, snapping, fadeOut,shakeRandomnessMode);
         transform.DOLocalMove(_gunStartPos,0.1f);*/
        //transform.DOLocalJump(transform.localPosition - new Vector3(0,0,2),1,1,1);

        if (isEnable)
        {
            muzzle.Play();
            animator.Play(0, 0, 0);
            GameObject bulletGO = Instantiate(bulletPrefab, bulletStartPos.transform);
            bulletGO.transform.localEulerAngles = bulletPrefab.transform.eulerAngles;
            bulletGO.transform.localPosition = Vector3.zero;
            bulletGO.transform.SetParent(null);
            Bullet bulletSc = bulletGO.GetComponent<Bullet>();
            bulletSc.Fired(bulletStartPos);
        }

    }



    public void StartShoot()
    {
        isEnable = false;
    }

    public void StopShoot()
    {
        isEnable = true;

        animator.transform.DOLocalMove(_gunStartPos, 0.1f).SetEase(Ease.Linear);
        animator.transform.DOLocalRotate(_gunStartRot, 0.1f).SetEase(Ease.Linear);
    }







    /*
     public Animator animator;
     public float gunMoveTimeToStartPos;
     public ParticleSystem fireParticleSystem;
     private Vector3 _gunStartPos, _gunStartRot;
     private void Start()
     {
         GetGunPosAndRot();
     }
     private void GetGunPosAndRot()
     {
         _gunStartPos = animator.transform.localPosition;
         _gunStartRot = animator.transform.localEulerAngles;
     }
     public void FireBullet()
     {
         GameObject bulletGO = Instantiate(bulletPrefab, bulletStartPos.transform);
         bulletGO.transform.localEulerAngles = bulletPrefab.transform.eulerAngles;

         bulletGO.transform.SetParent(null);
         Bullet bulletSc = bulletGO.GetComponent<Bullet>();

         bulletSc.Fired(bulletStartPos);

         fireParticleSystem.Play();
     }
     public void StartFire()
     {
         if (!animator.enabled)
         {
             animator.Play(0, 0, 0);
             animator.enabled = true;
         }
     }
     public void StopFire()
     {
         if (animator.enabled)
         {
             animator.enabled = false;
            /* animator.transform.DOLocalMove(_gunStartPos, gunMoveTimeToStartPos).SetEase(Ease.Linear);
             animator.transform.DOLocalRotate(_gunStartRot, gunMoveTimeToStartPos).SetEase(Ease.Linear);
         }
     }*/
}