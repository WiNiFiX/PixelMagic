﻿namespace PixelMagic.Helpers
{
    public static class Addon
    {
        public static string LuaContents = @"
-- Configurable Variables
local size = 5;	-- this is the size of the ""pixels"" at the top of the screen that will show stuff, currently 5x5 because its easier to see and debug with

-- Actual Addon Code below
local f = CreateFrame(""frame"")
f:SetSize(5 * size, size);  -- Width, Height
f:SetPoint(""TOPLEFT"", 0, 0)
f:RegisterEvent(""ADDON_LOADED"")

local hpframes = {}
local cooldownframes = {}
local updateSpellChargesFrame = {}
local buffFrames = {}
local targetDebuffFrames = {}
local spellInRangeFrames = {}
local healthFrames = {}
local targetHealthFrames = {}
local isTargetFriendlyFrame = nil
local hasTargetFrame = nil
local powerFrames = {}
local playerIsCastingFrame = nil
local targetIsCastingFrame = nil
local unitIsVisibleFrame = nil

local runePrev = 0
local ssPrev = 0
local ccPrev = 0
local hpPrev = 0

local lastCooldownState = {}
local lastSpellChargeCharges = {}
local lastBuffState = {}
local lastDebuffState = {}

local function updateHolyPower()
	local power = UnitPower(""player"", 9)
	
	if power ~= hpPrev then	
		print(""Holy Power: "" .. power)   
	   
		local i = 1

		while i <= power do
			hpframes[i].t:SetColorTexture(1, 0, 0, 1)
			hpframes[i].t:SetAllPoints(false)
			i = 1 + i
		end
		
		while i <= 5 do
			hpframes[i].t:SetColorTexture(0, 1, 1, 1)
			hpframes[i].t:SetAllPoints(false)
			i = 1 + i
		end
		
		hpPrev = power
	 end
 end

local function updateComboPoints()
    local power = UnitPower(""player"", 4)

    if power ~= ccPrev then	
        local i = 1

        print(""Combo Points: "" .. power)

        while i <= power do                             -- update all power frames to red (this should update all 8, need to confirm)
            hpframes[i].t:SetColorTexture(1, 0, 0, 1)
            hpframes[i].t:SetAllPoints(false)
            i = 1 + i
        end		
    end
  
    while i <= 8 do                                     -- mark the remaining frames in color white
        hpframes[i].t:SetColorTexture(1, 1, 0, 1)
        hpframes[i].t:SetAllPoints(false)
        i = 1 + i
    end    

    ccPrev = power
end

local function updateSoulShards()
    local power = UnitPower(""player"", 7)
	
    if power ~= ssPrev then	    
        local i = 1

        while i <= power do
            hpframes[i].t:SetColorTexture(1, 0, 0, 1)
            hpframes[i].t:SetAllPoints(false)
            i = 1 + i
        end
		    
        while i <= 5 do
            hpframes[i].t:SetColorTexture(1, 1, 0, 1)
            hpframes[i].t:SetAllPoints(false)
            i = 1 + i
        end

        ssPrev = power
    end
end

local function updateRunes()
    local CurrentMaxRunes = UnitPower(""player"", SPELL_POWER_RUNES);
    local selRune = 6
    local i = 1
    for i = 1, CurrentMaxRunes do
        local RuneReady = select(3, GetRuneCooldown(i))
        if not RuneReady then
            selRune = selRune - 1
        end
    end
    if selRune ~= runePrev then	

        while i <= selRune do
            hpframes[i].t:SetColorTexture(1, 0, 0, 1)
            hpframes[i].t:SetAllPoints(false)
            i = 1 + i
        end
    
        while i <= 6 do
            hpframes[i].t:SetColorTexture(1, 1, 1, 1)
            hpframes[i].t:SetAllPoints(false)
            i = i + 1
		end

        runePrev = selRune     
    end
end

local function updateSpellCooldowns() 
	for _, spellId in pairs(cooldowns) do
		-- start is the value of GetTime() at the point the spell began cooling down
		-- duration is the total duration of the cooldown, NOT the remaining
		local start, duration, _ = GetSpellCooldown(spellId)
		if duration ~= 0 then -- the spell is not ready to be cast
			if (lastCooldownState[spellId] ~= ""onCD"") then										 
				--print(""Spell with Id = "" .. spellId .. "" is on CD"")
					
				cooldownframes[spellId].t:SetColorTexture(1, 0, 0, 1)
				cooldownframes[spellId].t:SetAllPoints(false)
					
				lastCooldownState[spellId] = ""onCD""
			end				
		else
			if (lastCooldownState[spellId] ~= ""offCD"") then
				--print(""Spell with Id = "" .. spellId .. "" is off CD and can be cast"")
					
				cooldownframes[spellId].t:SetColorTexture(1, 1, 1, 1)
				cooldownframes[spellId].t:SetAllPoints(false)
					
				lastCooldownState[spellId] = ""offCD""
			end
		end						
	end
end

local function updateSpellCharges() 
	for _, spellId in pairs(cooldowns) do
        charges, maxCharges, start, duration = GetSpellCharges(spellId)

        if (lastSpellChargeCharges[spellId] ~= charges) then
            local green = 0             
            local strcount = ""0.0"" .. charges;
                    
            if (charges >= 10) then
                strcount = ""0."" .. charges;
            end
            green = tonumber(strcount)

            print(""Spell with Id = "" .. spellId .. "" has charges: "" .. charges .. "" Green = "" .. green)

            updateSpellChargesFrame[spellId].t:SetColorTexture(0, green, 0, 1)
		    updateSpellChargesFrame[spellId].t:SetAllPoints(false)
		    		
		    lastSpellChargeCharges[spellId] = charges		
        end        
	end
end

local function updateMyBuffs() 
	for _, auraId in pairs(buffs) do
        local buff = ""UnitBuff"";
		local auraName = GetSpellInfo(auraId)
		
		if auraName == nil then
			if (lastBuffState[auraId] ~= ""BuffOff"") then
                buffFrames[auraId].t:SetColorTexture(1, 1, 1, 1)
                buffFrames[auraId].t:SetAllPoints(false)
                lastBuffState[auraId] = ""BuffOff""
                --print(""["" .. buff .. ""] "" .. auraName.. "" Off"")
            end
			return
		end
		
		local name, rank, icon, count, debuffType, duration, expirationTime, unitCaster, isStealable, shouldConsolidate, spellId = UnitBuff(""player"", auraName)		
		
		if (name == auraName) then -- We have Aura up and Aura ID is matching our list					
			if (lastBuffState[auraId] ~= ""BuffOn"" .. count) then
                local green = 0             
                local strcount = ""0.0"" .. count;
                
                if (count >= 10) then
                    strcount = ""0."" .. count;
                end
                green = tonumber(strcount)
                buffFrames[auraId].t:SetColorTexture(0, green, 0, 1)
				buffFrames[auraId].t:SetAllPoints(false)
                --print(""["" .. buff .. ""] "" .. auraName.. "" "" .. count .. "" Green: "" .. green)
                lastBuffState[auraId] = ""BuffOn"" .. count 
            end
        else
            if (lastBuffState[auraId] ~= ""BuffOff"") then
                buffFrames[auraId].t:SetColorTexture(1, 1, 1, 1)
                buffFrames[auraId].t:SetAllPoints(false)
                lastBuffState[auraId] = ""BuffOff""
                --print(""["" .. buff .. ""] "" .. auraName.. "" Off"")
            end
        end
    end
end

local function updateTargetDebuffs() 
	for _, auraId in pairs(debuffs) do
        local buff = ""UnitDebuff"";
		local auraName = GetSpellInfo(auraId)

        if auraName == nil then
            if (lastDebuffState[auraId] ~= ""DebuffOff"") then
                targetDebuffFrames[auraId].t:SetColorTexture(1, 1, 1, 1)
                targetDebuffFrames[auraId].t:SetAllPoints(false)
                lastDebuffState[auraId] = ""DebuffOff""               
            end
    
            return
        end
        
		--print(""Getting debuff for Id = "" .. auraName)
		
        local name, rank, icon, count, debuffType, duration, expirationTime, unitCaster, isStealable, shouldConsolidate, spellId, canApplyAura, isBossDebuff, value1, value2, value3 = UnitDebuff(""target"", auraName)		        

		if (name == auraName) then -- We have Aura up and Aura ID is matching our list					
            local getTime = GetTime()
            local remainingTime = math.floor(expirationTime - getTime + 0.5) 	

			if (lastDebuffState[auraId] ~= ""DebuffOn"" .. count .. remainingTime) then
                local green = 0
                local blue = 0             
                local strcount = ""0.0"" .. count;
                local strbluecount = ""0.0"" .. remainingTime;
                
                if (count >= 10) then
                    strcount = ""0."" .. count;
                end

                if(remainingTime >= 10) then
                   strbluecount = ""0."" .. remainingTime
                end

                green = tonumber(strcount)
                blue = tonumber(strbluecount)

                targetDebuffFrames[auraId].t:SetColorTexture(0, green, blue, 1)
				targetDebuffFrames[auraId].t:SetAllPoints(false)
                --print(""["" .. buff .. ""] "" .. auraName.. "" "" .. count .. "" Green: "" .. green .. "" Blue: "" .. blue)
                lastDebuffState[auraId] = ""DebuffOn"" .. count .. remainingTime
            end
        else
            if (lastDebuffState[auraId] ~= ""DebuffOff"") then
                targetDebuffFrames[auraId].t:SetColorTexture(1, 1, 1, 1)
                targetDebuffFrames[auraId].t:SetAllPoints(false)
                lastDebuffState[auraId] = ""DebuffOff""
                --print(""["" .. buff .. ""] "" .. auraName.. "" Off"")
            end
        end
    end
end

local lastSpellInRange = {}

local function updateSpellInRangeFrames() 
	for _, spellId in pairs(cooldowns) do		
		local inRange = 0		
		local name, rank, icon, castTime, minRange, maxRange = GetSpellInfo(spellId)
		
		if (name == nil) then		
			inRange = 0		
		else
			-- http://wowwiki.wikia.com/wiki/API_IsSpellInRange	
			inRange = IsSpellInRange(name, ""target"")  -- '0' if out of range, '1' if in range, or 'nil' if the unit is invalid.	
		end
								
		if lastSpellInRange[spellId] ~= inRange then
			if (inRange == 1) then
				spellInRangeFrames[spellId].t:SetColorTexture(1, 0, 0, 1)
			else
				spellInRangeFrames[spellId].t:SetColorTexture(1, 1, 1, 1)
			end 
			spellInRangeFrames[spellId].t:SetAllPoints(false)
			
			--print(""Spell: "" .. name .. "" InRange = "" .. inRange)
			
			lastSpellInRange[spellId] = inRange	
		end				
	end
end

function healthToBinary(num)
	local bits = 7

    -- returns a table of bits
    local t={} -- will contain the bits
    for b=bits,1,-1 do
        rest=math.fmod(num,2)
        t[b]=rest
        num=(num-rest)/2
    end
	
	return table.concat(t)
end

local lastHealth = 0

local function updateHealth()
	local health = UnitHealth(""player"");		
	local maxHealth = UnitHealthMax(""player"");
	local percHealth = ceil((health / maxHealth) * 100)
	
	if (percHealth ~= lastHealth) then		
		local binaryHealth = healthToBinary(percHealth)
		--print (""Health = "" .. percHealth .. "" binary = "".. binaryHealth)
		
		for i = 1, string.len(binaryHealth) do
			local currentBit = string.sub(binaryHealth, i, i)
			
			if (currentBit == ""1"") then
				healthFrames[i].t:SetColorTexture(1, 0, 0, 1)
			else
				healthFrames[i].t:SetColorTexture(1, 1, 1, 1)
			end
			healthFrames[i].t:SetAllPoints(false)
		end
		
		lastHealth = percHealth
	end
end

local lastTargetHealth = 0

local function updateTargetHealth()
	local guid = UnitGUID(""target"")

    local health = 0		
    local maxHealth = 100
    local percHealth = 0

    if (guid ~= nil) then
	    health = UnitHealth(""target"");		
	    maxHealth = UnitHealthMax(""target"");
	    percHealth = ceil((health / maxHealth) * 100)
    end	
	
	if (percHealth ~= lastTargetHealth) then		
		local binaryHealth = healthToBinary(percHealth)
		--print (""Target Health = "" .. percHealth .. "" binary = "".. binaryHealth)
		
		for i = 1, string.len(binaryHealth) do
			local currentBit = string.sub(binaryHealth, i, i)
			
			if (currentBit == ""1"") then
				targetHealthFrames[i].t:SetColorTexture(0, 0, 1, 1)
			else
				targetHealthFrames[i].t:SetColorTexture(1, 1, 1, 1)
			end
			targetHealthFrames[i].t:SetAllPoints(false)
		end
		
		lastTargetHealth = percHealth
	end
end

local lastPower = 0
local playerClass, englishClass, classIndex = UnitClass(""player"");
local currentSpec = GetSpecialization()
local currentSpecId = currentSpec and select(1, GetSpecializationInfo(currentSpec)) or 0

local function updatePower()
	local power = UnitPower(""player"");		
	local maxPower = UnitPowerMax(""player"");
	
	-- For testing 120 we will hardcode to add 20 to current power since we testing on a hunter
	-- power = power + 20
	
	if (power ~= lastPower) then
		lastPower = power
			
		-- If the class uses mana, then we need to calculate percent mana to show its power
		-- http://wowwiki.wikia.com/wiki/API_UnitClass
		-- http://wowwiki.wikia.com/wiki/SpecializationID
		if (
			(classIndex == 7)  -- Shaman   
		 or (classIndex == 2)  -- Paladin
		 or (classIndex == 5)  -- Priest 
		 or (classIndex == 8)  -- Mage
		 or (classIndex == 9)  -- Lock
		 or ((classIndex == 11) and (currentSpecId == 102)) -- Druid Balance
		 or ((classIndex == 11) and (currentSpecId == 105)) -- Druid Resto 
		   ) 
		then 
			power = ceil((power / maxPower) * 100)
		end
		
		local binaryPower = healthToBinary(power)			
		--print (""Power = "" .. power .. "" binary = "".. binaryPower)	
		--print (""Current Spec = "" .. currentSpecId)
		
		for i = 1, string.len(binaryPower) do
			local currentBit = string.sub(binaryPower, i, i)
			
			if (currentBit == ""1"") then
				powerFrames[i].t:SetColorTexture(0, 1, 0, 1)
			else
				powerFrames[i].t:SetColorTexture(1, 1, 1, 1)
			end
			powerFrames[i].t:SetAllPoints(false)
		end	
	end
end
 
local lastIsFriend = true 
 
local function updateIsFriendly()
	isFriend = UnitIsFriend(""player"",""target"");
	
	if (isFriend ~= lastIsFriend) then
	
		if (isFriend == true) then
			--print (""Unit is friendly: True"")
			
			isTargetFriendlyFrame.t:SetColorTexture(0, 1, 0, 1)
		else
			--print (""Unit is friendly: False"")
			
			isTargetFriendlyFrame.t:SetColorTexture(0, 0, 1, 1)
		end
	
		lastIsFriend = isFriend
	end
end

local lastTargetGUID = """"

local function hasTarget()
	guid = UnitGUID(""target"")
		
	if (guid ~= lastTargetGUID) then
		if (guid == nil) then
			--print (""Target GUID: None"" )	
			
			hasTargetFrame.t:SetColorTexture(0, 0, 0, 1)
		else			
			--print (""Target GUID: "" .. guid )	
			
			hasTargetFrame.t:SetColorTexture(1, 0, 0, 1)
		end
			
		lastTargetGUID = guid		
	end
end

local lastCastID = 0

local function updatePlayerIsCasting()
	spell, rank, displayName, icon, startTime, endTime, isTradeSkill, castID, interrupt = UnitCastingInfo(""player"")
	
	if castID ~= nil then	
		if castID ~= lastCastID then
			--print(""Casting spell: "" .. spell)
		
			playerIsCastingFrame.t:SetColorTexture(1, 0, 0, 1)
		
			lastCastID = castID		
		end
	else
		if castID ~= lastCastID then
			--print(""Not casting"")
			
			playerIsCastingFrame.t:SetColorTexture(1, 1, 1, 1)
			
			lastCastID = castID		
		end	
	end		
end

local lastTargetCastID = 0

local function updateTargetIsCasting()	
	spell, rank, displayName, icon, startTime, endTime, isTradeSkill, castID, interrupt = UnitCastingInfo(""target"")
		
	if castID ~= nil then	
		if castID ~= lastTargetCastID then
			--print(""Casting spell: "" .. spell)
		
			targetIsCastingFrame.t:SetColorTexture(1, 0, 0, 1)
		
			lastTargetCastID = castID		
		end
	else
		if castID ~= lastTargetCastID then
			--print(""Not casting"")
			
			targetIsCastingFrame.t:SetColorTexture(1, 1, 1, 1)
			
			lastTargetCastID = castID		
		end	
	end
end

local lastVis = nil

local function updateUnitIsVisible()	
	local vis = UnitIsVisible(""target"")
		
	if vis == nil then			
        if (vis ~= lastVis) then
		    --print(""Target Is Not Visible"")		

	        unitIsVisibleFrame.t:SetColorTexture(1, 1, 1, 1)
		    lastVis = vis				
        end
	else
		if vis ~= lastVis then
			--print(""Target Is Visible"")			

			unitIsVisibleFrame.t:SetColorTexture(1, 0, 0, 1)			
			lastVis = vis		
		end	
	end
end
 
local function initFrames()
    local playerClass, englishClass, classIndex = UnitClass(""player"");
    local i = 0

	print (""Initialising Health Frames"")
	for i = 1, 7 do
		healthFrames[i] = CreateFrame(""frame"")
		healthFrames[i]:SetSize(size, size)
		healthFrames[i]:SetPoint(""TOPLEFT"", (i - 1) * size, 0)                -- column 1 - 7, row 1
		healthFrames[i].t = healthFrames[i]:CreateTexture()        
		healthFrames[i].t:SetColorTexture(1, 1, 1, 1)
		healthFrames[i].t:SetAllPoints(healthFrames[i])
		healthFrames[i]:Show()		
		
		healthFrames[i]:SetScript(""OnUpdate"", updateHealth)
	end

	-- Power can go above 100, it can be 120 maximum to my knowledge
	print (""Initialising Power Frames (Rage, Energy, etc...)"")  
    local start = 7
	for i = 8, 14 do
		powerFrames[i-start] = CreateFrame(""frame"")
		powerFrames[i-start]:SetSize(size, size)
		powerFrames[i-start]:SetPoint(""TOPLEFT"", (i - 1) * size, 0)           -- column 8-15, row 1
		powerFrames[i-start].t = powerFrames[i-start]:CreateTexture()        
		powerFrames[i-start].t:SetColorTexture(1, 1, 1, 1)
		powerFrames[i-start].t:SetAllPoints(powerFrames[i-start])
		powerFrames[i-start]:Show()		
		
		powerFrames[i-start]:SetScript(""OnUpdate"", updatePower)
	end

	print (""Initialising Target Health Frames"")
    start = 14
	for i = 15, 22 do
		targetHealthFrames[i-start] = CreateFrame(""frame"")
		targetHealthFrames[i-start]:SetSize(size, size)
		targetHealthFrames[i-start]:SetPoint(""TOPLEFT"", (i - 1) * size, 0)    -- column 16 - 23, row 1        
		targetHealthFrames[i-start].t = targetHealthFrames[i-start]:CreateTexture()        
		targetHealthFrames[i-start].t:SetColorTexture(1, 1, 1, 1)
		targetHealthFrames[i-start].t:SetAllPoints(targetHealthFrames[i-start])
		targetHealthFrames[i-start]:Show()		
		
		targetHealthFrames[i-start]:SetScript(""OnUpdate"", updateTargetHealth)
	end
	
	print (""Initialising Spell Cooldown Frames"")
	i = 1
	for _, spellId in pairs(cooldowns) do	
		cooldownframes[spellId] = CreateFrame(""frame"")
		cooldownframes[spellId]:SetSize(size, size)
		cooldownframes[spellId]:SetPoint(""TOPLEFT"", (i - 1) * size, -size)          -- column 1+, row 2
		cooldownframes[spellId].t = cooldownframes[spellId]:CreateTexture()        
		cooldownframes[spellId].t:SetColorTexture(1, 1, 1, 1)
		cooldownframes[spellId].t:SetAllPoints(cooldownframes[spellId])
		cooldownframes[spellId]:Show()
		               
		cooldownframes[spellId]:SetScript(""OnUpdate"", updateSpellCooldowns)
		i = i + 1
	end

	print (""Initialising Spell Charges Frames"")
	i = 1
	for _, spellId in pairs(cooldowns) do	
		updateSpellChargesFrame[spellId] = CreateFrame(""frame"")
		updateSpellChargesFrame[spellId]:SetSize(size, size)
		updateSpellChargesFrame[spellId]:SetPoint(""TOPLEFT"", (i - 1) * size, -size * 8)          -- column 1+, row 9
		updateSpellChargesFrame[spellId].t = updateSpellChargesFrame[spellId]:CreateTexture()        
		updateSpellChargesFrame[spellId].t:SetColorTexture(1, 1, 1, 1)
		updateSpellChargesFrame[spellId].t:SetAllPoints(updateSpellChargesFrame[spellId])
		updateSpellChargesFrame[spellId]:Show()
		               
		updateSpellChargesFrame[spellId]:SetScript(""OnUpdate"", updateSpellCharges)
		i = i + 1
	end

    if classIndex == 2 then                                 -- Paladin
        print (""Initialising Holy Power Frames"")
	    for i = 1, 5 do
		    hpframes[i] = CreateFrame(""frame"");
		    hpframes[i]:SetSize(size, size)
		    hpframes[i]:SetPoint(""TOPLEFT"", i * size - 5, -size * 6)          -- column 1 - 5, row 7
		    hpframes[i].t = hpframes[i]:CreateTexture()        
		    hpframes[i].t:SetColorTexture(0, 1, 1, 1)
		    hpframes[i].t:SetAllPoints(hpframes[i])
		    hpframes[i]:Show()
		
		    hpframes[i]:SetScript(""OnUpdate"", updateHolyPower)
	    end
    end

    if classIndex == 9 then                                 -- Warlock
        print (""Initialising Soulshard Frames"")
	    for i = 1, 5 do
		    hpframes[i] = CreateFrame(""frame"");
		    hpframes[i]:SetSize(size, size)
		    hpframes[i]:SetPoint(""TOPLEFT"", i * size - 5, -size * 6)          -- column 1 - 5, row 7
		    hpframes[i].t = hpframes[i]:CreateTexture()        
		    hpframes[i].t:SetColorTexture(0, 1, 1, 1)
		    hpframes[i].t:SetAllPoints(hpframes[i])
		    hpframes[i]:Show()
		
		    hpframes[i]:SetScript(""OnUpdate"", updateSoulShards)
	    end
    end

    if classIndex == 4 or classIndex == 11 then                                 -- Rogue or Druid Feral
        print (""Initialising Combo Point Frames - Class Index = "" .. classIndex)
	    for i = 1, 8 do
		    hpframes[i] = CreateFrame(""frame"");
		    hpframes[i]:SetSize(size, size)
		    hpframes[i]:SetPoint(""TOPLEFT"", i * size - 5, -size * 6)          -- column 1 - 5, row 7
		    hpframes[i].t = hpframes[i]:CreateTexture()        
		    hpframes[i].t:SetColorTexture(0, 1, 1, 1)
		    hpframes[i].t:SetAllPoints(hpframes[i])
		    hpframes[i]:Show()
		
		    hpframes[i]:SetScript(""OnUpdate"", updateComboPoints)
	    end
    end

    if classIndex == 6 then                                 -- DeathKnight
        print (""Initialising Runes Frames"")
	    for i = 1, 6 do
		    hpframes[i] = CreateFrame(""frame"");
		    hpframes[i]:SetSize(size, size)
		    hpframes[i]:SetPoint(""TOPLEFT"", i * size - 5, -size * 6)          -- column 1 - 6, row 7
		    hpframes[i].t = hpframes[i]:CreateTexture()        
		    hpframes[i].t:SetColorTexture(0, 1, 1, 1)
		    hpframes[i].t:SetAllPoints(hpframes[i])
		    hpframes[i]:Show()	
		    hpframes[i]:SetScript(""OnUpdate"", updateRunes)
	    end
    end
	
	print (""Initialising Spell In Range Frames"")
	local i = 0
	for _, spellId in pairs(cooldowns) do	
		spellInRangeFrames[spellId] = CreateFrame(""frame"")
		spellInRangeFrames[spellId]:SetSize(size, size)
		spellInRangeFrames[spellId]:SetPoint(""TOPLEFT"", i * size, -size * 5)  -- entire row 6
		spellInRangeFrames[spellId].t = spellInRangeFrames[spellId]:CreateTexture()        
		spellInRangeFrames[spellId].t:SetColorTexture(1, 1, 1, 1)
		spellInRangeFrames[spellId].t:SetAllPoints(spellInRangeFrames[spellId])
		spellInRangeFrames[spellId]:Show()
		               
		spellInRangeFrames[spellId]:SetScript(""OnUpdate"", updateSpellInRangeFrames)
		i = i + 1
	end
	
	print (""Initialising IsTargetFriendly Frame"")
	isTargetFriendlyFrame = CreateFrame(""frame"");
	isTargetFriendlyFrame:SetSize(size, size);
	isTargetFriendlyFrame:SetPoint(""TOPLEFT"", 0, -(size * 2))                 -- column 1 row 3
	isTargetFriendlyFrame.t = isTargetFriendlyFrame:CreateTexture()        
	isTargetFriendlyFrame.t:SetColorTexture(0, 1, 0, 1)
	isTargetFriendlyFrame.t:SetAllPoints(isTargetFriendlyFrame)
	isTargetFriendlyFrame:Show()		
		
	isTargetFriendlyFrame:SetScript(""OnUpdate"", updateIsFriendly)
	
	print (""Initialising HasTarget Frame"")
	hasTargetFrame = CreateFrame(""frame"");
	hasTargetFrame:SetSize(size, size);
	hasTargetFrame:SetPoint(""TOPLEFT"", size, -(size * 2))                     -- column 2 row 3
	hasTargetFrame.t = hasTargetFrame:CreateTexture()        
	hasTargetFrame.t:SetColorTexture(0, 1, 0, 1)
	hasTargetFrame.t:SetAllPoints(hasTargetFrame)
	hasTargetFrame:Show()		
		
	hasTargetFrame:SetScript(""OnUpdate"", hasTarget)
	
	print (""Initialising PlayerIsCasting Frame"")
	playerIsCastingFrame = CreateFrame(""frame"");
	playerIsCastingFrame:SetSize(size, size);
	playerIsCastingFrame:SetPoint(""TOPLEFT"", size * 2, -(size * 2))           -- column 3 row 3
	playerIsCastingFrame.t = playerIsCastingFrame:CreateTexture()        
	playerIsCastingFrame.t:SetColorTexture(1, 1, 1, 1)
	playerIsCastingFrame.t:SetAllPoints(playerIsCastingFrame)
	playerIsCastingFrame:Show()		
		
	playerIsCastingFrame:SetScript(""OnUpdate"", updatePlayerIsCasting)
	
	print (""Initialising TargetIsCasting Frame"")
	targetIsCastingFrame = CreateFrame(""frame"");
	targetIsCastingFrame:SetSize(size, size);
	targetIsCastingFrame:SetPoint(""TOPLEFT"", size * 3, -(size * 2))           -- column 4 row 3
	targetIsCastingFrame.t = targetIsCastingFrame:CreateTexture()        
	targetIsCastingFrame.t:SetColorTexture(1, 1, 1, 1)
	targetIsCastingFrame.t:SetAllPoints(targetIsCastingFrame)
	targetIsCastingFrame:Show()		
		
	targetIsCastingFrame:SetScript(""OnUpdate"", updateTargetIsCasting)

	print (""Initialising Unit Is Visible Frame"")
	unitIsVisibleFrame = CreateFrame(""frame"");
	unitIsVisibleFrame:SetSize(size, size);
	unitIsVisibleFrame:SetPoint(""TOPLEFT"", size * 4, -(size * 2))             -- column 5 row 3
	unitIsVisibleFrame.t = unitIsVisibleFrame:CreateTexture()        
	unitIsVisibleFrame.t:SetColorTexture(0, 1, 0, 1)
	unitIsVisibleFrame.t:SetAllPoints(unitIsVisibleFrame)
	unitIsVisibleFrame:Show()		
		
	unitIsVisibleFrame:SetScript(""OnUpdate"", updateUnitIsVisible)
		
	print (""Initialising Player Buff Frames"")
	local i = 5
	for _, buffId in pairs(buffs) do
		buffFrames[buffId] = CreateFrame(""frame"")
		buffFrames[buffId]:SetSize(size, size)
		buffFrames[buffId]:SetPoint(""TOPLEFT"", i * size, -(size * 2))         -- column 6+ row 3
		buffFrames[buffId].t = buffFrames[buffId]:CreateTexture()        
		buffFrames[buffId].t:SetColorTexture(1, 1, 1, 1)
		buffFrames[buffId].t:SetAllPoints(buffFrames[buffId])
		buffFrames[buffId]:Show()
		               
		buffFrames[buffId]:SetScript(""OnUpdate"", updateMyBuffs)
		i = i + 1
	end

	print (""Initialising Target Debuff Frames"")
	local i = 0
	for _, debuffId in pairs(debuffs) do
		targetDebuffFrames[debuffId] = CreateFrame(""frame"")
		targetDebuffFrames[debuffId]:SetSize(size, size)
		targetDebuffFrames[debuffId]:SetPoint(""TOPLEFT"", i * size, -(size * 7))         -- column 1+ row 8
		targetDebuffFrames[debuffId].t = targetDebuffFrames[debuffId]:CreateTexture()        
		targetDebuffFrames[debuffId].t:SetColorTexture(1, 1, 1, 1)
		targetDebuffFrames[debuffId].t:SetAllPoints(targetDebuffFrames[debuffId])
		targetDebuffFrames[debuffId]:Show()
		               
		targetDebuffFrames[debuffId]:SetScript(""OnUpdate"", updateTargetDebuffs)
		i = i + 1
	end
	
	print (""Initialization Complete"")
end

local function eventHandler(self, event, ...)
	local arg1 = ...
	if event == ""ADDON_LOADED"" then
		if (arg1 == ""PixMag"") then
			print(""Addon Loaded... PixMag"")
			print(""Tracking "" .. table.getn(cooldowns) .. "" cooldowns"")
			initFrames()
		end
	end
end	

f:SetScript(""OnEvent"", eventHandler) ";

    }
}
