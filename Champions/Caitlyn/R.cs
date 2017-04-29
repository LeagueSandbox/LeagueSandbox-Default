using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class CaitlynAceintheHole : IGameScript
    {
        GameScriptInformation info;
        Spell spell;
        Unit owner;
        public void OnActivate(GameScriptInformation scriptInfo)
        {
            info = scriptInfo;
            spell = info.OwnerSpell;
            owner = info.OwnerUnit;
            //Setup event listeners
            ApiEventManager.OnSpellFinishCast.AddListener(this, spell, OnFinishCasting);
            ApiEventManager.OnSpellApplyEffects.AddListener(this, spell, ApplyEffects);
        }
        public void OnDeactivate() { }
        public void OnFinishCasting(Unit target) {
            spell.AddProjectileTarget("CaitlynAceintheHoleMissile", target);
        }
        public void ApplyEffects(Unit target, Projectile projectile) {
            if (target != null && !target.IsDead)
            {
                // 250/475/700
                var damage = 250 + owner.GetStats().AttackDamage.Total * 2;
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            }
            projectile.setToRemove();
        }
     }
}