#define	SCALE_NUM_MAX			10
typedef struct {
	u8		scale_num;	//�ܵĳӸ���
	u8		qualified;	//������Ͻ�� �ϸ���� 1:�ϸ� 0: ���ϸ�[������ǿ��֮��]
	u8		comb_heads[SCALE_NUM_MAX]; //��϶���ţ���ʾ������ϵĶ�����û�в�����ϵĶ��������Լ�������
	u8		state[SCALE_NUM_MAX];	// ��ͷ״̬,������������ʱ���������õĳ�̨��������
	float	wet[SCALE_NUM_MAX];		// ������ͷ�ĵ�ǰ����
	u32		quali;					// �ϸ���
	u32		unquali;				// ���ϸ���
	float	quali_wet;				// ���κϸ��������,���ܺϸ���񣬶���һ������

}resultDef;