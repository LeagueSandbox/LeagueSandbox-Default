using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class EzrealMysticShot : IGameScript
    {
        GameScriptInformation info;
        Spell spell;
        Unit owner;
        public void OnActivate(GameScriptInformation scriptInfo)
        {
            info = scriptInfo;
            spell = info.OwnerSpell;
            owner = info.OwnerUnit;
            //Setup event listeners
            ApiEventManager.OnSpellCast.AddListener(this, spell, OnStartCasting);
            ApiEventManager.OnSpellFinishCast.AddListener(this, spell, OnFinishCasting);
            ApiEventManager.OnSpellApplyEffects.AddListener(this, spell, ApplyEffects);
        }
        public void OnDeactivate() { }
        public void OnStartCasting(Unit target){
            if (owner is Champion) ApiFunctionManager.AddParticleTarget(owner as Champion, "ezreal_bow.troy", owner, 1, "L_HAND");

            owner.AddBuffGameScript("Quickdraw", "Quickdraw", spell);

            ApiFunctionManager.AddBuffHUDVisual("OlafBerzerkerRage", 6.0f, 0, owner);
            ApiFunctionManager.AddBuffHUDVisual("Absolute_Zero", 6.0f, 0, owner);
        }
        public void OnFinishCasting(Unit target) {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 1150;
            var trueCoords = current + range;

            spell.AddProjectile("EzrealMysticShotMissile", trueCoords.X, trueCoords.Y);

            //Create an extra spell after 3 seconds to test
            ApiFunctionManager.LogInfo("Finished casting, creating timer");
            ApiFunctionManager.CreateTimer(3.0f, () => {
                ApiFunctionManager.LogInfo("3 second timer finished, adding another projectile");
                spell.AddProjectile("EzrealMysticShotMissile", trueCoords.X, trueCoords.Y);
            });
        }
        public void ApplyEffects(Unit target, Projectile projectile) {
            var ad = owner.GetStats().AttackDamage.Total * 1.1f;
            var ap = owner.GetStats().AbilityPower.Total * 0.4f;
            var damage = 15 + spell.Level * 20 + ad + ap;
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);
            spell.LowerCooldown(0, 1);
            spell.LowerCooldown(1, 1);
            spell.LowerCooldown(2, 1);
            spell.LowerCooldown(3, 1);
            projectile.setToRemove();
        }
     }
}