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
<<<<<<< HEAD
    public class R : IGameScript
=======
    public class LuxMaliceCannon : GameScript
>>>>>>> refs/remotes/origin/indev
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
            ApiEventManager.OnSpellApplyEffects.AddListener(this, spell, ApplyEffects);
        }
        public void OnDeactivate() { }
        public void OnStartCasting(Unit target){
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 3340;
            var trueCoords = current + range;

            spell.AddLaser(trueCoords.X, trueCoords.Y);
            if (owner is Champion) ApiFunctionManager.AddParticle(owner as Champion, "LuxMaliceCannon_beam.troy", trueCoords.X, trueCoords.Y);
            ApiFunctionManager.FaceDirection(owner, trueCoords, false);
            spell.spellAnimation("SPELL4", owner);
            if (owner is Champion) ApiFunctionManager.AddParticleTarget(owner as Champion, "LuxMaliceCannon_cas.troy", owner);
        }
        public void ApplyEffects(Unit target, Projectile projectile) {
            owner.DealDamageTo(spell.Target, 200f + spell.Level * 100f + owner.GetStats().AbilityPower.Total * 0.75f, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
        }
     }
}