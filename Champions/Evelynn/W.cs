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
    class EvelynnW : GameScript
    {
        private BuffGameScriptController FrenzyBuff;

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            spell.spellAnimation("SPELL2", owner);
            ApiFunctionManager.AddParticleTarget(owner, "Evelynn_W_cas.troy", owner);
            FrenzyBuff = owner.AddBuffGameScript("EveFrenzy", "EveFrenzy", spell, 3.0f, true);
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
