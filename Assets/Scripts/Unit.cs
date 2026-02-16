using UnityEngine;

public class Unit : MonoBehaviour
{

    public string unitName;
    // STATS
    public int STR; 
    public int HP;
    public int MAG = 5;

    // inGame
    public int currentHP;

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
       
        return false;
    }

    public void Heal()
    {
        currentHP += 2 * MAG;
        if(currentHP > HP)
            currentHP = HP;
    }
}
