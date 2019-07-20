using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServerCore.Domain;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.Scripting.CSharp;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;

namespace Recall
{
    class Recall : IBuffGameScript
    {
        private IBuff _visualBuff;
        private GameScriptTimer _recallTimer;

        public void OnActivate(IObjAiBase unit, ISpell ownerSpell)
        {
            _visualBuff = AddBuffHudVisual("Recall", 8.0f, 1, BuffType.COMBAT_ENCHANCER, unit);

            _recallTimer = CreateTimer(7.9f, () =>
            {
                ((IChampion) unit).Recall();
            });
        }

        public void OnDeactivate(IObjAiBase unit)
        {
            RemoveBuffHudVisual(_visualBuff);

            if (!_recallTimer.IsDead())
            {
                _recallTimer.EndTimerWithoutCallback();
            }
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
