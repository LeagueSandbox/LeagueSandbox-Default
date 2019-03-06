using LeagueSandbox.GameServer.API;
using GameServerCore.Domain;
using GameServerCore.Domain.GameObjects;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;

namespace TaricPassive
{
    internal class TaricPassive : IBuffGameScript
    {
        
        private IBuff _visualBuff;
        public IChampion _owner;
        public void OnActivate(IObjAiBase unit, ISpell ownerSpell)
        {
            _visualBuff = AddBuffHudVisual("Gemcraft", 8f, 1, BuffType.COMBAT_ENCHANCER, unit,8.0f);
            for (float a = 0.0f; a < 8.0f; a += 0.1f)
            {
                if (unit.IsAttacking)
                {
                    RemoveBuffHudVisual(_visualBuff);
                }
            }
        }       

        public void OnHit(IObjAiBase unit)
        {

        }
        public void OnDeactivate(IObjAiBase unit)
        {
            
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
