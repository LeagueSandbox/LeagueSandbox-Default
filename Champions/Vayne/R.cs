using System.Collections.Generic;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class VayneInquisition : GameScript
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
            float duration = new float[] { 8.0f, 10.0f, 12.0f }[spell.Level - 1];

            var buff = ((ObjAIBase)target).AddBuffGameScript("FinalHourBuff", "FinalHourBuff", spell, -1, true);

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