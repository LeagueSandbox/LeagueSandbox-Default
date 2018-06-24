using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class EzrealMysticShot : GameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            ApiFunctionManager.AddParticleTarget(owner, "ezreal_bow.troy", owner, 1, "L_HAND");
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 1150;
            var trueCoords = current + range;
            
            spell.AddProjectile("EzrealMysticShotMissile", trueCoords.X, trueCoords.Y);

            var buff = owner.AddBuffGameScript("Quickdraw", "Quickdraw", spell);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("Quickdraw", 6.0f, 0, owner);

            ApiFunctionManager.CreateTimer(6.0f, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                owner.RemoveBuffGameScript(buff);
            });
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var ad = owner.GetStats().AttackDamage.Total * 1.1f;
            var ap = owner.GetStats().AbilityPower.Total * 0.4f;
            var damage = 15 + spell.Level * 20 + ad + ap;
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);
            spell.LowerCooldown(0, 1);
            spell.LowerCooldown(1, 1);
            spell.LowerCooldown(2, 1);
            spell.LowerCooldown(3, 1);
            projectile.setToRemove();
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
