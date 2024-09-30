using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public GameObject contBtn , exitBtn, restartBtn , howToPlay;

    public GameObject defIcon , winIcon;
    public TextMeshProUGUI HP , Score , En;
    Rigidbody2D rb;

    short health=5;
     int score=0;

     int enemyKilled=0;

    public float speed;
    public Transform DetectGround;
    public LayerMask maskground;
    bool isGrounded;
    public float jumpForce;
    SpriteRenderer sp;

    bool isAttacking=false;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp=GetComponent<SpriteRenderer>();
        animator=GetComponent<Animator>();
        HP.text=5.ToString();
        Score.text=0.ToString();
        En.text=0.ToString();

        contBtn.SetActive(false);
        exitBtn.SetActive(false);
        restartBtn.SetActive(false);

        defIcon.SetActive(false);
        winIcon.SetActive(false);

        howToPlay.SetActive(true);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    
        Mouvement();
        Dying();
    }

    void Update(){
        HUD();
        Stat();
    }

    public void PlayFootstep(){
      //Jouez le son de FootStep ici;
    }
    
    void Mouvement(){
            //input.GetButton
            float DirX = Input.GetAxis("Horizontal");
            
            //Mouvement gauche droite
            if(DirX >0 ){
                sp.flipX=false;
            }else if(DirX < 0){
            sp.flipX=true;
            }
            
            if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && !Input.GetButton("Jump")){
                PlayFootstep();
                animator.SetBool("Runing",true);
                rb.velocity = new Vector2( Mathf.Ceil(DirX) * (speed+5)  , rb.velocity.y);
        
            }else{
                animator.SetBool("Runing",false);
                rb.velocity = new Vector2(Mathf.Ceil(DirX) * speed , rb.velocity.y);
        
            }
    
        

            //Mouvement de saut
            isGrounded = Physics2D.OverlapCircle(DetectGround.position , 1f  , maskground);
            if (Input.GetButton("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }else{
                animator.SetBool("Jumping",false);
            }
            

            //Mouvement d'attaque
            if(Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return) || Input.GetButton("Fire1")){
                isAttacking=true;
                animator.SetBool("Attacking",true);
            }else{
                isAttacking=false;
                animator.SetBool("Attacking",false);
            }      

            //Script pour l'animation
            if(Input.GetButton("Jump") && isGrounded){
                animator.SetBool("Jumping",true);
            }else if(DirX == 0 && !Input.GetButton("Jump")){
                animator.SetBool("Walking",false);
            }else if(DirX!=0 && !Input.GetButton("Jump")){
                animator.SetBool("Walking",true);
            }
    
            
    }

      void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Collectable"))
            {
                Destroy(col.gameObject);
                score++;
            }
            if(col.gameObject.CompareTag("Flag")){
                winIcon.SetActive(true);
                restartBtn.SetActive(true);
                exitBtn.SetActive(true);
                Time.timeScale=0f;
                Debug.Log("Arrivé");
            }

         
        }
    
      void OnCollisionEnter2D(Collision2D col){
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
             if(col.gameObject.CompareTag("Enemy") && isAttacking){
                if(enemy !=null){
                    enemy.TakeDamage();
                }
                enemyKilled++;
            }else if(col.gameObject.CompareTag("Enemy")) {
                health--;
                animator.SetBool("Hurting",true);
                Debug.Log("HP:"+health);
                StartCoroutine(Resister());
            }
            
      }
     
    IEnumerator Resister(){
        yield return new WaitForSeconds(1f);
         animator.SetBool("Hurting",false);
    }

    void HUD(){
        HP.text=(health>=0) ? health.ToString() :  "DEAD";
        Score.text=score.ToString();
        En.text=enemyKilled.ToString();
    }
    
    void Stat(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            Time.timeScale=0f;
            contBtn.SetActive(true);
            exitBtn.SetActive(true);
        }
    }

    public void Continue(){
        Time.timeScale=1f;
        contBtn.SetActive(false);
        exitBtn.SetActive(false);
        restartBtn.SetActive(false);
    }

    public void Exit(){
        SceneManager.LoadScene("Home");
    }

    public void Restart(){
          SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Dying(){
        if(health<0){
           animator.SetBool("Dying",true);
           defIcon.SetActive(true);
           restartBtn.SetActive(true);
           exitBtn.SetActive(true);
        }
    }

    public void OK(){
        howToPlay.SetActive(false);
    }
}
