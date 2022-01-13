class SSJASM.VM {
	MAX_STACK_SIZE => 262144
	
	enum instKinds {
		INST_MUT_SP
		INST_JMP
		INST_EXPR
		
		// inc operation
		INST_INC_SP // SP++
		INST_INC_RT // RT++
		INST_INC_IN // [SP+..]++ ; <offset>
		
		// dec operation
		INST_DEC_SP // SP--
		INST_DEC_RT // RT--
		INST_DEC_IN // [SP+..]-- ; <offset>
		
		// mov operation
		INST_MOV_SP_RT  // SP = RT
		INST_MOV_SP_IN  // SP = [SP+..] ; <offset>
		INST_MOV_SP_NUM // SP = 4
		
		INST_MOV_RT_SP  // RT = SP
		INST_MOV_RT_IN  // RT = [SP+..] ; <offset>
		INST_MOV_RT_STR // RT = "..."
		INST_MOV_RT_NUM // RT = 4
		
		INST_MOV_IN_SP  // [SP+..] = SP      ; <offset>
		INST_MOV_IN_RT  // [SP+..] = RT      ; <offset>
		INST_MOV_IN_IN  // [SP+..] = [SP+..] ; <offset-1, offset-2>
		INST_MOV_IN_STR // [SP+..] = "..."   ; <offset, value>
		INST_MOV_IN_NUM // [SP+..] = 4       ; <offset, value>
		
		// add operation
		INST_ADD_SP_RT  // SP = SP + RT
		INST_ADD_SP_IN  // <offset>
		INST_ADD_SP_NUM // SP = SP + 4
		
		INST_ADD_RT_SP  // RT = RT + SP
		INST_ADD_RT_IN  // <offset>
		INST_ADD_RT_STR // RT = RT + "..."
		INST_ADD_RT_NUM // RT = RT + 4
		
		INST_ADD_IN_IN // <offset-1, offset-2>
		
		// conditional jump operation
		INST_CND_SP_RT_SML // SP <  RT ; <jump-address>
		INST_CND_SP_RT_SME // SP <= RT ; <jump-address>
		INST_CND_SP_RT_EQL // SP == RT ; <jump-address>
		INST_CND_SP_RT_NEQ // SP != RT ; <jump-address>
		INST_CND_SP_RT_GTE // SP >= RT ; <jump-address>
		INST_CND_SP_RT_GRT // SP >  RT ; <jump-address>
		
		// new inst.cnd_sp_in (kind, offset, jump-address)
		INST_CND_SP_IN_SML // SP <  [SP+..] ; <offset, jump-address>
		INST_CND_SP_IN_SME // SP <= [SP+..] ; <offset, jump-address>
		INST_CND_SP_IN_EQL // SP == [SP+..] ; <offset, jump-address>
		INST_CND_SP_IN_NEQ // SP != [SP+..] ; <offset, jump-address>
		INST_CND_SP_IN_GTE // SP >= [SP+..] ; <offset, jump-address>
		INST_CND_SP_IN_GRT // SP >  [SP+..] ; <offset, jump-address>
		
		// new inst.cnd_sp_num (kind, number, jump-address)
		INST_CND_SP_NUM_SML // SP <  4 ; <number, jump-address>
		INST_CND_SP_NUM_SME // SP <= 4 ; <number, jump-address>
		INST_CND_SP_NUM_EQL // SP == 4 ; <number, jump-address>
		INST_CND_SP_NUM_NEQ // SP != 4 ; <number, jump-address>
		INST_CND_SP_NUM_GTE // SP >= 4 ; <number, jump-address>
		INST_CND_SP_NUM_GRT // SP >  4 ; <number, jump-address>
		
		// --RT based (RT_x)
		INST_CND_RT_SP_SML // RT <  SP ; <jump-address>
		INST_CND_RT_SP_SME // RT <= SP ; <jump-address>
		INST_CND_RT_SP_EQL // RT == SP ; <jump-address>
		INST_CND_RT_SP_NEQ // RT != SP ; <jump-address>
		INST_CND_RT_SP_GTE // RT >= SP ; <jump-address>
		INST_CND_RT_SP_GRT // RT >  SP ; <jump-address>
		
		// new inst.cnd_rt_in (kind, offset, jump-address)
		INST_CND_RT_IN_SML // RT <  [SP+..] ; <offset, jump-address>
		INST_CND_RT_IN_SME // RT <= [SP+..] ; <offset, jump-address>
		INST_CND_RT_IN_EQL // RT == [SP+..] ; <offset, jump-address>
		INST_CND_RT_IN_NEQ // RT != [SP+..] ; <offset, jump-address>
		INST_CND_RT_IN_GTE // RT >= [SP+..] ; <offset, jump-address>
		INST_CND_RT_IN_GRT // RT >  [SP+..] ; <offset, jump-address>
		
		// new inst.cnd_rt_str (kind, string, jump-address)
		INST_CND_RT_STR_SML // RT <  "..." ; <string, jump-address>
		INST_CND_RT_STR_SME // RT <= "..." ; <string, jump-address>
		INST_CND_RT_STR_EQL // RT == "..." ; <string, jump-address>
		INST_CND_RT_STR_NEQ // RT != "..." ; <string, jump-address>
		INST_CND_RT_STR_GTE // RT >= "..." ; <string, jump-address>
		INST_CND_RT_STR_GRT // RT >  "..." ; <string, jump-address>
		
		// new inst.cnd_rt_num (kind, number, jump-address)
		INST_CND_RT_NUM_SML // RT <  4 ; <number, jump-address>
		INST_CND_RT_NUM_SME // RT <= 4 ; <number, jump-address>
		INST_CND_RT_NUM_EQL // RT == 4 ; <number, jump-address>
		INST_CND_RT_NUM_NEQ // RT != 4 ; <number, jump-address>
		INST_CND_RT_NUM_GTE // RT >= 4 ; <number, jump-address>
		INST_CND_RT_NUM_GRT // RT >  4 ; <number, jump-address>
		
		// --index based (IN_x)
		// new inst.cnd_in_reg (kind, offset, jump-address)
		INST_CND_IN_SP_SML // [SP+..] <  SP ; <offset, jump-address>
		INST_CND_IN_SP_SME // [SP+..] <= SP ; <offset, jump-address>
		INST_CND_IN_SP_EQL // [SP+..] == SP ; <offset, jump-address>
		INST_CND_IN_SP_NEQ // [SP+..] != SP ; <offset, jump-address>
		INST_CND_IN_SP_GTE // [SP+..] >= SP ; <offset, jump-address>
		INST_CND_IN_SP_GRT // [SP+..] >  SP ; <offset, jump-address>
		
		// new inst.cnd_in_reg (kind, offset, jump-address)
		INST_CND_IN_RT_SML // [SP+..] <  RT ; <offset, jump-address>
		INST_CND_IN_RT_SME // [SP+..] <= RT ; <offset, jump-address>
		INST_CND_IN_RT_EQL // [SP+..] == RT ; <offset, jump-address>
		INST_CND_IN_RT_NEQ // [SP+..] != RT ; <offset, jump-address>
		INST_CND_IN_RT_GTE // [SP+..] >= RT ; <offset, jump-address>
		INST_CND_IN_RT_GRT // [SP+..] >  RT ; <offset, jump-address>
		
		// new inst.cnd_in_in (kind, offset-1, offset-2, jump-address)
		INST_CND_IN_IN_SML // [SP+..] <  [SP+..] ; <offset-1, offset-2, jump-address>
		INST_CND_IN_IN_SME // [SP+..] <= [SP+..] ; <offset-1, offset-2, jump-address>
		INST_CND_IN_IN_EQL // [SP+..] == [SP+..] ; <offset-1, offset-2, jump-address>
		INST_CND_IN_IN_NEQ // [SP+..] != [SP+..] ; <offset-1, offset-2, jump-address>
		INST_CND_IN_IN_GTE // [SP+..] >= [SP+..] ; <offset-1, offset-2, jump-address>
		INST_CND_IN_IN_GRT // [SP+..] >  [SP+..] ; <offset-1, offset-2, jump-address>
		
		// new inst.cnd_in_str (kind, offset, string, jump-address)
		INST_CND_IN_STR_SML // [SP+..] <  "..." ; <offset, string, jump-address>
		INST_CND_IN_STR_SME // [SP+..] <= "..." ; <offset, string, jump-address>
		INST_CND_IN_STR_EQL // [SP+..] == "..." ; <offset, string, jump-address>
		INST_CND_IN_STR_NEQ // [SP+..] != "..." ; <offset, string, jump-address>
		INST_CND_IN_STR_GTE // [SP+..] >= "..." ; <offset, string, jump-address>
		INST_CND_IN_STR_GRT // [SP+..] >  "..." ; <offset, string, jump-address>
		
		// new inst.cnd_in_num (kind, offset, number, jump-address)
		INST_CND_IN_NUM_SML // [SP+..] <  4 ; <offset, number, jump-address>
		INST_CND_IN_NUM_SME // [SP+..] <= 4 ; <offset, number, jump-address>
		INST_CND_IN_NUM_EQL // [SP+..] == 4 ; <offset, number, jump-address>
		INST_CND_IN_NUM_NEQ // [SP+..] != 4 ; <offset, number, jump-address>
		INST_CND_IN_NUM_GTE // [SP+..] >= 4 ; <offset, number, jump-address>
		INST_CND_IN_NUM_GRT // [SP+..] >  4 ; <offset, number, jump-address>
		
		// print operation
		INST_PRT_SP
		INST_PRT_RT
		INST_PRT_IN // ; <offset>
		INST_PRT_JS
	}
	
	print:unc => new function (str:str) -> void {
		prt str
	}
	
	class Program {
		code:[:SSJASM.VM.inst]
		data:[:unc]
		//..
		start_addr:num
		
		// new SSJASM.VM.Program (code, [start-address], [data])
		constructor (code:[:SSJASM.VM.inst], start_addr:num|0, data:[:unc]|none) {
			// set properties
			@code = code
			@data = data
			
			//..
			@start_addr = start_addr
		}
	}
	
	class inst {
		kind => -1
		item:unc
		
		constructor (kind:num) {
			// set kind
			@kind = kind
		}
		
		constructor (kind:num, item:unc) {
			// set properties
			@kind = kind
			@item = item
		}
	}
	
	// new SSJASM.VM ()
	constructor () {
		
	}
}

class SSJASM.VM.memory {
	 name:str
	index:num
	
	//..
	loct:loct
	
	// new SSJASM.VM.memory (name, location)
	constructor (name:str, loct:loct) {
		// set name & location
		@name = name
		@loct = loct
	}
}

