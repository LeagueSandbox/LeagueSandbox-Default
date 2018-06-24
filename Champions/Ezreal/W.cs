using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class EzrealEssenceFlux : GameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            ApiFunctionManager.AddParticleTarget(owner, "ezreal_bow_yellow.troy", owner, 1, "L_HAND");
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 1000;
            var trueCoords = current + range;
            spell.AddProjectile("EzrealEssenceFluxMissile", trueCoords.X, trueCoords.Y);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            if (target is Champion)
            {
                if (target.Team == owner.Team)
                {
                    var buff = ((ObjAIBase)target).AddBuffGameScript("EssenceFluxAttackSpeed", "EssenceFluxAttackSpeed", spell);
                    var visualBuff = ApiFunctionManager.AddBuffHUDVisual("EzrealEssenceFluxBuff", 5.0f, 0, owner);
                    ApiFunctionManager.CreateTimer(5.0f, () =>
                    {
                        owner.RemoveBuffGameScript(buff);
                        ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                    });
                }
                else
                {
                    var ap = owner.GetStats().AbilityPower.Total * 0.8f;
                    var damage = (new float[] { 80, 130, 180, 230, 280 }[spell.Level - 1]) + ap;
                    target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                }
            }
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
