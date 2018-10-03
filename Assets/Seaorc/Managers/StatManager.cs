using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] static int Strength;
    [SerializeField] static int Endurance;
    [SerializeField] static int Agility;
    [SerializeField] static int Charisma;
    [SerializeField] static int Perception;
    [SerializeField] static int Intelligence;
    [SerializeField] static int Luck;


    public int getStrength() { return Strength; }
    public void SetStrength(int _Strength) { Strength = _Strength; }

    public int GetEndurance() { return Endurance; }
    public void SetEndurance(int _Endurance) { Endurance = _Endurance; }

    public int GetAgility() { return Agility; }
    public void SetAgility(int _Agility) { Agility = _Agility; }

    public int GetCharisma() { return Charisma; }
    public void SetCharisma(int _Charisma) { Charisma = _Charisma; }

    public int GetPerception() { return Perception; }
    public void SetPerception(int _Perception) { Perception = _Perception; }

    public int GetIntelligence() { return Intelligence; }
    public void SetIntelligence(int _Intelligence) { Intelligence = _Intelligence; }

    public int GetLuck() { return Luck; }
    public void SetLuck(int _Luck) { Luck = _Luck; }
}