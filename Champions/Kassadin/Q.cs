using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class NullLance : IGameScript
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
            ApiEventManager.OnSpellCast.AddListener(this, spell, OnStartCasting);
            ApiEventManager.OnSpellFinishCast.AddListener(this, spell, OnFinishCasting);
            ApiEventManager.OnSpellApplyEffects.AddListener(this, spell, ApplyEffects);
        }
        public void OnDeactivate() { }
        public void OnStartCasting(Unit target){
            if (owner is Champion) ApiFunctionManager.AddParticleTarget(owner as Champion, "Kassadin_Base_cas.troy", owner, 1, "L_HAND");
        }
        public void OnFinishCasting(Unit target) {
            spell.AddProjectileTarget("NullLance", target, true);
        }
        public void ApplyEffects(Unit target, Projectile projectile) {
            var ap = owner.GetStats().AbilityPower.Total * 0.7f;
            var damage = 30 + spell.Level * 50 + ap;

            if (target != null && !ApiFunctionManager.IsDead(target))
            {
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                if (target.IsDead)
                {
                }
            }
            projectile.setToRemove();
        }
     }
}