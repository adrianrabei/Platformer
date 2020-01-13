using UnityEngine;

public class EnemyMovement: MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private RangeDetection range;//detectarea playerului
    [SerializeField] private Transform target;//playerul
    [SerializeField] private Animator animator;//animatorul
    public bool loop; //asta decide daca el o sa mearga conform algoritmului : Varianta 1: merge crescator 1,2,3,4 si cand ajunge la punctu 4 el se intoarce 4,3,2,1 . Varianta 2: 1,2,3,4 cand ajunge la 4 se duce inapoi la primu punct 
    public float startWaitTime; //cat timp o sa stea la un punct
    public float speed;
    public Transform[] moveSpots; //punctele prin care merge 
    private float waitTime;//timer
    private int spot;//punctul concret
    public float stoppingDistance;
    private bool isFacingRight;
    Vector3 lastPosition  = Vector3.zero;
    bool changeDirection = false;
    void Start()
    {
        waitTime = startWaitTime; //Set wait time
        spot = 0;//se alege primul punct la cere o sa se miste 
    }
    void Update()
    {
        Flip();
        if (range.ValueOfChecker() == true) //range e aria la enemy in care daca intra player si atunci enemy incepe sa il urmareasca . Lui Range i se atribuie o valoarea (se vede in scriptul range) daca colider la range se atinge cu colider la obiectul care are tag player
        {
            if (Vector2.Distance(transform.position, target.position) > stoppingDistance) //daca distanta dintre enemy si player e mai mare de distanta de stopare atunci se efectueaza urmarirea lui enemy pe player
            {
                animator.SetBool("run", true);
                rb.transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, target.position) <= stoppingDistance)//daca distanca dintre ei e potrivita atunci se apeleaza animatia de atac
            {
                animator.SetTrigger("attack");
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, moveSpots[spot].position) < 1f)//daca distanta lui enemy e mai mica de 1  , asta am facut ca sa nu alba mini laguri ca tipo fix sa ajunga la point
            {
                animator.SetBool("run", false);
                if (loop == false) //prima varianta cand ajunge la 4 si se intoarce la 3,2,1 
                {
                    if (waitTime <= 0)
                    {
                        if (changeDirection == false)
                        {
                            spot++;
                            if (spot == moveSpots.Length)
                            {
                                changeDirection = true;
                            }
                        }
                        if (changeDirection == true)
                        {
                            spot--;
                            if (spot == 0)
                            {
                                changeDirection = false;
                            }
                        }
                        waitTime = startWaitTime;
                    }
                    waitTime -= Time.deltaTime;
                }
                else //varianta 2 cand ajunge la 4 si se duce deodata la 1 
                {
                    if (waitTime <= 0)
                    {
                        if (changeDirection == false)
                        {
                            spot++;
                            if (spot == moveSpots.Length)
                            {
                                changeDirection = true;
                            }
                        }
                        if (changeDirection == true)
                        {
                            spot = 0;
                            if (spot == 0)
                            {
                                changeDirection = false;
                            }
                        }
                        waitTime = startWaitTime;
                    }
                }
            }
            else
            {
                rb.transform.position = Vector3.MoveTowards(transform.position, moveSpots[spot].position, speed * Time.deltaTime);
                animator.SetBool("run", true);
            }
        }
    }
    private void Flip()
    {
        //aici caroci ia pozitia curenta si scande din cea precedenta si vede daca e negativ inseamna ca a pers spre stinga , iar daca e pozitiv , atunci spre dreapta
        var direction = transform.position - lastPosition; 
        var localDirection = transform.InverseTransformDirection(direction);
        lastPosition = transform.position;
        if (direction.x < 0 && isFacingRight || direction.x > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
