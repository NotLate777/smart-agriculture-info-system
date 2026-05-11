#ifndef USART2_H_H
#define USART2_H_H

#define MAX_RCV_LEN  1024
#define MAX_CMD_LEN  256


extern void USART2_Init(void);
extern void usart2_write(USART_TypeDef* USARTx, uint8_t *Data,uint32_t len);

extern volatile unsigned char  rcv_cmd_start;
extern volatile unsigned char  rcv_cmd_flag;

extern unsigned char  usart2_rcv_buf[MAX_RCV_LEN];
extern volatile unsigned int   usart2_rcv_len;

extern unsigned char  usart2_cmd_buf[MAX_CMD_LEN];
extern volatile unsigned int   usart2_cmd_len;

extern const char rcv_http_data_head[4];//约定http请求的头部开始标识
extern volatile int rcv_http_data_count;					//http数据对比位计数

#endif

