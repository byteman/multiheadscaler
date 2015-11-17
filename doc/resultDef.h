#define	SCALE_NUM_MAX			10
typedef struct {
	u8		scale_num;	//总的秤个数
	u8		qualified;	//本次组合结果 合格与否 1:合格 0: 不合格[可能是强排之类]
	u8		comb_heads[SCALE_NUM_MAX]; //组合斗编号，表示参与组合的斗数，没有参与组合的斗数，我自己来计算
	u8		state[SCALE_NUM_MAX];	// 秤头状态,具体主动发送时个数由设置的秤台数量决定
	float	wet[SCALE_NUM_MAX];		// 各个秤头的当前重量
	u8		quali;					// 合格数
	u8		unquali;				// 不合格数
	float	quali_wet;				// 本次合格的总重量,不管合格与否，都有一个重量

}resultDef;

模拟数据: 直接发的结构体
51 80 C2 00 01 80 02 44 01 0A 01 01 02 00 00 00 00 00 00 00 00 01 01 02 02 02 02 02 02 02 02 00 00 48 43 33 33 48 43 00 00 46 43 00 00 47 43 00 00 48 43 00 00 49 43 00 00 4A 43 00 00 4B 43 00 00 4C 43 00 00 4D 43 02 08 9A 19 C8 43 A4 48
