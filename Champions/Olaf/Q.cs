using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
 
namespace Olaf
{
    public class Q
    {
        public static void onStartCasting(Champion owner, Spell spell, Unit target)
        {
             
        }
 
        public static void onFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = new Vector2(spell.X, spell.Y) - current;
            Vector2 trueCoords;
 
            if (to.Length() > 1550)
            {
                to = Vector2.Normalize(to);
                var range = to * 1550;
                trueCoords = current + range;
            }
            else
            {
                trueCoords = new Vector2(spell.X, spell.Y);
            }
 
            spell.AddProjectile("OlafAxeThrowDamage", trueCoords.X, trueCoords.Y);
        }
 
        public static void applyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            ApiFunctionManager.AddParticleTarget(owner, "olaf_axeThrow_tar.troy", target, 1);
            var AD = owner.GetStats().AttackDamage.Total * 0.9f;
            var AP = owner.GetStats().AttackDamage.Total * 0.0f;
            var damage = 15 + spell.Level * 20 + AD + AP;
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);
        }
 
        public static void onUpdate(double diff)
        {
 
        }
    }
}