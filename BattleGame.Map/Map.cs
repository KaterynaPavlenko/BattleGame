using System.Collections.Generic;
using System.Linq;
using BattleGame.Models;

namespace BattleGame.Map
{
    public class Map
    {
        public Map()
        {
            Cells = new List<Cell>();
        }

        public List<Cell> Cells { get; set; }
        public int Width { get; set; }

        public int Height { get; set; }

        public Cell this[int x, int y]
        {
            get { return Cells.SingleOrDefault(c => c.X == x && c.Y == y); }
        }
    }
}