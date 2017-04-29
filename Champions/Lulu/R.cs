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
    public class LuluR : GameScript
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
        }
        public void OnDeactivate() { }
        public void OnStartCasting(Unit target)
        {
            Particle p = null;
            if (owner is Champion)
            {
                p = ApiFunctionManager.AddParticleTarget(owner as Champion, "Lulu_R_cas.troy", target, 1);
            }
            var buff = target.AddBuffGameScript("LuluR", "LuluR", spell);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("LuluR", 7.0f, 1, owner);
            ApiFunctionManager.CreateTimer(7.0f, () =>
            {
                if (p != null) ApiFunctionManager.RemoveParticle(p);
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                owner.RemoveBuffGameScript(buff);
                if (owner is Champion) ApiFunctionManager.AddParticleTarget(owner as Champion, "Lulu_R_expire.troy", target, 1);
            });

        }
     }
}