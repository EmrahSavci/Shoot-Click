using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarret : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform bulletSpawnPos;
    [SerializeField] ParticleSystem SmokeEffect;
    [SerializeField] GameObject Barrel;
    [SerializeField] List<GameObject> BulletList = new List<GameObject>();
    [SerializeField] int BulletPoolIndex = 0;
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            ObjectPoolingBullet();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }
    void Shoot()
    {
        LeanTween.moveLocalZ(Barrel, -0.5f, 0.05f).setOnComplete(() =>
        {
            GameObject _bullet = BulletList[BulletPoolIndex];
            _bullet.GetComponent<TrailRenderer>().enabled = false;
            _bullet.SetActive(false);
            _bullet.transform.position = bulletSpawnPos.position;
            _bullet.SetActive(true);
            _bullet.GetComponent<TrailRenderer>().enabled = true;
            _bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 15, ForceMode.Impulse);
            SmokeEffect.Play();
            LeanTween.moveLocalZ(Barrel, 0f, 0.05f);
            BulletPoolIndex++;
            if (BulletPoolIndex >= 10)
            {
                BulletPoolIndex = 0;

            }
                
        });
        
    }
    void ObjectPoolingBullet()
    {
        GameObject _bullet = Instantiate(Bullet, bulletSpawnPos.position, Quaternion.identity);
        BulletList.Add(_bullet);
        _bullet.SetActive(false);
    }
}
