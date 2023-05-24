using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Enemy
{
    //[SerializeField] GameObject bulletPrefab;

    [SerializeField] float shootRate;
    [SerializeField] Transform shootPoint;

    bool isShooting;
    bool hasStartedShooting;

    [SerializeField] ObjectPool pool;

    Transform[] turretParts;

    public override void Start()
    {
        base.Start();
        turretParts = GetComponentsInChildren<Transform>();
        GameManager.Instance.AddListenerSceneIsLoaded(PlayerStartEnemy);
    }

    private void PlayerStartEnemy()
    {
        GameManager.Instance.Player.AddListenerFirstDashRespawn(StartEnemy);
    }


    //Shoot bullet
    IEnumerator Shooting()
    {
        Debug.LogError("Shooting");
        while (isShooting)
        {
            GameObject bullet = pool.GetObject()/*Instantiate(bulletPrefab, shootPoint.position, transform.rotation)*/;
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = transform.rotation;
            yield return new WaitForSeconds(1 / shootRate);
        }
    }

    //Kill enemy
    protected override void Die()
    {
        isShooting = false;
        foreach(Transform part in turretParts)
        {
            part.gameObject.SetActive(false);
        }
        base.Die();
    }

    //Initialize enemy
    protected override void StartEnemy()
    {
        base.StartEnemy();
        if (!hasStartedShooting)
        {
            isShooting = true;
            StartCoroutine(Shooting());
        }

        hasStartedShooting = true;

    }

    public override void ResetInteractable()
    {
        base.ResetInteractable();
        foreach (Transform part in turretParts)
        {
            part.gameObject.SetActive(true);
        }

        isShooting = false;
        hasStartedShooting = false;
    }
}
