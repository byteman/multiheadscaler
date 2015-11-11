#define	SCALE_NUM_MAX			10
typedef struct {
	u8		scale_num;	//总的秤个数
	u8		qualified;	//本次组合结果 合格与否 1:合格 0: 不合格[可能是强排之类]
	u8		comb_heads[SCALE_NUM_MAX]; //组合斗编号，表示参与组合的斗数，没有参与组合的斗数，我自己来计算
	u8		state[SCALE_NUM_MAX];	// 秤头状态,具体主动发送时个数由设置的秤台数量决定
	float	wet[SCALE_NUM_MAX];		// 各个秤头的当前重量
	u32		quali;					// 合格数
	u32		unquali;				// 不合格数
	float	quali_wet;				// 本次合格的总重量,不管合格与否，都有一个重量

}resultDef;