Vector2 = require 'Vector2' -- include 2d vector lib 

function onStartCasting()   

end

function onFinishCasting()
    addProjectileTarget("Disintegrate", castTarget, false)
end

function applyEffects()
    local AP = owner:GetStats().AbilityPower.Total*0.8
    local damage = 45 + (spellLevel*35) + AP

    if ( not ( castTarget == 0 ) ) and ( not isDead( castTarget ) ) then
        dealMagicalDamage(damage)
        if isDead(castTarget) then
            -- Mana recover
            lowerCooldown(0, totalCooldown / 2)
            local manaToRecover = 55 + (spellLevel*5)
            local newMana = owner:GetStats().CurrentMana + manaToRecover
            local maxMana = owner:GetStats().ManaPoints.Total
            if newMana >= maxMana then
                owner:GetStats().CurrentMana = maxMana
            else
                owner:GetStats().CurrentMana = newMana
            end
        end
    end
    destroyProjectile()
end

function onUpdate(diff)

end
