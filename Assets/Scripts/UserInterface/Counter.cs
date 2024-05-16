using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Counter : MonoBehaviour
{
    [SerializeField] TMP_Text minerscounter = null;
    [SerializeField] TMP_Text minersunitcounter = null;
    [SerializeField] TMP_Text warriorscounter = null;
    [SerializeField] TMP_Text warriorsunitcounter = null;
    [SerializeField] Settlement settlementdata;
    [SerializeField] Settlements settlements;
    int warriorcounter=0;
    int warriorunitcounter=0;
    int minerunitcounter=0;
    int minercounter=0;

    public void Update()
    {
        minercounter = 0;
        warriorcounter = 0;
        minerunitcounter = 0;
        warriorunitcounter = 0;
        var data = settlementdata.data;
        List<GameObject> towerslist = GameObject.FindGameObjectsWithTag("Tower").ToList();
        for( int i =0; i<towerslist.Count; i++ ) 
        {
            if(towerslist[i].GetComponent("WarriorTower") != null)
            {
                warriorcounter++;
                warriorunitcounter+= towerslist[i].GetComponent<WarriorTower>().GetUnitsCount();
            }
            if (towerslist[i].GetComponent("ArcherTower") != null)
            {
                warriorcounter++;
                warriorunitcounter += towerslist[i].GetComponent<ArcherTower>().GetUnitsCount();
            }
            if (towerslist[i].GetComponent("Mine") != null)
            {
                minercounter++;
                minerunitcounter += towerslist[i].GetComponent<Mine>().GetUnitsCount();
            }
            
        }
        minersunitcounter.text = minerunitcounter.ToString("#");
        warriorsunitcounter.text = warriorunitcounter.ToString("#");
        if (minerunitcounter == 0 || warriorunitcounter == 0)
        {
            if (minerunitcounter == 0)
            {
                minersunitcounter.text = minerunitcounter.ToString("0");
            }
            if (warriorunitcounter == 0)
            {
                warriorsunitcounter.text = warriorunitcounter.ToString("0");
            }
        }
        minerscounter.text = minercounter.ToString("#");
        warriorscounter.text = warriorcounter.ToString("#");

    }
}
