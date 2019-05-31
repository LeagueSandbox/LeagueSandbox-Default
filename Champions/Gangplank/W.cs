using GameServerCore.Domain.GameObjects;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class RemoveScurvy : GameScript
    {
        public void OnActivate(Champion owner)
        {

        }

        private void SelfWasDamaged()
        {
        }

        public void OnDeactivate(Champion owner)
        {

        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {	
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {	
            float ad = owner.GetStats().AttackDamage.Total * 0.1f;
            owner.RestoreHealth(15*ad);
            var buff = ((ObjAIBase) target).AddBuffGameScript("GangplankW", "GangplankW", spell);
            ApiFunctionManager.CreateTimer(5.0f, () =>
            {
                //ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                owner.RemoveBuffGameScript(buff);
            });				
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
