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
    public class CaitlynPiltoverPeacemaker : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target){

        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target) {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 1150;
            var trueCoords = current + range;

            spell.AddProjectile("CaitlynPiltoverPeacemaker", trueCoords.X, trueCoords.Y, true);
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) {
            var reduc = Math.Min(projectile.ObjectsHit.Count, 5);
            var baseDamage = new[] {20, 60, 100, 140, 180}[spell.Level - 1] + 1.3f * owner.GetStats().AttackDamage.Total;
            var damage = baseDamage * (1 - reduc / 10);
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
        }
        public void OnUpdate(double diff) {

        }
     }
}