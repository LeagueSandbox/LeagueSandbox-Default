using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Global
{
    public class Recall : IGameScript
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
            //addBuff("Recall", 8, owner, owner)
            if (owner is Champion) ApiFunctionManager.AddParticleTarget(owner as Champion, "TeleportHome.troy", owner);
        }
    }
}
