using TMPro;
using UnityEngine;

public class HitTarget : MonoBehaviour
{
    [SerializeField] protected int baseScore = 1;

    [SerializeField] protected TextMeshProUGUI gainScoreUI;

    protected void Start()
    {
        //Update UI
        if (gainScoreUI != null)
        {
            gainScoreUI.text = "";
        }  
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
        UpdateUI(scoreGain);
        Invoke(nameof(Destroy), 1f);
    }

    private void UpdateUI(int gainScore)
    {
        //UpdateUI
        if (gainScoreUI != null)
        {
            gainScoreUI.text = "+" + gainScore.ToString();
        }
    }

    protected void Destroy()
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

