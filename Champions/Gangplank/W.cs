using GameServerCore;
using GameServerCore.Domain.GameObjects;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Scripting.CSharp;
using GameServerCore.Enums;

namespace Spells
{
    public class RemoveScurvy : IGameScript
    {
        public void OnActivate(IChampion owner)
        {

        }

        private void SelfWasDamaged()
        {
        }

        public void OnDeactivate(IChampion owner)
        {

        }

        public void OnStartCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {	
        }

        public void OnFinishCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {	
            float ap = owner.Stats.AbilityPower.Total * 0.1f;
            owner.Stats.CurrentHealth += 15*ap; 
            var buff = ((ObjAIBase) target).AddBuffGameScript("GangplankW", "GangplankW", spell);
            CreateTimer(5.0f, () =>
            {
                //ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                owner.RemoveBuffGameScript(buff);
            });	
        }

        public void ApplyEffects(IChampion owner, IAttackableUnit target, ISpell spell, IProjectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
