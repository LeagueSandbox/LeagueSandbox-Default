using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Warwick
{
    public class R : GameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            ApiFunctionManager.AddParticleTarget(owner, "InfiniteDuress_buf.troy", owner);
            ApiFunctionManager.TeleportTo(owner, target.X, target.Y);
        }

        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
           ApiFunctionManager.AddParticleTarget(owner, "InfiniteDuress_tar.troy", owner, 1, "root");
            spell.AddProjectileTarget("InfiniteDuress", target);

            for (float i = 0.0f; i < 1.5; i += 0.3f)
            {
                ApiFunctionManager.CreateTimer(i, () =>
                {
                    ApplyDamage(owner, spell, target);
                });
            }
            float time = 1.5f;
            var buff = target.AddBuffGameScript("Disarm", "Disarm", spell);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("InfiniteDuress", time, 1, target);
            ApiFunctionManager.CreateTimer(time, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                target.RemoveBuffGameScript(buff);
            });
        }

        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {

        }

        public void OnUpdate(double diff)
        {

        }

        public void ApplyDamage(Champion owner, Spell spell, Unit target)
        {
            var bonusAd = owner.GetStats().AttackDamage.PercentBonus * 0.4f;
            var damage = 10 + spell.Level * 20 + bonusAd;
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);

        }
    }
}