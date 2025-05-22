using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHub : MonoBehaviour
{
    public UISlider enemyHealthBarSlider;
    public UISprite enemyHealthBarSprite;
    public UISprite enemyHealthSprite;
    public UISprite enemyEnergyBarSprite;
    public UISprite enemyEnergySprite;
    public UISprite enemyHeadBGSprite;
    public UISprite enemyHeadSprite;
    public EBunnyStatus enemystatus;
    public WidgetAttackController widgetAttackController;

    GameObject closestEnemy;
    GameObject player;
    float disToClosestEnemy;

    void Start()
    {
        enemyHealthBarSlider = GameObject.Find("EnemyHealthBar").GetComponent<UISlider>();
        enemyHealthBarSprite = GameObject.Find("EnemyHealthBar").GetComponent<UISprite>();
        enemyHealthSprite = GameObject.Find("EnemyHealth").GetComponent<UISprite>();
        enemyEnergyBarSprite = GameObject.Find("EnemyEnergyBar").GetComponent<UISprite>();
        enemyEnergySprite = GameObject.Find("EnemyEnergy").GetComponent<UISprite>();
        enemyHeadBGSprite = GameObject.Find("EnemyHeadBG").GetComponent<UISprite>();
        enemyHeadSprite = GameObject.Find("EnemyHead").GetComponent<UISprite>();

        player = GameObject.FindGameObjectWithTag("Player");
        widgetAttackController = player.GetComponent<WidgetAttackController>();

        HideEnemyHub();
    }

    void Update()
    {
        HideEnemyHub();
        closestEnemy = widgetAttackController.GetClosetEnemy();
        if (closestEnemy != null)
        {
            EBunnyStatus enemyStatus = closestEnemy.GetComponent<EBunnyStatus>();
            disToClosestEnemy = Vector3.Distance(closestEnemy.transform.position, player.transform.position);
            if (disToClosestEnemy < 20)
            {
                ShowEnemyHub();
                enemyHealthBarSlider.value = enemyStatus.health / enemyStatus.maxHealth;
            }
        }
        else
        {
            HideEnemyHub();
        }
    }

    void HideEnemyHub()
    {
        enemyHealthBarSprite.enabled = false;
        enemyHealthSprite.enabled = false;
        enemyEnergyBarSprite.enabled = false;
        enemyEnergySprite.enabled = false;
        enemyHeadBGSprite.enabled = false;
        enemyHeadSprite.enabled = false;
    }

    void ShowEnemyHub()
    {
        enemyHealthBarSprite.enabled = true;
        enemyHealthSprite.enabled = true;
        enemyEnergyBarSprite.enabled = true;
        enemyEnergySprite.enabled = true;
        enemyHeadBGSprite.enabled = true;
        enemyHeadSprite.enabled = true;
    }
}