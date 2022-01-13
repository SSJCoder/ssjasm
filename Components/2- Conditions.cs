class SSJASM.VM.inst.cnd_sp_in {
	   offset:num
	jump_addr:num
	
	// new SSJASM.VM.inst.cnd_sp_in (kind, offset, jump-address)
	constructor (kind:num, offset:num, jump_addr:num) -> parent {
		// set properties
		   @offset = offset
		@jump_addr = jump_addr
		// give instruction
		ret new SSJASM.VM.inst (kind, self)
	}
}

class SSJASM.VM.inst.cnd_sp_num {
	   number:num
	jump_addr:num
	
	// new SSJASM.VM.inst.cnd_sp_num (kind, number, jump-address)
	constructor (kind:num, number:num, jump_addr:num) -> parent {
		// set properties
		   @number = number
		@jump_addr = jump_addr
		// give instruction
		ret new SSJASM.VM.inst (kind, self)
	}
}

class SSJASM.VM.inst.cnd_rt_in {
	   offset:num
	jump_addr:num
	
	// new SSJASM.VM.inst.cnd_rt_in (kind, offset, jump-address)
	constructor (kind:num, offset:num, jump_addr:num) -> parent {
		// set properties
		   @offset = offset
		@jump_addr = jump_addr
		// give instruction
		ret new SSJASM.VM.inst (kind, self)
	}
}

class SSJASM.VM.inst.cnd_rt_str {
	   string:str
	jump_addr:num
	
	// new SSJASM.VM.inst.cnd_rt_num (kind, string, jump-address)
	constructor (kind:num, string:str, jump_addr:num) -> parent {
		// set properties
		   @string = string
		@jump_addr = jump_addr
		// give instruction
		ret new SSJASM.VM.inst (kind, self)
	}
}

class SSJASM.VM.inst.cnd_rt_num {
	   number:num
	jump_addr:num
	
	// new SSJASM.VM.inst.cnd_rt_num (kind, number, jump-address)
	constructor (kind:num, number:num, jump_addr:num) -> parent {
		// set properties
		   @number = number
		@jump_addr = jump_addr
		// give instruction
		ret new SSJASM.VM.inst (kind, self)
	}
}

class SSJASM.VM.inst.cnd_in_reg {
	   offset:num
	jump_addr:num
	
	// new inst.cnd_in_reg (kind, offset, jump-address)
	constructor (kind:num, offset:num, jump_addr:num) -> parent {
		// set properties
		   @offset = offset
		@jump_addr = jump_addr
		// give instruction
		ret new SSJASM.VM.inst (kind, self)
	}
}

class SSJASM.VM.inst.cnd_in_in {
	  offset1:num
	  offset2:num
	jump_addr:num
	
	// new inst.cnd_in_in (kind, offset-1, offset-2, jump-address)
	constructor (kind:num, offset1:num, offset2:num, jump_addr:num) -> parent {
		// set properties
		  @offset1 = offset1
		  @offset2 = offset2
		@jump_addr = jump_addr
		// give instruction
		ret new SSJASM.VM.inst (kind, self)
	}
}

class SSJASM.VM.inst.cnd_in_str {
	   offset:num
	   string:str
	jump_addr:num
	
	// new inst.cnd_in_str (kind, offset, string, jump-address)
	constructor (kind:num, offset:num, string:str, jump_addr:num) -> parent {
		// set properties
		   @offset = offset
		   @string = string
		@jump_addr = jump_addr
		// give instruction
		ret new SSJASM.VM.inst (kind, self)
	}
}

class SSJASM.VM.inst.cnd_in_num {
	   offset:num
	   number:num
	jump_addr:num
	
	// new inst.cnd_in_num (kind, offset, number, jump-address)
	constructor (kind:num, offset:num, number:num, jump_addr:num) -> parent {
		// set properties
		   @offset = offset
		   @number = number
		@jump_addr = jump_addr
		// give instruction
		ret new SSJASM.VM.inst (kind, self)
	}
}

