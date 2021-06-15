using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using BattleGame.Models;

namespace BattleGame.Map.Controller
{
    public static class MapController
    {
        public static Map map;
        public static int With = 40;
        public static int Height = 40;
        public static Image PlayerBase;
        public static Image Player;
        public static Image EnemiesBase;
        public static Image ground;
        public static Image wallV;
        public static Image wallX;
        public static int cellSize = 30;
        private static readonly Random _random = new Random();
        private static Point delta = new Point(0, 0);

        public static void Init()
        {
            var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent;
            if (directoryInfo?.Parent != null)
            {
                ground = new Bitmap(Path.Combine(
                    directoryInfo.Parent.FullName,
                    "Sprites\\ground.png"));
                wallV = new Bitmap(Path.Combine(
                    directoryInfo.Parent.FullName,
                    "Sprites\\wall.png"));
                wallX = new Bitmap(Path.Combine(
                    directoryInfo.Parent.FullName,
                    "Sprites\\wall.png"));
                PlayerBase = new Bitmap(Path.Combine(
                    directoryInfo.Parent.FullName,
                    "Sprites\\playerBase1.png"));
                EnemiesBase = new Bitmap(Path.Combine(
                    directoryInfo.Parent.FullName,
                    "Sprites\\enemiesBase1.png"));
                Player = new Bitmap(Path.Combine(
                    directoryInfo.Parent.FullName,
                    "Sprites\\player1.png"));
            }

            map = GetMap();
            GenerateEnemiesBase();
            GeneratePlayerBase();
            GeneratePlayer();
            SetUpPlayer();
        }

        public static void TryToStep(Direction direction)
        {
            var player = map.Cells.OfType<Player>().Single();
            switch (direction)
            {
                case Direction.Up:
                    if (!(map.Cells.FirstOrDefault(x => x.Y == player.Y - 1 && x.X == player.X) is Wall))
                    {
                        Replace<Player>(player.X, player.Y - 1, Player);
                        Replace<Ground>(player.X, player.Y, ground);
                        if (player.Y < map.Width / 2)
                            delta.Y++;
                    }

                    break;
                case Direction.Down:
                    if (!(map.Cells.FirstOrDefault(x => x.Y == player.Y + 1 && x.X == player.X) is Wall))
                    {
                        Replace<Player>(player.X, player.Y + 1, Player);
                        Replace<Ground>(player.X, player.Y, ground);
                        if (player.Y < map.Width / 2)
                            delta.Y--;
                    }

                    break;
                case Direction.Left:
                    if (!(map.Cells.FirstOrDefault(x => x.X == player.X - 1 && x.Y == player.Y) is Wall))
                    {
                        Replace<Player>(player.X - 1, player.Y, Player);
                        Replace<Ground>(player.X, player.Y, ground);
                        if (player.X > map.Height / 2)
                            delta.X++;
                    }

                    break;
                case Direction.Right:
                    if (!(map.Cells.FirstOrDefault(x => x.X == player.X + 1 && x.Y == player.Y) is Wall))
                    {
                        Replace<Player>(player.X + 1, player.Y, Player);
                        Replace<Ground>(player.X, player.Y, ground);
                        if (player.X > map.Height / 2)
                            delta.X--;
                    }

                    break;
            }

            var playerInMap = map.Cells.OfType<Player>().Single();
            playerInMap.LastDirection = direction;
        }

        public static Map GetMap()
        {
            Initialize(With, Height);
            var startCell = new Ground(1, 1, ground);

            var cellsForBreak = new List<Cell>();

            do
            {
                Replace<Ground>(startCell.X, startCell.Y, ground);
                var brokenWall = cellsForBreak.SingleOrDefault(x => x.X == startCell.X && x.Y == startCell.Y);
                if (brokenWall != null) cellsForBreak.Remove(brokenWall);
                var nearCells = GetNeighbours<Wall>(startCell.X, startCell.Y);
                cellsForBreak.AddRange(nearCells);
                cellsForBreak = cellsForBreak
                    .Where(wall =>
                        GetNeighbours<Ground>(wall.X, wall.Y).Count() <= 1)
                    .Distinct()
                    .ToList();
                var randomCell = GetRandomCell(cellsForBreak);
                startCell.X = randomCell?.X ?? 0;
                startCell.Y = randomCell?.Y ?? 0;
            } while (cellsForBreak.Any());

            return map;
        }

        public static void SetUpPlayer()
        {
            var player = map.Cells.OfType<Player>().Single();
            player.IsBot = false;
        }

        private static void GeneratePlayerBase()
        {
            var groundCells = map.Cells.OfType<Ground>().ToList();
            var minGroundX = groundCells.Select(cell => cell.X).Min();
            var groundCellsWithMinX = groundCells.Where(cell => cell.X == minGroundX);
            var minGroundY = groundCellsWithMinX.Select(cell => cell.Y).Min();
            var playerCell = groundCellsWithMinX.Single(cell => cell.Y == minGroundY);
            Replace<Base>(playerCell.X, playerCell.Y, PlayerBase);
        }

        private static void GenerateEnemiesBase()
        {
            var groundCells = map.Cells.OfType<Ground>().ToList();
            var minGroundX = groundCells.Select(cell => cell.X).Max();
            var groundCellsWithMinX = groundCells.Where(cell => cell.X == minGroundX);
            var minGroundY = groundCellsWithMinX.Select(cell => cell.Y).Max();
            var enemiesCell = groundCellsWithMinX.Single(cell => cell.Y == minGroundY);
            Replace<Base>(enemiesCell.X, enemiesCell.Y, EnemiesBase);
        }

        private static void GeneratePlayer()
        {
            var groundCells = map.Cells.OfType<Ground>().ToList();
            var minGroundX = groundCells.Select(cell => cell.X).Min();
            var groundCellsWithMinX = groundCells.Where(cell => cell.X == minGroundX);
            var minGroundY = groundCellsWithMinX.Select(cell => cell.Y).Min();
            var playerCell = groundCellsWithMinX.Single(cell => cell.Y == minGroundY);
            Replace<Player>(playerCell.X, playerCell.Y, Player);
        }

        private static void Initialize(int width, int height)
        {
            map = new Map
            {
                Width = width,
                Height = height
            };
            for (var i = 0; i < map.Height; i++)
            for (var j = 0; j < map.Width; j++)
                if (i == 0 && j == 0 || j == 0 && i == map.Height || i == 0 && j == map.Width ||
                    j == map.Width && i == map.Height)
                    map.Cells.Add(new Wall(j, i) {Sprite = wallX});
                else
                    map.Cells.Add(new Wall(j, i) {Sprite = wallV});
        }

        private static void Replace<T>(int x, int y, Image sprite) where T : Cell
        {
            var deleteCell = map.Cells.SingleOrDefault(cell => cell.X == x && cell.Y == y);
            var o = Activator.CreateInstance(typeof(T), deleteCell?.X, deleteCell?.Y, sprite);
            var objectInTheCell = (T) o;
            map.Cells.Remove(deleteCell);
            map.Cells.Add(objectInTheCell);
        }

        private static Cell GetRandomCell(IEnumerable<Cell> nearCells)
        {
            if (!nearCells.Any()) return null;

            var list = nearCells.ToList();
            var randomIndex = _random.Next(0, list.Count);
            return list[randomIndex];
        }

        public static void Bullet(Direction direction)
        {
            var player = map.Cells.OfType<Player>().Single();
            var bullet = new Bullet(player.X, player.Y);
            switch (direction)
            {
                case Direction.Up:
                    if (!(map.Cells.FirstOrDefault(x => x.Y == player.Y - 1 && x.X == player.X) is Wall))
                        do
                        {
                            Replace<Bullet>(bullet.X, bullet.Y - 2, Player);
                            Replace<Ground>(bullet.X, bullet.Y - 1, ground);
                        } while (!(map.Cells.FirstOrDefault(x => x.Y == bullet.Y - 2 && x.X == bullet.X) is Wall));

                    break;
                case Direction.Down:
                    if (!(map.Cells.FirstOrDefault(x => x.Y == player.Y + 1 && x.X == player.X) is Wall))
                        do
                        {
                            Replace<Bullet>(bullet.X, bullet.Y + 2, Player);
                            Replace<Ground>(bullet.X, bullet.Y + 1, ground);
                        } while (!(map.Cells.FirstOrDefault(x => x.Y == bullet.Y + 2 && x.X == bullet.X) is Wall));

                    break;
                case Direction.Left:
                    if (!(map.Cells.FirstOrDefault(x => x.X == player.X - 1 && x.Y == player.Y) is Wall))
                    {
                        Replace<Player>(player.X - 1, player.Y, Player);
                        Replace<Ground>(player.X, player.Y, ground);
                    }

                    break;
                case Direction.Right:
                    if (!(map.Cells.FirstOrDefault(x => x.X == player.X + 1 && x.Y == player.Y) is Wall))
                    {
                        Replace<Player>(player.X + 1, player.Y, Player);
                        Replace<Ground>(player.X, player.Y, ground);
                    }

                    break;
            }
        }

        private static IEnumerable<Cell> GetNeighbours<T>(int X, int Y) where T : Cell
        {
            var nearCells = new List<Cell>
            {
                map[X - 1, Y], //lt
                map[X + 1, Y], //rt 
                map[X, Y - 1], //up 
                map[X, Y + 1] //dw
            };
            var answer = nearCells
                .Where(x => x != null && x.X > 0 && x.X < map.Width && x.Y > 0 && x.Y < map.Height &&
                            x.X != map.Width - 1 && x.Y != map.Height - 1)
                .OfType<T>();
            return answer;
        }


        public static void DrawMap(Graphics g)
        {
            for (var i = 0; i < map.Cells.Count; i++)
                if (map.Cells[i].Sprite != null)
                {
                    if (map.Cells[i] is Wall)
                        g.DrawImage(map.Cells[i].Sprite,
                            new Rectangle(
                                new Point((map.Cells[i].X + delta.X) * cellSize, (map.Cells[i].Y + delta.Y) * cellSize),
                                new Size(cellSize, cellSize)), 0, 0,
                            75, 79, GraphicsUnit.Pixel);
                    else
                        g.DrawImage(map.Cells[i].Sprite,
                            new Rectangle(
                                new Point((map.Cells[i].X + delta.X) * cellSize, (map.Cells[i].Y + delta.Y) * cellSize),
                                new Size(cellSize, cellSize)), 0, 0,
                            200, 200, GraphicsUnit.Pixel);
                }
        }
    }
}