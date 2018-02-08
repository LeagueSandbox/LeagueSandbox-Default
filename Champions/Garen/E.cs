using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System.Collections.Generic;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;

namespace Spells
{
    public class GarenE : GameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            Particle p = ApiFunctionManager.AddParticleTarget(owner, "Garen_Base_E_Spin.troy", owner, 1);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("GarenE", 3.0f, 1, owner);
            ApiFunctionManager.CreateTimer(3.0f, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                ApiFunctionManager.RemoveParticle(p);
            });
            for (float i = 0.0f; i < 3.0; i += 0.5f)
            {
                ApiFunctionManager.CreateTimer(i, () => { ApplySpinDamage(owner, spell, target); });
            }
        }

        private void ApplySpinDamage(Champion owner, Spell spell, AttackableUnit target)
        {
            List<AttackableUnit> units = ApiFunctionManager.GetUnitsInRange(owner, 500, true);
            foreach (AttackableUnit unit in units)
            {
                if (unit.Team != owner.Team)
                {
                    //PHYSICAL DAMAGE PER SECOND: 20 / 45 / 70 / 95 / 120 (+ 70 / 80 / 90 / 100 / 110% AD)
                    float ad = new[] {.7f, .8f, .9f, 1f, 1.1f}[spell.Level - 1] * owner.GetStats().AttackDamage.Total *
                               0.5f;
                    float damage = new[] {20, 45, 70, 95, 120}[spell.Level - 1] * 0.5f + ad;
                    if (unit is Minion) damage *= 0.75f;
                    target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL,
                        false);
                }
            }
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}

