namespace BattleGame.Models.Tanks.Modules
{
    public class RapidFiringGun : TankGun
    {
        public RapidFiringGun(int level)
        {
            SetLevel(level);
        }

        public sealed override void SetLevel(int level)
        {
            switch (level)
            {
                case 1:
                    Level = 1;
                    Damage = 2;
                    Distance = 4;
                    break;
                case 2:
                    Level = 2;
                    Damage = 4;
                    Distance = 6;
                    break;
                case 3:
                    Level = 3;
                    Damage = 6;
                    Distance = 8;
                    break;
            }
        }
    }
}