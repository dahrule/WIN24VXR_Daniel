using TMPro;
using UnityEngine;

public class HitTarget : MonoBehaviour
{
    [SerializeField] protected int baseScore = 1;
    [SerializeField] protected TextMeshProUGUI gainScoreUI;

    private void Start()
    {
        // Reset the gainScoreUI to an empty string at the start of the game.
        if (gainScoreUI == null)
        {
            return;
        }
        gainScoreUI.text = "";
    }

    // Moved this to a separate script for readability: MouseClicksToTestHits
    /* protected virtual void Update()
     {
         TestHitTarget(); 
     }*/

    protected virtual int CalculateScore() // This method can be accessed and overridden in a child class because it has the protected and virtual modifiers respectively.
    {
        return baseScore;
    }

    public virtual void TakeHit(int hitpower) // This method can be accessed and overridden in a child class because it has the public and virtual modifiers respectively.It can also be accessed by any class due to the public modifier.It can also be accessed by any class due to the public modifier.
    {
        int scoreGain = CalculateScore();
        gainScoreUI.text=scoreGain.ToString();

        // The following two lines are to avoid registering multiple hits while we wait for the object to be destroyed.
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;

        Invoke(nameof(Destroy), 1f);
    }

    protected virtual void Destroy() // This method can be accessed and overridden in a child class because it has the protected and virtual modifiers respectively.
    {
        Destroy(gameObject);
    }

    // Moved this to a separate script for readability: MouseClicksToTestHits
    /*private void TestHitTarget()
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
    }*/

}

