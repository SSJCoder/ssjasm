SSJASM.VM.{
	// .execute ({SSJASM.VM.Program})
	execute (program:Program) -> unc {
		// set data
		if ( program.data != none ) {
			//..
		}
		
		// create stack
		stack := new [:unc]
		stack->resize( MAX_STACK_SIZE )
		
		// set IP register
		IP := program.start_addr
		
		// set SP & RT registers
		SP:num = stack->size
		RT:unc = none
		
		// execute
		inst_list := program.code
				e := inst_list->length
		
		loop (IP < e) {
			// get instruction
			inst := inst_list[IP]
			
			case inst.kind {
				// "inc" operation
				INST_INC_SP => {
					SP++
				}
				
				INST_INC_RT => {
					RT++
				}
				
				INST_INC_IN => {
					stack[SP+inst.item:$num]++
				}
				
				// "dec" operation
				INST_DEC_SP => {
					SP--
				}
				
				INST_DEC_RT => {
					RT--
				}
				
				INST_DEC_IN => {
					stack[SP+inst.item:$num]--
				}
				
				// "mov" operation
				INST_MOV_SP_RT => {
					SP = RT:$num
				}
				
				INST_MOV_SP_IN => {
					SP = stack[SP+inst.item:$num]:$num
				}
				
				INST_MOV_SP_NUM => {
					SP = inst.item:$num
				}
				
				INST_MOV_RT_SP => {
					RT = SP
				}
				
				INST_MOV_RT_STR => {
					RT = inst.item
				}
				
				INST_MOV_RT_NUM => {
					RT = inst.item
				}
				
				INST_MOV_RT_IN => {
					RT = stack[SP+inst.item:$num]:$num
				}
				
				INST_MOV_IN_SP => {
					stack[SP+inst.item:$num] = SP
				}
				
				INST_MOV_IN_RT => {
					stack[SP+inst.item:$num] = RT
				}
				
				INST_MOV_IN_IN => {
					mov_in_in := inst.item:$inst.mov_in_in
					stack[SP+mov_in_in.offs1] = stack[SP+mov_in_in.offs2]
				}
				
				INST_MOV_IN_STR => {
					mov_in_str := inst.item:$inst.mov_in_str
					stack[SP+mov_in_str.offset] = mov_in_str.value
				}
				
				INST_MOV_IN_NUM => {
					mov_in_num := inst.item:$inst.mov_in_num
					stack[SP+mov_in_num.offset] = mov_in_num.value
				}
				
				// "add" operation
				INST_ADD_SP_RT => {
					SP = SP + RT:$num
				}
				
				INST_ADD_SP_IN => {
					SP = SP + stack[SP+inst.item:$num]:$num
				}
				
				INST_ADD_RT_SP => {
					RT = RT + SP
				}
				
				INST_ADD_RT_IN => {
					RT = RT + stack[SP+inst.item:$num]
				}
				
				INST_ADD_RT_STR => {
					RT = RT + inst.item
				}
				
				INST_ADD_RT_NUM => {
					RT = RT + inst.item
				}
				
				// conditional jump operation
				INST_CND_SP_RT_SML => {
					if ( SP < RT ) {
						IP = inst.item:$num, next // skips IP increment;;
					}
				}
				
				INST_CND_SP_RT_SME => {
					if ( SP <= RT ) {
						IP = inst.item:$num, next // skips IP increment;;
					}
				}
				
				INST_CND_SP_RT_EQL => {
					if ( SP != RT ) {
						IP = inst.item:$num, next // skips IP increment;;
					}
				}
				
				INST_CND_SP_RT_NEQ => {
					if ( SP != RT ) {
						IP = inst.item:$num, next // skips IP increment;;
					}
				}
				
				INST_CND_SP_RT_GTE => {
					if ( SP >= RT ) {
						IP = inst.item:$num, next // skips IP increment;;
					}
				}
				
				INST_CND_SP_RT_GRT => {
					if ( SP > RT ) {
						IP = inst.item:$num, next // skips IP increment;;
					}
				}
				
				// new inst.cnd_sp_in (kind, offset, jump-address)
				INST_CND_SP_IN_SML => {
					cnd_sp_in := inst.item:$inst.cnd_sp_in
					if ( SP < stack[SP+cnd_sp_in.offset] ) {
						IP = cnd_sp_in.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_SP_IN_SME => {
					cnd_sp_in := inst.item:$inst.cnd_sp_in
					if ( SP <= stack[SP+cnd_sp_in.offset] ) {
						IP = cnd_sp_in.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_SP_IN_EQL => {
					cnd_sp_in := inst.item:$inst.cnd_sp_in
					if ( SP == stack[SP+cnd_sp_in.offset] ) {
						IP = cnd_sp_in.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_SP_IN_NEQ => {
					cnd_sp_in := inst.item:$inst.cnd_sp_in
					if ( SP != stack[SP+cnd_sp_in.offset] ) {
						IP = cnd_sp_in.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_SP_IN_GTE => {
					cnd_sp_in := inst.item:$inst.cnd_sp_in
					if ( SP >= stack[SP+cnd_sp_in.offset] ) {
						IP = cnd_sp_in.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_SP_IN_GRT => {
					cnd_sp_in := inst.item:$inst.cnd_sp_in
					if ( SP > stack[SP+cnd_sp_in.offset] ) {
						IP = cnd_sp_in.jump_addr, next // skips IP increment;;
					}
				}
				
				// new inst.cnd_sp_num (kind, number, jump-address)
				INST_CND_SP_NUM_SML => {
					cnd_sp_num := inst.item:$inst.cnd_sp_num
					if ( SP < cnd_sp_num.number ) {
						IP = cnd_sp_num.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_SP_NUM_SME => {
					cnd_sp_num := inst.item:$inst.cnd_sp_num
					if ( SP <= cnd_sp_num.number ) {
						IP = cnd_sp_num.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_SP_NUM_EQL => {
					cnd_sp_num := inst.item:$inst.cnd_sp_num
					if ( SP == cnd_sp_num.number ) {
						IP = cnd_sp_num.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_SP_NUM_NEQ => {
					cnd_sp_num := inst.item:$inst.cnd_sp_num
					if ( SP != cnd_sp_num.number ) {
						IP = cnd_sp_num.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_SP_NUM_GTE => {
					cnd_sp_num := inst.item:$inst.cnd_sp_num
					if ( SP >= cnd_sp_num.number ) {
						IP = cnd_sp_num.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_SP_NUM_GRT => {
					cnd_sp_num := inst.item:$inst.cnd_sp_num
					if ( SP > cnd_sp_num.number ) {
						IP = cnd_sp_num.jump_addr, next // skips IP increment;;
					}
				}
				
				
				// --RT based (RT_x)
				INST_CND_RT_SP_SML => {
					if ( RT < SP ) {
						IP = inst.item:$num, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_SP_SME => {
					if ( RT <= SP ) {
						IP = inst.item:$num, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_SP_EQL => {
					if ( RT == SP ) {
						IP = inst.item:$num, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_SP_NEQ => {
					if ( RT != SP ) {
						IP = inst.item:$num, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_SP_GTE => {
					if ( RT >= SP ) {
						IP = inst.item:$num, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_SP_GRT => {
					if ( RT > SP ) {
						IP = inst.item:$num, next // skips IP increment;;
					}
				}
				
				// new inst.cnd_rt_in (kind, offset, jump-address)
				INST_CND_RT_IN_SML => {
					cnd_rt_in := inst.item:$inst.cnd_rt_in
					if ( RT < stack[SP+cnd_rt_in.offset] ) {
						IP = cnd_rt_in.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_IN_SME => {
					cnd_rt_in := inst.item:$inst.cnd_rt_in
					if ( RT <= stack[SP+cnd_rt_in.offset] ) {
						IP = cnd_rt_in.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_IN_EQL => {
					cnd_rt_in := inst.item:$inst.cnd_rt_in
					if ( RT == stack[SP+cnd_rt_in.offset] ) {
						IP = cnd_rt_in.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_IN_NEQ => {
					cnd_rt_in := inst.item:$inst.cnd_rt_in
					if ( RT != stack[SP+cnd_rt_in.offset] ) {
						IP = cnd_rt_in.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_IN_GTE => {
					cnd_rt_in := inst.item:$inst.cnd_rt_in
					if ( RT >= stack[SP+cnd_rt_in.offset] ) {
						IP = cnd_rt_in.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_IN_GRT => {
					cnd_rt_in := inst.item:$inst.cnd_rt_in
					if ( RT > stack[SP+cnd_rt_in.offset] ) {
						IP = cnd_rt_in.jump_addr, next // skips IP increment;;
					}
				}
				
				// new inst.cnd_rt_str (kind, string, jump-address)
				INST_CND_RT_STR_SML => {
					cnd_rt_str := inst.item:$inst.cnd_rt_str
					if ( RT < cnd_rt_str.string ) {
						IP = cnd_rt_str.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_STR_SME => {
					cnd_rt_str := inst.item:$inst.cnd_rt_str
					if ( RT <= cnd_rt_str.string ) {
						IP = cnd_rt_str.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_STR_EQL => {
					cnd_rt_str := inst.item:$inst.cnd_rt_str
					if ( RT == cnd_rt_str.string ) {
						IP = cnd_rt_str.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_STR_NEQ => {
					cnd_rt_str := inst.item:$inst.cnd_rt_str
					if ( RT != cnd_rt_str.string ) {
						IP = cnd_rt_str.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_STR_GTE => {
					cnd_rt_str := inst.item:$inst.cnd_rt_str
					if ( RT >= cnd_rt_str.string ) {
						IP = cnd_rt_str.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_STR_GRT => {
					cnd_rt_str := inst.item:$inst.cnd_rt_str
					if ( RT > cnd_rt_str.string ) {
						IP = cnd_rt_str.jump_addr, next // skips IP increment;;
					}
				}
				
				// new inst.cnd_rt_num (kind, number, jump-address)
				INST_CND_RT_NUM_SML => {
					cnd_rt_num := inst.item:$inst.cnd_rt_num
					if ( RT < cnd_rt_num.number ) {
						IP = cnd_rt_num.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_NUM_SME => {
					cnd_rt_num := inst.item:$inst.cnd_rt_num
					if ( RT <= cnd_rt_num.number ) {
						IP = cnd_rt_num.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_NUM_EQL => {
					cnd_rt_num := inst.item:$inst.cnd_rt_num
					if ( RT == cnd_rt_num.number ) {
						IP = cnd_rt_num.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_NUM_NEQ => {
					cnd_rt_num := inst.item:$inst.cnd_rt_num
					if ( RT != cnd_rt_num.number ) {
						IP = cnd_rt_num.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_NUM_GTE => {
					cnd_rt_num := inst.item:$inst.cnd_rt_num
					if ( RT >= cnd_rt_num.number ) {
						IP = cnd_rt_num.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_RT_NUM_GRT => {
					cnd_rt_num := inst.item:$inst.cnd_rt_num
					if ( RT > cnd_rt_num.number ) {
						IP = cnd_rt_num.jump_addr, next // skips IP increment;;
					}
				}
				
				
				// --index based (IN_x)
				// new inst.cnd_in_reg (kind, offset, jump-address)
				INST_CND_IN_SP_SML => {
					cnd_in_reg := inst.item:$inst.cnd_in_reg
					if ( stack[SP+cnd_in_reg.offset] < SP ) {
						IP = cnd_in_reg.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_SP_SME => {
					cnd_in_reg := inst.item:$inst.cnd_in_reg
					if ( stack[SP+cnd_in_reg.offset] <= SP ) {
						IP = cnd_in_reg.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_SP_EQL => {
					cnd_in_reg := inst.item:$inst.cnd_in_reg
					if ( stack[SP+cnd_in_reg.offset] == SP ) {
						IP = cnd_in_reg.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_SP_NEQ => {
					cnd_in_reg := inst.item:$inst.cnd_in_reg
					if ( stack[SP+cnd_in_reg.offset] != SP ) {
						IP = cnd_in_reg.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_SP_GTE => {
					cnd_in_reg := inst.item:$inst.cnd_in_reg
					if ( stack[SP+cnd_in_reg.offset] >= SP ) {
						IP = cnd_in_reg.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_SP_GRT => {
					cnd_in_reg := inst.item:$inst.cnd_in_reg
					if ( stack[SP+cnd_in_reg.offset] > SP ) {
						IP = cnd_in_reg.jump_addr, next // skips IP increment;;
					}
				}
				
				
				// new inst.cnd_in_reg (kind, offset, jump-address)
				INST_CND_IN_RT_SML => {
					cnd_in_reg := inst.item:$inst.cnd_in_reg
					if ( stack[SP+cnd_in_reg.offset] < RT ) {
						IP = cnd_in_reg.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_RT_SME => {
					cnd_in_reg := inst.item:$inst.cnd_in_reg
					if ( stack[SP+cnd_in_reg.offset] <= RT ) {
						IP = cnd_in_reg.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_RT_EQL => {
					cnd_in_reg := inst.item:$inst.cnd_in_reg
					if ( stack[SP+cnd_in_reg.offset] == RT ) {
						IP = cnd_in_reg.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_RT_NEQ => {
					cnd_in_reg := inst.item:$inst.cnd_in_reg
					if ( stack[SP+cnd_in_reg.offset] != RT ) {
						IP = cnd_in_reg.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_RT_GTE => {
					cnd_in_reg := inst.item:$inst.cnd_in_reg
					if ( stack[SP+cnd_in_reg.offset] >= RT ) {
						IP = cnd_in_reg.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_RT_GRT => {
					cnd_in_reg := inst.item:$inst.cnd_in_reg
					if ( stack[SP+cnd_in_reg.offset] > RT ) {
						IP = cnd_in_reg.jump_addr, next // skips IP increment;;
					}
				}
				
				// new inst.cnd_in_in (kind, offset-1, offset-2, jump-address)
				INST_CND_IN_IN_SML => {
					cnd_in_in := inst.item:$inst.cnd_in_in
					if ( stack[SP+cnd_in_in.offset1] < stack[SP+cnd_in_in.offset2] ) {
						IP = cnd_in_in.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_IN_SME => {
					cnd_in_in := inst.item:$inst.cnd_in_in
					if ( stack[SP+cnd_in_in.offset1] <= stack[SP+cnd_in_in.offset2] ) {
						IP = cnd_in_in.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_IN_EQL => {
					cnd_in_in := inst.item:$inst.cnd_in_in
					if ( stack[SP+cnd_in_in.offset1] == stack[SP+cnd_in_in.offset2] ) {
						IP = cnd_in_in.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_IN_NEQ => {
					cnd_in_in := inst.item:$inst.cnd_in_in
					if ( stack[SP+cnd_in_in.offset1] != stack[SP+cnd_in_in.offset2] ) {
						IP = cnd_in_in.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_IN_GTE => {
					cnd_in_in := inst.item:$inst.cnd_in_in
					if ( stack[SP+cnd_in_in.offset1] >= stack[SP+cnd_in_in.offset2] ) {
						IP = cnd_in_in.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_IN_GRT => {
					cnd_in_in := inst.item:$inst.cnd_in_in
					if ( stack[SP+cnd_in_in.offset1] > stack[SP+cnd_in_in.offset2] ) {
						IP = cnd_in_in.jump_addr, next // skips IP increment;;
					}
				}
				
				// new inst.cnd_in_str (kind, offset, string, jump-address)
				INST_CND_IN_STR_SML => {
					cnd_in_str := inst.item:$inst.cnd_in_str
					if ( stack[SP+cnd_in_str.offset] < cnd_in_str.string ) {
						IP = cnd_in_str.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_STR_SME =>{
					cnd_in_str := inst.item:$inst.cnd_in_str
					if ( stack[SP+cnd_in_str.offset] <= cnd_in_str.string ) {
						IP = cnd_in_str.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_STR_EQL =>{
					cnd_in_str := inst.item:$inst.cnd_in_str
					if ( stack[SP+cnd_in_str.offset] == cnd_in_str.string ) {
						IP = cnd_in_str.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_STR_NEQ =>{
					cnd_in_str := inst.item:$inst.cnd_in_str
					if ( stack[SP+cnd_in_str.offset] != cnd_in_str.string ) {
						IP = cnd_in_str.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_STR_GTE =>{
					cnd_in_str := inst.item:$inst.cnd_in_str
					if ( stack[SP+cnd_in_str.offset] >= cnd_in_str.string ) {
						IP = cnd_in_str.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_STR_GRT =>{
					cnd_in_str := inst.item:$inst.cnd_in_str
					if ( stack[SP+cnd_in_str.offset] > cnd_in_str.string ) {
						IP = cnd_in_str.jump_addr, next // skips IP increment;;
					}
				}
				
				// new inst.cnd_in_num (kind, offset, number, jump-address)
				INST_CND_IN_NUM_SML => {
					cnd_in_num := inst.item:$inst.cnd_in_num
					if ( stack[SP+cnd_in_num.offset] < cnd_in_num.number ) {
						IP = cnd_in_num.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_NUM_SME => {
					cnd_in_num := inst.item:$inst.cnd_in_num
					if ( stack[SP+cnd_in_num.offset] <= cnd_in_num.number ) {
						IP = cnd_in_num.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_NUM_EQL => {
					cnd_in_num := inst.item:$inst.cnd_in_num
					if ( stack[SP+cnd_in_num.offset] == cnd_in_num.number ) {
						IP = cnd_in_num.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_NUM_NEQ => {
					cnd_in_num := inst.item:$inst.cnd_in_num
					if ( stack[SP+cnd_in_num.offset] != cnd_in_num.number ) {
						IP = cnd_in_num.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_NUM_GTE => {
					cnd_in_num := inst.item:$inst.cnd_in_num
					if ( stack[SP+cnd_in_num.offset] >= cnd_in_num.number ) {
						IP = cnd_in_num.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_CND_IN_NUM_GRT => {
					cnd_in_num := inst.item:$inst.cnd_in_num
					if ( stack[SP+cnd_in_num.offset] > cnd_in_num.number ) {
						IP = cnd_in_num.jump_addr, next // skips IP increment;;
					}
				}
				
				INST_MUT_SP => {
					SP += inst.item:$num
				}
				
				INST_JMP => {
					IP = inst.item:$num, next // skips IP increment;;
				}
				
				INST_EXPR => {
					RT = inst.item( stack, SP )
				}
				
				INST_PRT_SP => {
					@print( SP )
				}
				
				INST_PRT_RT => {
					@print( RT )
				}
				
				INST_PRT_IN => {
					@print( stack[SP+inst.item:$num] )
				}
				
				INST_PRT_JS => {
					@print( inst.item:$str )
				}
			}
			
			// continue
			IP++
		}
		
		// give result from RT register
		ret RT
	}
}

