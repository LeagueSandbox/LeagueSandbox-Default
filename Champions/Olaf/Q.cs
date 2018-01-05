using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class OlafAxeThrowCast : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target){

        }
        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target) {
            var current = new Vector2(owner.X, owner.Y);
            var to = new Vector2(spell.X, spell.Y) - current;
            Vector2 trueCoords;
 
            if (to.Length() > 1651)
            {
                to = Vector2.Normalize(to);
                var range = to * 1651;
                trueCoords = current + range;
            }
            else
            {
                trueCoords = new Vector2(spell.X, spell.Y);
            }
 
            spell.AddProjectile("OlafAxeThrowDamage", trueCoords.X, trueCoords.Y);
        }
        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile) {
            ApiFunctionManager.AddParticleTarget(owner, "olaf_axeThrow_tar.troy", target, 1);
            var AD = owner.GetStats().AttackDamage.Total * 1.1f;
            var AP = owner.GetStats().AttackDamage.Total * 0.0f;
            var damage = 15 + spell.Level * 20 + AD + AP;
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);
        }
        public void OnUpdate(double diff) {

        }
     }
}
