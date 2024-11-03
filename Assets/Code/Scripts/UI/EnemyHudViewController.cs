using SibGameJam.HUD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SibGameJam.HUD
{
    public class EnemyHudViewController : MonoBehaviour
    {
        [SerializeField] EnemyHUD iconPrefab;
        [SerializeField] Transform cameraPos;
        [SerializeField] Camera playerCamera;
        [SerializeField] float minDrawDistance;

        [SerializeField] private List<EnemyHUD> enemyHUDs = new List<EnemyHUD>();

        public void AddToList(EnemyHUD enemyHud)
        {
            enemyHUDs.Add(enemyHud);
        }

        public void Update()
        {
            
            foreach (var hud in enemyHUDs)
            {
                if(Vector3.Distance(playerCamera.transform.position, hud.TankPostion()) < minDrawDistance)
                {
                    hud.Show();
                }
                else
                {
                    hud.Hide();
                }



            }
        }
        public IEnumerator HideAllIcons()
        {
            foreach (var hud in enemyHUDs)
            {
                hud.Hide();
                yield return null;
            }
        }
    }
}


