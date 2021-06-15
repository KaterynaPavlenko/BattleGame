namespace BattleGame.Models.Tanks.Modules
{
    public class ArmorPiercingCannon : TankGun
    {
        public ArmorPiercingCannon(int level)
        {
            SetLevel(level);
        }


        public sealed override void SetLevel(int level)
        {
            switch (level)
            {
                case 1:
                    Level = 1;
                    Damage = 1;
                    Distance = 3;
                    break;
                case 2:
                    Level = 2;
                    Damage = 2;
                    Distance = 4;
                    break;
                case 3:
                    Level = 2;
                    Damage = 3;
                    Distance = 5;
                    break;
            }
        }
    }
}