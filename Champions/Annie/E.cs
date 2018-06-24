using System.Collections.Generic;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;


namespace Spells
{
    public class MoltenShield : GameScript
    {

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
            float duration = new float[] { 5.0f, 6.0f, 8.0f }[spell.Level - 1];

            var buff = ((ObjAIBase)target).AddBuffGameScript("MoltenShield", "MoltenShield", spell, -1, true);

            ApiFunctionManager.CreateTimer(duration, () =>
            {
                owner.RemoveBuffGameScript(buff);
            });

        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {

        }

        public void OnUpdate(double diff)
        {
        }
    }
}

