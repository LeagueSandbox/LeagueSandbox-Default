using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class BlindingDart : GameScript
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
            var range = to * 580;
            var trueCoords = current + range;

            spell.AddProjectile("ToxicShot", trueCoords.X, trueCoords.Y);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var ap = owner.GetStats().AbilityPower.Total * 0.8f;
            var damage = 35 + spell.Level * 45 + ap;
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            float time = 1.25f + 0.25f * spell.Level;
            var buff = ((ObjAIBase) target).AddBuffGameScript("Blind", "Blind", spell);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("Blind", time, 1, (ObjAIBase) target);
            ApiFunctionManager.CreateTimer(time, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                ((ObjAIBase) target).RemoveBuffGameScript(buff);
            });
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
