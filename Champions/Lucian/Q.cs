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
    public class Q : IGameScript
=======
    public class LucianQ : GameScript
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
            var range = to * 1100;
            var trueCoords = current + range;

            spell.AddLaser(trueCoords.X, trueCoords.Y);
            spell.spellAnimation("SPELL1", owner);
            if (owner is Champion)
            {
                ApiFunctionManager.AddParticle(owner as Champion, "Lucian_Q_laser.troy", trueCoords.X, trueCoords.Y);
                ApiFunctionManager.AddParticleTarget(owner as Champion, "Lucian_Q_cas.troy", owner);
            }
        }
        public void ApplyEffects(Unit target, Projectile projectile) {
            var damage = owner.GetStats().AttackDamage.Total * (0.45f + spell.Level * 0.15f) + (50 + spell.Level * 30);
            owner.DealDamageTo(spell.Target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
        }
     }
}