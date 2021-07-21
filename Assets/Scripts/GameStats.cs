public static class GameStats
{

    private static int bounty;
    private static int kills;

    // Add shots and hits to have accuracy rating

    public static int Bounty
    {
        get
        {
            return bounty;
        }
        set
        {
            bounty = value;
        }
    }

    public static int Kills
    {
        get
        {
            return kills;
        }
        set
        {
            kills = value;
        }
    }



    public static void Addbounty(int val)
    {
        bounty += val;
    }

    public static void AddKill()
    {
        kills += 1;
    }


    public static void Clear()
    {
        kills = 0;
        bounty = 0;
    }


}