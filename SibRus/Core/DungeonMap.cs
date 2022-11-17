using RLNET;
using RogueSharp;
using SibRus.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SibRus.Core
{
    public class DungeonMap : Map
    {
        public List<Rectangle> Rooms;

        private readonly List<Monster> _monsters;
        private readonly List<TreasurePile> _treasurePiles;

        public Stairs StairsUp { get; set; }

        public Stairs StairsDown { get; set; }

        public List<Door> Doors { get; set; }

        public DungeonMap()
        {
            Game.SchedulingSystem.Clear();

            Rooms = new List<Rectangle>();

            _monsters = new List<Monster>();
            _treasurePiles = new List<TreasurePile>();

            Doors = new List<Door>();
        }

        public void Draw( RLConsole mapConsole, RLConsole statConsole )
        {
            mapConsole.Clear();
            foreach ( Cell cell in GetAllCells())
            {
                SetConsoleSymbolForICell(mapConsole, cell);
            }

            int i = 0;

            foreach (Door door in Doors)
            {
                door.Draw(mapConsole, this);
            }

            StairsUp.Draw(mapConsole, this);
            StairsDown.Draw(mapConsole, this);

            foreach (Monster monster in _monsters)
            {
                monster.Draw(mapConsole, this);

                if ( IsInFov(monster.X, monster.Y))
                {
                    monster.DrawStats(statConsole, i);
                    i++;
                }
            }
        }

        private void SetConsoleSymbolForICell(RLConsole console, ICell cell)
        {
            if (!cell.IsExplored)
            {
                return;
            }

            if (IsInFov(cell.X, cell.Y))
            {
                
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                }
            }
  
            else
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                }
            }
        }

        public bool CanMoveDownToNextLevel()
        {
            Player player = Game.Player;
            return StairsDown.X == player.X && StairsDown.Y == player.Y;
        }

        public void UpdatePlayerFieldOfView()
        {
            Player player = Game.Player;

            ComputeFov(player.X, player.Y, player.Awareness, true);

            foreach( Cell cell in GetAllCells())
            {
                if (IsInFov(cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }

        public bool SetActorPosition( Actor actor, int x, int y)
        {
            if ( GetCell(x, y).IsWalkable)
            {
                SetIsWalkable(actor.X, actor.Y, true);

                actor.X = x;
                actor.Y = y;

                SetIsWalkable(actor.X, actor.Y, false);

                // Try to open a door if one exists here
                OpenDoor(actor, x, y);

                if (actor is Player)
                {
                    UpdatePlayerFieldOfView();
                }
                return true;
            }
            return false;
        }

        public void SetIsWalkable( int x, int y, bool isWalkable)
        {
            ICell cell = GetCell(x, y );
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }
        
        public void AddPlayer(Player player)
        {
            Game.Player = player;
            SetIsWalkable(player.X, player.Y, false);
            UpdatePlayerFieldOfView();
            Game.SchedulingSystem.Add(player);
        }

        public void AddTreasure(int x, int y, ITreasure treasure)
        {
            _treasurePiles.Add(new TreasurePile(x, y, treasure));
        }

        public void AddMonster(Monster monster)
        {   
            _monsters.Add(monster);

            SetIsWalkable(monster.X, monster.Y, false);

            Game.SchedulingSystem.Add(monster);
        }

        public void RemoveMonster(Monster monster)
        {
            _monsters.Remove(monster);
            SetIsWalkable(monster.X, monster.Y, true);
            Game.SchedulingSystem.Remove(monster);
        }

        public Monster GetMonsterAt( int x, int y)
        {
            return _monsters.FirstOrDefault(m => m.X == x && m.Y == y);
        }

        public Point GetRandomLocationInRoom(Rectangle room)
        {
            if (DoesRoomHaveWalkableSpace(room))
            {
                for (int i = 0; i < 100; i++)
                {
                    int x = Game.Random.Next(1, room.Width - 2) + room.X;
                    int y = Game.Random.Next(1, room.Height - 2) + room.Y;
                    if (IsWalkable(x, y))
                    {
                        return new Point(x, y);
                    }
                }
            }

            return default(Point);
        }

        public bool DoesRoomHaveWalkableSpace(Rectangle room)
        {
            for (int x = 1; x <= room.Width - 2; x++)
            {
                for (int y = 1; y <= room.Height - 2; y++)
                {
                    if (IsWalkable(x + room.X, y + room.Y))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Return the door at the x,y position or null if one is not found.
        public Door GetDoor(int x, int y)
        {
            return Doors.SingleOrDefault(d => d.X == x && d.Y == y);
        }

        // The actor opens the door located at the x,y position
        private void OpenDoor(Actor actor, int x, int y)
        {
            Door door = GetDoor(x, y);
            if (door != null && !door.IsOpen)
            {
                door.IsOpen = true;
                var cell = GetCell(x, y);
                // Once the door is opened it should be marked as transparent and no longer block field-of-view
                SetCellProperties(x, y, true, cell.IsWalkable, cell.IsExplored);

                Game.MessageLog.Add($"{actor.Name} opened a door");
            }
        }
    }
}
