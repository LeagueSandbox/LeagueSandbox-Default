using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Behavior
{
    class Poro : IGameScript
    {
        public void OnActivate(GameScriptInformation scriptInfo)
        {
            Console.WriteLine("Poro activated");
        }

        public void OnDeactivate()
        {
        }
    }
}
