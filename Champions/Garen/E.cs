using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System.Numerics;
using System.Collections.Generic;

namespace Garen
{
    public class E : GameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {

        }
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            ApiFunctionManager.AddParticleTarget(owner, "Garen_Base_E_Cas.troy", owner, 1, "Root");
            ApiFunctionManager.AddParticleTarget(owner, "Garen_Base_E_Spin.troy", owner, 1);
            ApiFunctionManager.AddParticleTarget(owner, "Garen_Base_E_Cancel.troy", owner, 1);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("GarenE", 3.0f, 1, owner);
            ApiFunctionManager.CreateTimer(3.0f, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
            });
            List<Unit> units = ApiFunctionManager.GetUnitsInRange(target, 500, true);
            foreach (Unit unit in units)
            {
                if (unit.Team != owner.Team)
                {
                    var ad = new[] { .7f, .8f, .9f, 1f, 1.1f }[spell.Level - 1] * owner.GetStats().AttackDamage.Total;
                    var damage = -5 + spell.Level * 25 + ad;
                    if (unit is Minion) damage *= 0.75f;
                    owner.DealDamageTo(target, damage * 1, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);


                }
            }
        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var unitsHit = ApiFunctionManager.GetUnitsInRange(owner, 175, true);

        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            var ad = new[] { .7f, .8f, .9f, 1f, 1.1f }[spell.Level - 1] * owner.GetStats().AttackDamage.Total;
            var damage = -5 + spell.Level * 25 + ad;
            if (target is Minion)
                damage *= 0.75f;
            owner.DealDamageTo(target, damage * 1, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
        }
        public void OnUpdate(double diff)
        {

        }
    }
}
