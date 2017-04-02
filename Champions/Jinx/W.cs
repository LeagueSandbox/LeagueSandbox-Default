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
    public class W : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 1500;
            var trueCoords = current + range;
            var curser = new Vector2(spell.X, spell.Y);
            var castrange = Vector2.Distance(current, curser);

            ApiFunctionManager.FaceDirection(owner, curser, true, 0);
            ApiFunctionManager.AddParticle(owner, "Jinx_W_Beam.troy", trueCoords.X, trueCoords.Y);
            spell.spellAnimation("SPELL2", owner);
            ApiFunctionManager.AddParticleTarget(owner, "Jinx_W_Cas.troy.troy", owner);
            
        }

        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 1500;
            var trueCoords = current + range;
            var curser = new Vector2(spell.X, spell.Y);
            var castrange = Vector2.Distance(current, curser);
            ApiFunctionManager.CreateTimer(0.6f, () =>
            {
                if (castrange <= 1500)
                {
                    ApiFunctionManager.AddParticle(owner, "Jinx_W_Mis.troy", spell.X, spell.Y);
                    spell.AddProjectile("JinxW", spell.X, spell.Y);
                }
                if (castrange > 1500)
                {
                    ApiFunctionManager.AddParticle(owner, "Jinx_W_Mis.troy", trueCoords.X, trueCoords.Y);
                    spell.AddProjectile("JinxW", trueCoords.X, trueCoords.Y);
                }
            });
        }
        public void ApplyEffects(Champion owner, Unit unit, Spell spell, Projectile projectile)
        {
                if (unit.Team != owner.Team)
                {
                    ApiFunctionManager.AddParticleTarget(owner, "Jinx_W_Tar.troy", unit);
                    var ad = owner.GetStats().AttackDamage.Total * 1.4f;
                    var damage = -40 + spell.Level * 50 + ad;
                    owner.DealDamageTo(unit, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                    projectile.setToRemove();
                }
        }
        public void OnUpdate(double diff) { }

     }
}