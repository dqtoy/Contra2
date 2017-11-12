﻿using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreGameHandler : MonoBehaviour {

    public GameObject shopPanel, topBarPanel, loadingPanel;
    private DatabaseReference reference;
    public Button lifeBtn, machineBtn, lazerBtn, flameBtn, spreadBtn, playBtn;
    public Text coinText, livesText;
    int coin, live;

    void Awake()
    {
        
        if (PlayerPrefs.GetInt(RefDefinition.OFFLINE_MODE) == 1)
        {
            //offline mode
            topBarPanel.SetActive(false);
            shopPanel.SetActive(false);
            if (GameManager.instance.immortal)
            {
                //immortal mode
                live = RefDefinition.IMMORTAL_LIVE_VALUE;
                livesText.text = "IMMORTAL MODE";
            } else
            {
                //default mode
                live = GameManager.instance.lives;
                livesText.text = "LIVE REMAIN: " + live.ToString();
            }
        } else
        {
            //online mode
            disableAllButton();
            //online mode
            //init coin

            //read live, guntype
            live = GameManager.instance.lives;
            //gunType = PlayerPrefs.GetString("GunType");

            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://super-contra-20171.firebaseio.com/");

            // Get the root reference location of the database.
            reference = FirebaseDatabase.DefaultInstance.RootReference.Child("User").Child(PlayerPrefs.GetString("uid"));

            reference.Child("Coin").GetValueAsync().ContinueWith(task => {

                if (task.IsFaulted)
                {
                    Debug.Log("coin get failed");
                    coinText.text = "0";

                }
                else if (task.IsCompleted)
                {
                    Debug.Log("coin get complete");

                    DataSnapshot snapshot = task.Result;
                    Debug.Log("coin = " + snapshot.Value.ToString());
                    //int coin = (int) snapshot.Value;
                    coin = System.Int32.Parse(snapshot.Value.ToString());
                    coinText.text = snapshot.Value.ToString();
                    enableAllButton();
                }
            });

        }
    }

    private void disableAllButton()
    {
        lifeBtn.enabled = false;
        machineBtn.enabled = false;
        lazerBtn.enabled = false;
        flameBtn.enabled = false;
        spreadBtn.enabled = false;
        playBtn.enabled = false;
    }

    private void enableAllButton()
    {
        lifeBtn.enabled = true;
        machineBtn.enabled = true;
        lazerBtn.enabled = true;
        flameBtn.enabled = true;
        spreadBtn.enabled = true;
        playBtn.enabled = true;
    }

    private void disableGunButton ()
    {
        machineBtn.enabled = false;
        lazerBtn.enabled = false;
        flameBtn.enabled = false;
        spreadBtn.enabled = false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void buyLife()
    {
        //get coin
        if (coin >= ItemPrice.LIFE)
        {
            int newCoin = coin - ItemPrice.FLAME_BULLET;
            //can buy
            showLoading();
            disableAllButton();
            reference.Child("Coin").SetValueAsync(newCoin).ContinueWith(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    //khong lam duoc
                    enableAllButton();
                    Debug.Log("khong mua dc");
                }
                else if (task.IsCompleted)
                {
                    Debug.Log("mua thanh cong");
                    enableAllButton();
                    live++;
                    coin = newCoin;
                    coinText.text = coin.ToString();
                }
                disableLoading();
            });


        } else
        {
            Debug.Log("not enough coin");
        }
    }

    public void buyMachineBullet()
    {
        if (coin >= ItemPrice.MACHINE_BULLET)
        {
            int newCoin = coin - ItemPrice.MACHINE_BULLET;
            //can buy
            showLoading();
            disableAllButton();
            reference.Child("Coin").SetValueAsync(newCoin).ContinueWith(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    //khong lam duoc
                    enableAllButton();
                    Debug.Log("khong mua dc");
                }
                else if (task.IsCompleted)
                {
                    Debug.Log("mua thanh cong");
                    enableAllButton();
                    disableGunButton();
                    //gunType = RefDefinition.MACHINE_BULLET;
                    coin = newCoin;
                    coinText.text = coin.ToString();
                }
                disableLoading();
            });
        }
        else
        {
            Debug.Log("not enough coin");
        }
    }

    public void buyLazerBullet()
    {
        if (coin >= ItemPrice.LAZER_BULLET)
        {
            int newCoin = coin - ItemPrice.LAZER_BULLET;
            //can buy
            showLoading();
            disableAllButton();
            reference.Child("Coin").SetValueAsync(newCoin).ContinueWith(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    //khong lam duoc
                    enableAllButton();
                    Debug.Log("khong mua dc");
                }
                else if (task.IsCompleted)
                {
                    Debug.Log("mua thanh cong");
                    enableAllButton();
                    disableGunButton();
                    //gunType = RefDefinition.LAZER_BULLET;
                    coin = newCoin;
                    coinText.text = coin.ToString();
                }
                disableLoading();
            });
        }
        else
        {
            Debug.Log("not enough coin");
        }
    }

    public void buyFlameBullet()
    {
        if (coin >= ItemPrice.FLAME_BULLET)
        {

            int newCoin = coin - ItemPrice.FLAME_BULLET;
            //can buy
            showLoading();
            disableAllButton();
            reference.Child("Coin").SetValueAsync(newCoin).ContinueWith(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    //khong lam duoc
                    enableAllButton();
                    Debug.Log("khong mua dc");
                }
                else if (task.IsCompleted)
                {
                    Debug.Log("mua thanh cong");
                    enableAllButton();
                    disableGunButton();
                    //gunType = RefDefinition.FLAME_BULLET;
                    coin = newCoin;
                    coinText.text = coin.ToString();
                }
                disableLoading();
            });
        }
        else
        {
            Debug.Log("not enough coin");
        }
    }

    public void buySpreadBullet()
    {
        if (coin >= ItemPrice.SPREAD_BULLET)
        {
            int newCoin = coin - ItemPrice.SPREAD_BULLET;
            //can buy
            showLoading();
            disableAllButton();
            reference.Child("Coin").SetValueAsync(newCoin).ContinueWith(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    //khong lam duoc
                    enableAllButton();
                    Debug.Log("khong mua dc");
                }
                else if (task.IsCompleted)
                {
                    Debug.Log("mua thanh cong");
                    enableAllButton();
                    disableGunButton();
                    //gunType = RefDefinition.SPREAD_BULLET;
                    coin = newCoin;
                    coinText.text = coin.ToString();
                }
                disableLoading();
            });
        }
        else
        {
            Debug.Log("not enough coin");
        }
    }

    public void play()
    {
        GameManager.instance.lives = live;
        //preGameData.bulletType = gunType;

        //play scene
    }

    public void showLoading()
    {
        loadingPanel.SetActive(true);
    }

    public void disableLoading()
    {
        loadingPanel.SetActive(false);
    }
}
