/**
  ******************************************************************************
  * @file    Project/STM32F10x_StdPeriph_Template/main.c 
  * @author  MCD Application Team
  * @version V3.5.0
  * @date    08-April-2011
  * @brief   Main program body
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
//USE_STDPERIPH_DRIVER, STM32F10X_HD, USE_STM3210B_EVAL

/***头文件引用****/
#include "main.h"

		
		char 	CWJAP[100]={0};		//AT命令CWJAP串
		
/**
  * @brief  使用esp8266模块和EDP协议连接ONENET平台
**/
int main(void)
{				
		int i;
	
		//SystemInit();
 		USART1_Init(); //USART1串口初始化函数 For printf()
 		USART2_Init(); //USART2串口初始化函数
	
		printf("%s\r\n","==================");
		
		Hal_I2C_Init();				
		mDelay(1000);
    for(i = 0; i < 16; i++)
    {
        printf("i:%d\n", i);
				SSID[i]=(u8)AT24CXX_ReadByte(i);
        mDelay(100);
    }
		Hal_I2C_Init();		
		mDelay(100);
	    for(i = 0; i < 16; i++)
    {
        printf("i:%d\n", i);
				PSW[i]=(u8)AT24CXX_ReadByte(i+16);
        mDelay(100);
    }

		sprintf(CWJAP,"AT+CWJAP=\"%.16s\",\"%.16s\"\r\n",SSID,PSW);		//构建AT命令的Wifi连接字符串
		printf("%s\r\n",CWJAP);

		ESP8266_Init();    //ESP8266初始化
	
				while(1)
				{						

						
// 						if(!(ESP8266_CheckStatus(30)))    //检测ESP8266模块连接状态
// 						{    

// 						}
// 						else
// 						{
									ESP8266_Echo();
// 						}
					mDelay(1000);
				}
}

/******************* (C) COPYRIGHT 2010 STMicroelectronics *****END OF FILE****/
