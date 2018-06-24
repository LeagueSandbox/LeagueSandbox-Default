using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class DetonatingShot : GameScript
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
            ApiFunctionManager.CreateTimer(1.0f, () => {
                var ap = owner.GetStats().AbilityPower.Total * 0.2f;
                var damage = 7 + spell.Level * 9 + ap;
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);				
            });	
            ApiFunctionManager.CreateTimer(2.0f, () => {
                var ap = owner.GetStats().AbilityPower.Total * 0.2f;
                var damage = 7 + spell.Level * 9 + ap;
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);				
            });	
            ApiFunctionManager.CreateTimer(3.0f, () => {
                var ap = owner.GetStats().AbilityPower.Total * 0.2f;
                var damage = 7 + spell.Level * 9 + ap;
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);				
            });	
            ApiFunctionManager.CreateTimer(4.0f, () => {
                var ap = owner.GetStats().AbilityPower.Total * 0.2f;
                var damage = 7 + spell.Level * 9 + ap;
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);				
            });	
            ApiFunctionManager.CreateTimer(5.0f, () => {
                var ap = owner.GetStats().AbilityPower.Total * 0.2f;
                var damage = 7 + spell.Level * 9 + ap;
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);				
            });				
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {	
            spell.AddProjectileTarget("DetonatingShot", target, false);	
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            float time = 5;
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("ExplosiveShotDebuff", time, 5, (ObjAIBase) target);			
            ApiFunctionManager.CreateTimer(time, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
            });			
            projectile.setToRemove();			
        }

        public void OnUpdate(double diff)
        {
        }
    }
}


