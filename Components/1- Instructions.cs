class SSJASM.VM.inst.equ_index {
	soff:num
	expr:unc
	
	//constructor (
}


class SSJASM.VM.inst.mov_in_in {
	offs1:num
	offs2:num
	
	// new SSJASM.VM.inst.mov_in_in (offset-1, offset-2)
	constructor (offset1:num, offset2:num) -> parent {
		// set properties
		@offs1 = offset1
		@offs2 = offset2
		// give instruction
		ret new SSJASM.VM.inst (types::(SSJASM.VM).INST_MOV_IN_IN, self)
	}
}

class SSJASM.VM.inst.mov_in_str {
	offset:num
	 value:str
	
	// new SSJASM.VM.inst.mov_in_str (offset, value)
	constructor (offset:num, value:str) -> parent {
		// set properties
		@offset = offset
		 @value = value
		// give instruction
		ret new SSJASM.VM.inst (types::(SSJASM.VM).INST_MOV_IN_STR, self)
	}
}

class SSJASM.VM.inst.mov_in_num {
	offset:num
	 value:num
	
	// new SSJASM.VM.inst.mov_in_num (offset, value)
	constructor (offset:num, value:num) -> parent {
		// set properties
		@offset = offset
		 @value = value
		// give instruction
		ret new SSJASM.VM.inst (types::(SSJASM.VM).INST_MOV_IN_NUM, self)
	}
}

