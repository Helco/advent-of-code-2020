using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static aoc.day12.Common;

namespace aoc.day12
{
    public enum ActionKind
    {
        North,
        South,
        East,
        West,
        Left,
        Right,
        Forward
    }

    public struct Action
    {
        public readonly ActionKind Kind;
        public readonly int Value;
        
        public Action(string line)
        {
            Kind = line[0] switch
            {
                'N' => ActionKind.North,
                'S' => ActionKind.South,
                'E' => ActionKind.East,
                'W' => ActionKind.West,
                'L' => ActionKind.Left,
                'R' => ActionKind.Right,
                'F' => ActionKind.Forward,
                _ => throw new InvalidDataException()
            };
            Value = int.Parse(line[1..]);
        }
    }

    public static class Common
    {
        public static readonly IVec2 North = IVec2.Up;
        public static readonly IVec2 South = IVec2.Down;
        public static readonly IVec2 East = IVec2.Right;
        public static readonly IVec2 West = IVec2.Left;
    }

    public struct Ship
    {
        public readonly IVec2 Pos;
        public readonly IVec2 Dir;

        public static Ship Initial => new Ship(IVec2.Zero, East);

        public Ship(IVec2 pos, IVec2 dir)
        {
            Pos = pos;
            Dir = dir;
        }

        public Ship(Ship prevShip, Action action)
        {
            Pos = prevShip.Pos;
            Dir = prevShip.Dir;

            switch(action.Kind)
            {
                case ActionKind.North: Pos += North * action.Value; break;
                case ActionKind.South: Pos += South * action.Value; break;
                case ActionKind.East: Pos += East * action.Value; break;
                case ActionKind.West: Pos += West * action.Value; break;
                case ActionKind.Left: Dir = Dir.Rotate90(-action.Value); break;
                case ActionKind.Right: Dir = Dir.Rotate90(+action.Value); break;
                case ActionKind.Forward: Pos += Dir * action.Value; break;
                default: throw new NotImplementedException();
            }
        }

        public Ship(IEnumerable<Action> actions)
        {
            var ship = Initial;
            foreach (var action in actions)
                ship = new Ship(ship, action);
            Pos = ship.Pos;
            Dir = ship.Dir;
        }
    }

    public struct WaypointShip
    {
        public readonly IVec2 ShipPos;
        public readonly IVec2 WpPos;

        public static WaypointShip Initial => new WaypointShip(IVec2.Zero, East * 10 + North * 1);

        public WaypointShip(IVec2 shipPos, IVec2 wpPos)
        {
            ShipPos = shipPos;
            WpPos = wpPos;
        }

        public WaypointShip(WaypointShip prevShip, Action action)
        {
            ShipPos = prevShip.ShipPos;
            WpPos = prevShip.WpPos;

            switch (action.Kind)
            {
                case ActionKind.North: WpPos += North * action.Value; break;
                case ActionKind.South: WpPos += South * action.Value; break;
                case ActionKind.East: WpPos += East * action.Value; break;
                case ActionKind.West: WpPos += West * action.Value; break;
                case ActionKind.Left: WpPos = WpPos.Rotate90(-action.Value); break;
                case ActionKind.Right: WpPos = WpPos.Rotate90(+action.Value); break;
                case ActionKind.Forward: ShipPos += WpPos * action.Value; break;
                default: throw new NotImplementedException();
            }
        }

        public WaypointShip(IEnumerable<Action> actions)
        {
            var ship = Initial;
            foreach (var action in actions)
                ship = new WaypointShip(ship, action);
            ShipPos = ship.ShipPos;
            WpPos = ship.WpPos;
        }
    }

    public static class Day12
    {
        public static void Run()
        {
            var inputActions = File.ReadAllLines("day12/input.txt").Select(l => new Action(l)).ToArray();

            var shipAtTarget = new Ship(inputActions);
            Console.WriteLine(shipAtTarget.Pos.LengthManhattan);
            var wpShipAtTarget = new WaypointShip(inputActions);
            Console.WriteLine(wpShipAtTarget.ShipPos.LengthManhattan);
        }
    }
}
