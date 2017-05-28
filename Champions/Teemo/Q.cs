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
    public class BlindingDart : IGameScript
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
            ApiEventManager.OnSpellFinishCast.AddListener(this, spell, OnFinishCasting);
            ApiEventManager.OnSpellApplyEffects.AddListener(this, spell, ApplyEffects);
        }
        public void OnDeactivate()
        {

        }
        public void OnFinishCasting(Unit target) {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 580;
            var trueCoords = current + range;

            spell.AddProjectile("ToxicShot", trueCoords.X, trueCoords.Y);
        }
        public void ApplyEffects(Unit target, Projectile projectile)
        {
            var ap = owner.GetStats().AbilityPower.Total * 0.8f;
            var damage = 35 + spell.Level * 45 + ap;
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            float time = 1.25f + 0.25f * spell.Level;
            var buff = target.AddBuffGameScript("Blind", "Blind", spell);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("Blind", time, 1, target);
            ApiFunctionManager.CreateTimer(time, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                target.RemoveBuffGameScript(buff);
            });
        }
     }
}