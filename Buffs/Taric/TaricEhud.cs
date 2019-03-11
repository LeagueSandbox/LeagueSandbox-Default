using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using GameServerCore.Domain;
using GameServerCore.Domain.GameObjects;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace TaricEhud
{
    internal class TaricEhud : IBuffGameScript
    {
        private IBuff _visualBuff;        
        public void OnActivate(IObjAiBase unit,ISpell spell)
        {
            var time = 1.1f + 0.1f * spell.Level;
            _visualBuff = AddBuffHudVisual("Stun", time, 1, BuffType.COMBAT_DEHANCER, unit);            
            //Immunity to slowness not added
        }

        public void OnDeactivate(IObjAiBase unit)
        {
            RemoveBuffHudVisual(_visualBuff);
            
        }

        private void OnAutoAttack(AttackableUnit target, bool isCrit)
        {
        }

        public void OnUpdate(double diff)
        {
            
        }
    }
}
