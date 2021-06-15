using System.Drawing;

namespace BattleGame.Models
{
    public abstract class Cell
    {
        protected Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Image Sprite { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public abstract bool IsAllowStep { get; set; }
    }
}