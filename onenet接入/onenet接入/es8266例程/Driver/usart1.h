#ifndef USART1_H_H
#define USART1_H_H

#define MAX_RCV_LEN  1024
#define MAX_CMD_LEN  256

extern void USART1_Init(void);
extern void usart1_write(USART_TypeDef* USARTx, uint8_t *Data,uint8_t len);

extern unsigned char usart1_rcv_buf[512];
extern volatile unsigned int  usart1_rcv_len;
extern volatile unsigned int  usart1_rcv_flag;
extern volatile unsigned int  usart1_rcv_start;

extern volatile unsigned char  usart1_rcv_cmd_start;
extern unsigned char  usart1_cmd_buf[MAX_CMD_LEN];
extern volatile unsigned int   usart1_cmd_len;

#endif

