using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;

namespace LeagueSandbox_Default.Champions.Annie
{
    public class Incinerate : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 625;
            var trueCoords = current + range;

            spell.AddCone("Incinerate", trueCoords.X, trueCoords.Y, 24.76f);
            ApiFunctionManager.AddParticle(owner, "IIncinerate_buf.troy", trueCoords.X, trueCoords.Y);
            ApiFunctionManager.FaceDirection(owner, trueCoords, false);
            spell.SpellAnimation("SPELL2", owner);
            ApiFunctionManager.AddParticleTarget(owner, "Incinerate_cas.troy", owner);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var ap = owner.Stats.AbilityPower.Total * 0.8f;
            var damage = 70 + spell.Level * 45 + ap;

            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);

        }

        public void OnUpdate(double diff)
        {
        }
    }
}
