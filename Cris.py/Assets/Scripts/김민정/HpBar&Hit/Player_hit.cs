using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_hit : MonoBehaviour
{

    Rigidbody2D rigid;
    SpriteRenderer SpriteRenderer;

    bool isDie = false;
    bool isJumping = false;
    private Text hpText;

    public int hp = 100;
    
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        hpText = GameObject.Find("HpText").GetComponent<Text>();

    }

    
    void Update()
    {
        if (hp == 0) 
        {
            isDie = true;
            SceneManager.LoadScene("MainMenu");
        }
    }
    void FixedUpdate()
    {
        hpText.text = hp.ToString();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            OnDamaged(collision.transform.position);
        }
    }
    
    void OnDamaged(Vector2 Targetpos) 
    {
        gameObject.layer = gameObject.layer+1;
        SpriteRenderer.color = new Color(1, 1, 1, 0.4f);
        hp -= 10;
        int dirc = transform.position.x - Targetpos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 2, ForceMode2D.Impulse);
        Invoke("OffDamaged", 1);
    }

    void OffDamaged()
    {
        gameObject.layer = gameObject.layer-1;
        SpriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
