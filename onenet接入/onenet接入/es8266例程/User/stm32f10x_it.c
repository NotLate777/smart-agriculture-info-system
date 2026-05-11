/**
  ******************************************************************************
  * @file    Project/STM32F10x_StdPeriph_Template/stm32f10x_it.c 
  * @author  MCD Application Team
  * @version V3.5.0
  * @date    08-April-2011
  * @brief   Main Interrupt Service Routines.
  *          This file provides template for all exceptions handler and 
  *          peripherals interrupt service routine.
  ******************************************************************************
  * @attention
  *
  * THE PRESENT FIRMWARE WHICH IS FOR GUIDANCE ONLY AIMS AT PROVIDING CUSTOMERS
  * WITH CODING INFORMATION REGARDING THEIR PRODUCTS IN ORDER FOR THEM TO SAVE
  * TIME. AS A RESULT, STMICROELECTRONICS SHALL NOT BE HELD LIABLE FOR ANY
  * DIRECT, INDIRECT OR CONSEQUENTIAL DAMAGES WITH RESPECT TO ANY CLAIMS ARISING
  * FROM THE CONTENT OF SUCH FIRMWARE AND/OR THE USE MADE BY CUSTOMERS OF THE
  * CODING INFORMATION CONTAINED HEREIN IN CONNECTION WITH THEIR PRODUCTS.
  *
  * <h2><center>&copy; COPYRIGHT 2011 STMicroelectronics</center></h2>
  ******************************************************************************
  */

/* Includes ------------------------------------------------------------------*/
#include "stm32f10x_it.h"
#include "usart1.h"
#include "usart2.h"
#include "stdlib.h"
#include "stdio.h"
#include "string.h"
extern uint32_t SystickTime;
extern __IO uint32_t TimeDisplay;

/** @addtogroup STM32F10x_StdPeriph_Template
  * @{
  */

/* Private typedef -----------------------------------------------------------*/
/* Private define ------------------------------------------------------------*/
/* Private macro -------------------------------------------------------------*/
/* Private variables ---------------------------------------------------------*/
/* Private function prototypes -----------------------------------------------*/
/* Private functions ---------------------------------------------------------*/

/******************************************************************************/
/*            Cortex-M3 Processor Exceptions Handlers                         */
/******************************************************************************/

/**
  * @brief  This function handles NMI exception.
  * @param  None
  * @retval None
  */
void NMI_Handler(void)
{
}

/**
  * @brief  This function handles Hard Fault exception.
  * @param  None
  * @retval None
  */
void HardFault_Handler(void)
{
  /* Go to infinite loop when Hard Fault exception occurs */
  while (1)
  {
  }
}

/**
  * @brief  This function handles Memory Manage exception.
  * @param  None
  * @retval None
  */
void MemManage_Handler(void)
{
  /* Go to infinite loop when Memory Manage exception occurs */
  while (1)
  {
  }
}

/**
  * @brief  This function handles Bus Fault exception.
  * @param  None
  * @retval None
  */
void BusFault_Handler(void)
{
  /* Go to infinite loop when Bus Fault exception occurs */
  while (1)
  {
  }
}

/**
  * @brief  This function handles Usage Fault exception.
  * @param  None
  * @retval None
  */
void UsageFault_Handler(void)
{
  /* Go to infinite loop when Usage Fault exception occurs */
  while (1)
  {
  }
}

/**
  * @brief  This function handles SVCall exception.
  * @param  None
  * @retval None
  */
void SVC_Handler(void)
{
}

/**
  * @brief  This function handles Debug Monitor exception.
  * @param  None
  * @retval None
  */
void DebugMon_Handler(void)
{
}

/**
  * @brief  This function handles PendSVC exception.
  * @param  None
  * @retval None
  */
void PendSV_Handler(void)
{
}

/**
  * @brief  This function handles SysTick Handler.
  * @param  None
  * @retval None
  */
void SysTick_Handler(void)
{
    //SystickTime_Increase();
    //SystickTime++;
}

/******************************************************************************/
/*                 STM32F10x Peripherals Interrupt Handlers                   */
/*  Add here the Interrupt Handler for the used peripheral(s) (PPP), for the  */
/*  available peripheral interrupt handler's name please refer to the startup */
/*  file (startup_stm32f10x_xx.s).                                            */
/******************************************************************************/


void EXTI0_IRQHandler(void)
{
    
}

/**
  * @brief  This function handles usart1 global interrupt request.
  * @param  None
  * @retval : None
  */
void USART1_IRQHandler(void)
{
	#if 0
		unsigned int data;

    if(USART1->SR & 0x0F)
    {
        // See if we have some kind of error
        // Clear interrupt (do nothing about it!)
        data = USART1->DR;
    }
    else if(USART1->SR & USART_FLAG_RXNE)      //Receive Data Reg Full Flag
    {		
        data = USART1->DR;
				//usart1_putrxchar(data);     //Insert received character into buffer                     
    }
		else
		{;}
			#endif
}

/**
  * @brief  This function handles usart2 global interrupt request.
  * @param  None
  * @retval : None
  */
void LED_CmdCtl(void);

//传输的html代码
char * Send_HtmlData="<html><body><form><h4>SSID:<input type=\"text\" name=\"SSID\" value=\"\"/><br/>PSWD:<input type=\"text\" name=\"PSW\" value=\"\"/><br/><br/><input type=\"submit\" value=\"SUBMIT\"/></h4></form></body></html>\r\n";
char CIPSEND_CONNECT[50];
char CIPCLOSE[50];
int connect_id = -1;
char http_parameter[100];
char SSID[16];
char PSW[16];

void USART2_IRQHandler(void)
{
		unsigned int data;
		int i;

    if(USART2->SR & 0x0F)
    {
        // See if we have some kind of error
        // Clear interrupt (do nothing about it!)
        data = USART2->DR;
    }
		else if(USART2->SR & USART_FLAG_RXNE)   //Receive Data Reg Full Flag
    {		
        data = USART2->DR;
				usart2_rcv_buf[usart2_rcv_len++]=data;
			//printf("++++++++++++++++++++++++: %c\r\n",data);
				if(data==rcv_http_data_head[rcv_http_data_count]) //对比获取到的数据是否符合对比标识的当前位
				{
						rcv_http_data_count++;			//若有符合，对比位右移一位
						if(strlen(rcv_http_data_head) == rcv_http_data_count)	//若对比位等于对比标识长度，即完全符合。开始获取内容
						{
							for(i=0;i<strlen(rcv_http_data_head)-1;i++)
							{
								usart2_cmd_buf[usart2_cmd_len++]=rcv_http_data_head[i];
							}
							rcv_http_data_count=0;
							rcv_cmd_start=1;
						}
				}
				else
				{
					rcv_http_data_count=0;
				}
			
				if(data=='[') //约定平台下发的控制命令以'['为开始符，‘]’为控制命令结束符，读者可以自定义自己的开始符合结束符
				{
						rcv_cmd_start=1;
				}
				if(rcv_cmd_start==1)
				{
						usart2_cmd_buf[usart2_cmd_len++]=data;
						if((data==']')||(usart2_cmd_len>=MAX_CMD_LEN-1))
						{
								rcv_cmd_start=0;
								//打印EDP下发命令信息
								printf("EDP Command: %s\r\n",(const char *)usart2_cmd_buf);
								//LED灯控制
								//LED_CmdCtl();
								
								memset(usart2_cmd_buf,0,usart2_cmd_len);
        				usart2_cmd_len=0;
						}
						if((data=='\n')||(usart2_cmd_len>=MAX_CMD_LEN-1))
						{
								rcv_cmd_start=0;

								//清空SSID和PSW
								memset(SSID,0,16);
								memset(PSW,0,16);
							
								printf("GET_Value: %s\r\n",(const char *)usart2_cmd_buf);
								
								sscanf((const char *)usart2_cmd_buf,"+IPD,%d,%*d:%*s%s",&connect_id,http_parameter);
								printf("connect_id: %d\r\n",connect_id);
								printf("http_parameter: %s\r\n",(const char *)http_parameter);
								sprintf(CIPSEND_CONNECT,"AT+CIPSEND=%d,%d\r\n",connect_id,strlen(Send_HtmlData)+1);	
								sprintf(CIPCLOSE,"AT+CIPCLOSE=%d\r\n",connect_id);	
							
								sscanf((const char *)http_parameter,"%*[/?]SSID=%[^&]%*[&]PSW=%[^ ]",SSID,PSW);
							
							//------------------------------------------------------------------

							
								memset(usart2_cmd_buf,0,usart2_cmd_len);
        				usart2_cmd_len=0;
						}
				}	  
    }
		else
		{
				;
		}
}

/**
  * @brief  This function handles RTC global interrupt request.
  * @param  None
  * @retval : None
  */
void RTC_IRQHandler(void)
{
   
}

/**
  * @brief  This function handles PPP interrupt request.
  * @param  None
  * @retval None
  */
/*void PPP_IRQHandler(void)
{
}*/

/**
  * @}
  */ 


/******************* (C) COPYRIGHT 2011 STMicroelectronics *****END OF FILE****/
