using System.Numerics;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class BlindingDart : IGameScript
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
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - owner.Position);
            var range = to * 580;
            var trueCoords = owner.Position + range;

            spell.AddProjectile("ToxicShot", trueCoords.X, trueCoords.Y);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var ap = owner.Stats.AbilityPower.Total * 0.8f;
            var damage = 35 + spell.Level * 45 + ap;
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            var time = 1.25f + 0.25f * spell.Level;
            ((ObjAiBase) target).AddBuffGameScript("Blind", "Blind", spell, time);
            ApiFunctionManager.AddBuffHudVisual("Blind", time, 1, BuffType.COMBAT_DEHANCER, (ObjAiBase) target, time);
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
