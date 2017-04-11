using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Global
{
    public class SummonerFlash : IGameScript
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
        }
        public void OnDeactivate() { }
        public void OnFinishCasting(Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = new Vector2(spell.X, spell.Y) - current;
            Vector2 trueCoords;

            if (to.Length() > 425)
            {
                to = Vector2.Normalize(to);
                var range = to * 425;
                trueCoords = current + range;
            }
            else
            {
                trueCoords = new Vector2(spell.X, spell.Y);
            }

            ApiFunctionManager.TeleportTo(owner, trueCoords.X, trueCoords.Y);
            if (owner is Champion)
            {
            ApiFunctionManager.AddParticle(owner as Champion, "global_ss_flash.troy", owner.X, owner.Y);
            ApiFunctionManager.AddParticleTarget(owner as Champion, "global_ss_flash_02.troy", owner);
            }
        }
    }
}
