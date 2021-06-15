using BattleGame.Models.Tanks.Interfaces;

namespace BattleGame.Models.Tanks
{
    public class Tank
    {
        public ITankModule Gun { get; set; }
        public ITankModule Corps { get; set; }
        public ITankModule Turret { get; set; }
    }
}