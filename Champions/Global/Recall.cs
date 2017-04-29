using LeagueSandbox.GameServer.Core.Logic.PacketHandlers;
using LeagueSandbox.GameServer.Logic;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Enet;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Packets;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System.Collections.Generic;

namespace Spells
{
    public class Recall : IGameScript
    {
<<<<<<< HEAD
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
=======

        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {

>>>>>>> refs/remotes/origin/indev
        }
        public void OnDeactivate() { }
        public void OnFinishCasting(Unit target)
        {
<<<<<<< HEAD
            //addBuff("Recall", 8, owner, owner)
            if (owner is Champion) ApiFunctionManager.AddParticleTarget(owner as Champion, "TeleportHome.troy", owner);
=======
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("Recall", 8.0f, 1, owner);
            var AddParticle = ApiFunctionManager.AddParticleTarget(owner, "TeleportHome.troy", owner);
            ApiFunctionManager.CreateTimer(8.0f, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                owner.Recall(owner);
            });
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {

        }
        public void OnUpdate(double diff)
        {

        }

        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
>>>>>>> refs/remotes/origin/indev
        }
    }
}
