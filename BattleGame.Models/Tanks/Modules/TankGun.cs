using BattleGame.Models.Tanks.Interfaces;

namespace BattleGame.Models.Tanks.Modules
{
    public class TankGun : ITankModule
    {
        public int Sharpshooting { get; set; }
        public int Damage { get; set; }
        public int Distance { get; set; }
        public int Level { get; set; }

        public virtual void SetLevel(int level)
        {
        }
    }
}