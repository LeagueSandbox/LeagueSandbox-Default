Vector2 = require 'Vector2' -- include 2d vector lib 

function onStartCasting()      
end

function onFinishCasting()
	--addBuff("Recall", 8, owner, owner)
	addParticleTarget(owner, "TeleportHome.troy", owner)
end

function onStartChanneling()
  printChat("StartChanneling")
end

function onFinishChanneling()             
  printChat("FinishChanneling")
  local teamBlue = Vector2:new(26, 280)       
  local teamPurple = Vector2:new(13927, 14175)      
  teleport(owner, teamBlue.x, teamBlue.y)
end

function applyEffects()
end

function onUpdate(diff)
end
