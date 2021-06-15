using System.Drawing;

namespace BattleGame.Models.Tanks
{
    public class Bullet : Cell
    {
        public Bullet(int x, int y) : base(x, y)
        {
        }

        public Bullet(int x, int y, Image sprite) : base(x, y)
        {
            Sprite = sprite;
        }

        public override bool IsAllowStep { get; set; }
    }
}