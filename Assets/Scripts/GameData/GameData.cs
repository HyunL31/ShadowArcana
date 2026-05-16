using System;
using System.Collections.Generic;

[Serializable]
public class GameDataBase
{
    public string ID;
}

[Serializable]
public class Scenario : GameDataBase
{
    public string NextID;
    public string CharacterID;
    public string Content;
    public string Background;
    public string BGM;
    public string SFX;
}

[Serializable]
public class Choice : GameDataBase
{
    public List<string> ArcanaID;
    public List<string> ReturnID;
}

[Serializable]
public class Arcana : GameDataBase
{
    public string Name;
    public string Description;
    public string Effect;
    public int Score;
    public int ATK;
}

[Serializable]
public class Character : GameDataBase
{
    public string Name;
}