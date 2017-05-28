using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Behavior
{
    class Poro : IGameScript
    {
        Unit selfUnit;
        public void OnActivate(GameScriptInformation scriptInfo)
        {
            selfUnit = scriptInfo.TargetUnit;
            Console.WriteLine("Poro activated");
            selfUnit.GetStats().MoveSpeed.BaseValue = 300;
            selfUnit.SetWaypoints(new List<Vector2> { new Vector2(selfUnit.X, selfUnit.Y),  new Vector2(selfUnit.X + 50000, selfUnit.Y) });
        }

        public void OnDeactivate()
        {
        }
    }
}
