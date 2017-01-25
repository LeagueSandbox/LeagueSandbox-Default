using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
namespace Global
{
    public static class SummonerFlash
    {
        public static void onStartCasting(Champion owner, Spell spell, Unit target)
        {

        }
        public static void onFinishCasting(Champion owner, Spell spell, Unit target)
        {
            Vector2 current = new Vector2(owner.X, owner.Y);
            Vector2 to = new Vector2(spell.X, spell.Y) - current;
            Vector2 trueCoords;


            if (to.Length() > 425) {
                to = Vector2.Normalize(to);
                Vector2 range = to * 425;
                trueCoords = current + range;
            } else {
                trueCoords = new Vector2(spell.X, spell.Y);
            }

            ApiFunctionManager.AddParticle(owner, "global_ss_flash.troy", owner.X, owner.Y);
            ApiFunctionManager.TeleportTo(owner, trueCoords.X, trueCoords.Y);

            ApiFunctionManager.AddParticleTarget(owner, "global_ss_flash_02.troy", owner);
        }
        public static void applyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {

        }
        public static void onUpdate(double diff)
        {

        }
    }
}
