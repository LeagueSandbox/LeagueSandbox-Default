using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
namespace Global
{
    public static class SummonerHeal
    {
        public static void OnStartCasting(Champion owner, Spell spell, Unit target)
        {

        }

        public static void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var units = ApiFunctionManager.GetChampionsInRange(owner, 850, true);

            Champion mostWoundedAlliedChampion = null;

            float lowestHealthPercentage = 100;
            float maxHealth;
            float newHealth;

            for (var i = 0; i <= units.Count - 1; i++)
            {
                var value = units[i];

                if (owner.Team == value.Team && i != 0)
                {
                    var currentHealth = value.GetStats().CurrentHealth;
                    maxHealth = value.GetStats().HealthPoints.Total;

                    if (currentHealth * 100 / maxHealth < lowestHealthPercentage && owner != value)
                    {
                        lowestHealthPercentage = currentHealth * 100 / maxHealth;
                        mostWoundedAlliedChampion = value;

                    }
                }
            }


            if (mostWoundedAlliedChampion != null)
            {
                newHealth = mostWoundedAlliedChampion.GetStats().CurrentHealth + 75 + owner.GetStats().GetLevel() * 15;
                maxHealth = mostWoundedAlliedChampion.GetStats().HealthPoints.Total;

                if (newHealth >= maxHealth)
                {
                    mostWoundedAlliedChampion.GetStats().CurrentHealth = maxHealth;
                }
                else
                {
                    mostWoundedAlliedChampion.GetStats().CurrentHealth = newHealth;
                }

                //local buff = Buff.new("Haste", 1.0, mostWoundedAlliedChampion, owner)
                //buff: setMovementSpeedPercentModifier(30)
                //addBuff(buff)

                ApiFunctionManager.AddParticleTarget(mostWoundedAlliedChampion, "global_ss_heal_02.troy", mostWoundedAlliedChampion);
                ApiFunctionManager.AddParticleTarget(mostWoundedAlliedChampion, "global_ss_heal_speedboost.troy", mostWoundedAlliedChampion);
            }

            newHealth = owner.GetStats().CurrentHealth + 75 + owner.GetStats().GetLevel() * 15;
            maxHealth = owner.GetStats().HealthPoints.Total;

            if (newHealth >= maxHealth)
            {
                owner.GetStats().CurrentHealth = maxHealth;
            }
            else
            {
                owner.GetStats().CurrentHealth = newHealth;
            }

            //local buff2 = Buff.new("Haste", 1.0, owner, owner)
            //buff2: setMovementSpeedPercentModifier(30)
            //addBuff(buff2)
            
            ApiFunctionManager.AddParticleTarget(owner, "global_ss_heal.troy", owner);
            ApiFunctionManager.AddParticleTarget(owner, "global_ss_heal_speedboost.troy", owner);
        }

        public static void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {

        }

        public static void OnUpdate(double diff)
        {

        }
    }
}
