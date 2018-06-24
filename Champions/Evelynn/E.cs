using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    class EvelynnE : GameScript
    {
        private BuffGameScriptController RavageBuff;

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            spell.spellAnimation("SPELL3", owner);
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var ad = (owner.GetStats().AttackDamage.Total - owner.GetStats().AttackDamage.BaseValue) * 0.5f;
            var ap = owner.GetStats().AbilityPower.Total * 0.5f;    

            var damage = ((new float[] { 35 , 55 , 75 , 95 , 115 }[spell.Level - 1]) + ad + ap) / 2;

            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);

            RavageBuff = owner.AddBuffGameScript("EveRavage", "EveRavage", spell, 3.0f, true);
        }

        public void OnUpdate(double diff)
        {
        }
    }
}