using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager_scr_K : MonoBehaviour
{
    private GameObject gamemanager;
    GameManager_scr_K Game_M;

    public GameObject AjiMuzzle;
    Bullet_scr_K bullet;

    public int state;
    public bool isAlive = true;

    public GameObject player;
    private Vector3 Epos, firstEpos, Ppos;
    private float dis, AbsDis, AjiSpeed, ManbouSpeed, enemySpeed, IwashiActiveDis, AjiActiveDis, ManbouActiveDis, enemyDespawnDis, manbouAttackDis, ajiAttackDis ,AjiAttackTime;
    float cosx, angle, r = 0.4f;
    float Spawn_delta = 10f;
    private Vector3 Edelta;
    private float AjiTimer;


    void Start()
    {
        //ゲームマネージャー取得
        gamemanager = GameObject.Find("GameManager");
        Game_M = gamemanager.GetComponent<GameManager_scr_K>();

        bullet = AjiMuzzle.GetComponent<Bullet_scr_K>();

        Edelta = Game_M.Edelta;

        AjiAttackTime = Game_M.AjiAttackTime;

        //動き出す距離,デスポーン距離
        IwashiActiveDis = Game_M.IwashiActiveDis;
        AjiActiveDis = Game_M.AjiActiveDis;
        ManbouActiveDis = Game_M.ManbouActiveDis;

        enemyDespawnDis = Game_M.enemyDespawnDis * -1f;

        manbouAttackDis = Game_M.manbouAttackDis;
        ajiAttackDis = Game_M.ajiAttackDis;

        //スピード
        enemySpeed = Game_M.enemyDefaultSpeed * 0.01f;
        AjiSpeed = enemySpeed * 10;
        ManbouSpeed = enemySpeed * 15;

        //プレイヤー取得
        //player = Game_M.player;

        //初期座標を取得                   
        Epos = this.transform.position;
        firstEpos = Epos;
        Epos += Edelta;
        this.transform.position = Epos;
        AjiTimer = AjiAttackTime * 60 - 30;
    }


    void Update()

    {
        //プレイヤーとの距離を取得する。　disは　正：近ずく　負：遠ざかる
        Ppos = player.transform.position;
        dis = Epos.x - Ppos.x;
        AbsDis = Mathf.Abs(dis);

        //離れるとデスポーン
        if (dis < enemyDespawnDis && isAlive)
        {
            isAlive = false;
            Destroy(this.gameObject);

        }
        if (PlayerMove_scr_K.isDead == false)
        {

            Move();

            //Eposの値をいじって ここで動かす
            this.transform.position = Epos;
        }
    }




    public void Move()
    {
        if (state == 0)
        {
            return;
        }
        //プレイヤーとの距離が縮まれば動き出す
        else if (state == 1 && dis <= IwashiActiveDis && dis >= enemyDespawnDis && isAlive)//イワシ
        {
            Epos.z += Spawn_delta;
            //.SetActive(true);
            if (Epos.z - firstEpos.z >= 0)
            {//元の位置に戻ったら
                Spawn_delta = 0;
                Iwashi();
            }


        }

        else if (state == 2 && dis <= AjiActiveDis && dis >= enemyDespawnDis && isAlive)//アジ
        {
            Epos.z += Spawn_delta;
            //.SetActive(true);
            if(Epos.z - firstEpos.z >= 0){//元の位置に戻ったら
                Spawn_delta = 0;
                Aji();
            }
        }


        else if (state == 3 && dis <= ManbouActiveDis && dis >= enemyDespawnDis && isAlive)
        {//マンボウ
            Epos.z += Spawn_delta;
            //.SetActive(true);
            if (Epos.z - firstEpos.z >= 0)
            {//元の位置に戻ったら
                Spawn_delta = 0;
                Manbou();
            }

        }
    }


    public void Iwashi()
    {

        Epos.x += -enemySpeed;
    }


    public void Aji()
    {

        //ajiAttackDisまで前進
        if (AbsDis > ajiAttackDis && angle != 180)
        {
            Epos.x += -AjiSpeed;
        }

        //近ずくと止まって小レーザー弾
        else if (AbsDis <= ajiAttackDis)
        {
            angle += 18;
            if (angle < 180)
            {
                cosx = Mathf.Cos(angle * (Mathf.PI / 180));
                Epos.x -= r * 2 * cosx;
                Epos.y += r;
            }

            else
            {
                angle = 180f;
                AjiAttack();
            }
        }

        else if (AbsDis > ajiAttackDis && angle == 180)
        {
            Epos.x += -AjiSpeed * 0.1f;
        }
    }


    public void Manbou()
    {

        //AttackDisまで前進
        if (AbsDis > manbouAttackDis && angle != 180)
        {
            Epos.x += -ManbouSpeed;
        }

        //近ずくと止まってレーザー発射
        else if (AbsDis <= manbouAttackDis)
        {
            angle += 18;
            if (angle < 180)
            {
                cosx = Mathf.Cos(angle * (Mathf.PI / 180));
                Epos.x -= r * 2 * cosx;
                Epos.y += r;
            }

            else
            {
                angle = 180f;
                ManbouAttack();
            }
        }

        else if (AbsDis > manbouAttackDis && angle == 180)
        {
            Epos.x += -ManbouSpeed * 0.1f;
        }

    }

    public void ManbouAttack()
    {
        Debug.Log("マンボウの攻撃！！！");
    }
    public void AjiAttack()
    {
        //Debug.Log("アジの攻撃！！！");
        AjiTimer++;
        if (AjiTimer >= AjiAttackTime * 60 )
        {
            bullet.Shot_3();

            AjiTimer = 0;
        }

    }
}
