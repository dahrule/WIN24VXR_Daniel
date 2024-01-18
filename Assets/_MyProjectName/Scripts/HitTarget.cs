using TMPro;
using UnityEngine;

public class HitTarget : MonoBehaviour
{
    [SerializeField] protected int baseScore = 1;

    [SerializeField] protected TextMeshProUGUI gainScoreUI;

    protected void Start()
    {
        //Update UI
        if (gainScoreUI == null)
        {
            return;
        }
        gainScoreUI.text = "";
          
    }
   
    protected virtual void Update()
    {
        TestHitTarget(); 
    }

    protected virtual int CalculateScore()
    {
        return baseScore;
    }

    public virtual void TakeHit(int hitpower)
    {
        int scoreGain = CalculateScore();
        gainScoreUI.text=scoreGain.ToString();
       

        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;

        Invoke(nameof(Destroy), 1f);
    }

  

    protected virtual void Destroy()
    {
        Destroy(gameObject);
    }

    private void TestHitTarget()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.TryGetComponent<HitTarget>(out HitTarget hitTarget))
                {
                    hitTarget.TakeHit(1);
                }
            }
        }
    }

}

