using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Jinx
{
    public class R : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 25000;
            var trueCoords = current + range;

            ApiFunctionManager.AddParticle(owner, "Jinx_R_Beam.troy", trueCoords.X, trueCoords.Y);
            ApiFunctionManager.FaceDirection(owner, trueCoords, true, 0);
            spell.spellAnimation("SPELL4", owner);
            ApiFunctionManager.AddParticleTarget(owner, "Jinx_R_Cas.troy", owner, 1, "Rocket_Launcher_End");
        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 25000;
            var trueCoords = current + range;

            spell.AddProjectile("JinxR", trueCoords.X, trueCoords.Y);
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            List<Unit> units = ApiFunctionManager.GetUnitsInRange(owner, 225, true);
            foreach (Unit unit in units)
            {
                if (target.Team != owner.Team)
                {
                    ApiFunctionManager.AddParticleTarget(owner, "Jinx_R_Tar_Weak.troy", target);
                    float bonusAd = owner.GetStats().AttackDamage.Total - owner.GetStats().AttackDamage.BaseValue;
                    float misHP = unit.GetStats().HealthPoints.Total - unit.GetStats().CurrentHealth;
                    float misHPDMG = new[] { 0.25f, 0.3f, 0.35f }[spell.Level - 1] * misHP;
                    float minDMG = 75 + spell.Level * 50 + bonusAd * 0.5f;

                    for (int i = 0; i < 10; i++)
                    {
                        ApiFunctionManager.CreateTimer(0.1f, () =>
                        {
                            var dmg = minDMG + minDMG * 0.1f;
                            var damage = dmg + misHPDMG;
                            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                            projectile.setToRemove();
                        });
                    }
                }
            }

        }
        public void OnUpdate(double diff) { }
    }
}