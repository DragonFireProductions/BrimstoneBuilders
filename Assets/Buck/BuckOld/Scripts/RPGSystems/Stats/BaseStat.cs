using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat
{
    public List<StatBonus> statAdditives { get; set; }

    public int BaseValue { get; set; }
    public int FinalValue { get; set; }
    public string StatName { get; set; }
    public string StatDescription { get; set; }

    [Newtonsoft.Json.JsonConstructor]
    public BaseStat(int baseValue, string statName, string statDescription)
    {
        statAdditives = new List<StatBonus>();
        BaseValue = baseValue;
        StatName = statName;
        StatDescription = statDescription;
    }

    public void AddStatBonus(StatBonus statBonus)
    {
        statAdditives.Add(statBonus);
    }

    public void RemoveStatBonus(StatBonus statBonus)
    {
        statAdditives.Remove(statAdditives.Find(x => x.BonusValue == statBonus.BonusValue));
    }

    public int GetFinalStatValue()
    {
        FinalValue = 0;
        statAdditives.ForEach(x => FinalValue += x.BonusValue);
        FinalValue += BaseValue;
        return FinalValue;
    }
}
