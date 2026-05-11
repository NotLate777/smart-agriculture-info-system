#include "main.h"


EdpPacket* send_pkg;
char send_buf[MAX_SEND_BUF_LEN];


/**
  * @brief  ESP8266初始化
**/
void ESP8266_Init(void)
{
	printf("%s\r\n",AT);
		if(SendCmd(AT,"OK",10))
		{ 			
			printf("%s\r\n","SUCCESS.");
		}
		else
		{
			printf("%s\r\n","FAIL.");
		}
		
		printf("%s\r\n",CWMODE_1);
		if(SendCmd(CWMODE_1,"OK",10))
		{ 			
			printf("%s\r\n","SUCCESS.");
		}
		else
		{
			printf("%s\r\n","FAIL.");
		}

		printf("%s\r\n",RST);
		if(SendCmd(RST,"ready",10))
		{ 			
			printf("%s\r\n","SUCCESS.");
		}
		else
		{
			printf("%s\r\n","FAIL.");
		}

		printf("%s\r\n",CWJAP);
		if(SendCmd(CWJAP,"OK",30))
		{ 			
			printf("%s\r\n","SUCCESS.");
			//如果连接成功，转为【STA模式】
			printf("%s\r\n",CIFSR);
			if(SendCmd(CIFSR,"OK",20))
			{ 			
				printf("%s\r\n","SUCCESS.");
			}
			else
			{
				printf("%s\r\n","FAIL.");
			}
		
			printf("%s\r\n",CIPSTART);
			if(SendCmd(CIPSTART,"OK",20))
			{ 			
				printf("%s\r\n","SUCCESS.");
			}
			else
			{
				printf("%s\r\n","FAIL.");
			}
			
			printf("%s\r\n",CIPMODE);
			if(SendCmd(CIPMODE,"OK",10))
			{ 			
				printf("%s\r\n","SUCCESS.");
			}
			else
			{
				printf("%s\r\n","FAIL.");
			}
			//============【STA模式切换结束】
		}
		else
		{
			
			printf("%s\r\n","FAIL.");
			printf("%s\r\n","==============Change to AP_Mode==============");
			//如果连接失败，转为【配置模式】
			
			//清空SSID和PSW
			memset(SSID,0,16);
			memset(PSW,0,16);
			
			printf("%s\r\n",RST);
			if(SendCmd(RST,"ready",10))
			{ 			
				printf("%s\r\n","SUCCESS.");
			}
			else
			{
				printf("%s\r\n","FAIL.");
			}
			printf("%s\r\n",CWMODE_2);
			if(SendCmd(CWMODE_2,"OK",10))
			{ 			
				printf("%s\r\n","SUCCESS.");
			}
			else
			{
				printf("%s\r\n","FAIL.");
			}
			
			printf("%s\r\n",RST);
			if(SendCmd(RST,"ready",10))
			{ 			
				printf("%s\r\n","SUCCESS.");
			}
			else
			{
				printf("%s\r\n","FAIL.");
			}
			
			printf("%s\r\n",CWSAP);
			if(SendCmd(CWSAP,"OK",20))
			{ 			
				printf("%s\r\n","SUCCESS.");
			}
			else
			{
				printf("%s\r\n","FAIL.");
			}
			
			printf("%s\r\n",CIPMUX);
			if(SendCmd(CIPMUX,"OK",20))
			{ 			
				printf("%s\r\n","SUCCESS.");
			}
			else
			{
				printf("%s\r\n","FAIL.");
			}
			
			printf("%s\r\n",CIPSERVER);
			if(SendCmd(CIPSERVER,"OK",20))
			{ 			
				printf("%s\r\n","SUCCESS.");
			}
			else
			{
				printf("%s\r\n","FAIL.");
			}
			//============【配置模式切换结束】
		}
}

/**
  * @brief  生成各传感器当前状态的上传数据，分割字符串格式
**/
void GetSendBuf(void)
{

}


/**
  * @brief  发送一条AT指令
**/
unsigned short int SendCmd(char* cmd, char* result, int timeOut)
{
		int32 count=0;
		int TrySum=5;		//上限尝试次数
	
    while(TrySum--)
    {
        memset(usart2_rcv_buf,0,sizeof(usart2_rcv_buf));
				usart2_rcv_len=0;
						
        usart2_write(USART2,(uint8_t *)cmd,strlen(cmd));
				
				if(timeOut==0)
				{
					break;
				}
        //mDelay(timeOut);	
				for(count=0;count<timeOut;count++)
				{
						mDelay(100);
						if((NULL != strstr((const char *)usart2_rcv_buf, (const char *)result)))
						{
								if((NULL != strstr((const char *)usart2_rcv_buf, "CIFSR:STAIP")))
								{
									//输出获取到的IP地址
								printf("%s\r\n",usart2_rcv_buf);
								}
								break;
						}
				}		
        if(count<timeOut)
				{
						break;
				}
				
    }
		if(TrySum>0)
		{
			return 1;
		}
		else
		{
			return 0;
		}
}

/**
  * @brief  发送一条AT指令，并显示结果
**/
unsigned short int SendCmd_ShowResult(char* cmd,char* result)
{
		int32 count=0;
	      memset(usart2_rcv_buf,0,sizeof(usart2_rcv_buf));
				usart2_rcv_len=0;
						
        usart2_write(USART2,(uint8_t *)cmd,strlen(cmd));
				for(count=0;count<100;count++)
				{
						mDelay(100);
						printf("Result:\r\n%s\r\n",usart2_rcv_buf);
						if((NULL != strstr((const char *)usart2_rcv_buf, (const char *)result)))
						{
								break;
						}
				}		
				return 1;
}

/**
  * @brief  和平台建立设备连接
**/
void ESP8266_DevLink(const char* devid, const char* auth_key, int timeOut)
{
		int32 count=0;
	
		memset(usart2_rcv_buf,0,strlen((const char *)usart2_rcv_buf));
		usart2_rcv_len=0;			
		
		//printf("%s\r\n","[ESP8266_DevLink]ENTER device link...");
		usart2_write(USART2,CIPSEND,strlen(CIPSEND));  //向ESP8266发送数据透传指令
		for(count=0;count<timeOut;count++)
		{
				mDelay(100);
				if((NULL != strstr((const char *)usart2_rcv_buf,">")))
				{
						break;
				}
		}	

		send_pkg = PacketConnect1(devid,auth_key);
		mDelay(200);
		usart2_write(USART2,send_pkg->_data,send_pkg->_write_pos);  //发送设备连接请求数据
		mDelay(500);
		DeleteBuffer(&send_pkg);
		mDelay(200);
		usart2_write(USART2,"+++",3);  //向ESP8266发送+++结束透传，使ESP8266返回指令模式
		mDelay(50);
		//printf("%s\r\n","[ESP8266_DevLink]EXIT device link...");
}

/**
  * @brief  检测ESP8266连接状态
**/
int ESP8266_CheckStatus(int timeOut)
{
		int32 res=0;
		int32 count=0;
	
		memset(usart2_rcv_buf,0,sizeof(usart2_rcv_buf));
		usart2_rcv_len=0;
		
		//printf("%s\r\n","[ESP8266_CheckStatus]ENTER check status...");
		usart2_write(USART2,CIPSTATUS,strlen(CIPSTATUS));
		for(count=0;count<timeOut;count++)
		{
				mDelay(100);
				if((NULL != strstr((const char *)usart2_rcv_buf,"STATUS:4")))  //失去连接
				{
						res=-4;
						break;
				}
				else if((NULL != strstr((const char *)usart2_rcv_buf,"STATUS:3")))  //建立连接
				{
						res=0;	
						break;
				}
				else if((NULL != strstr((const char *)usart2_rcv_buf,"STATUS:2")))  //获得IP
				{
						res=-2;
						break;				
				}
				else if((NULL != strstr((const char *)usart2_rcv_buf,"STATUS:5")))  //物理掉线
				{
						res=-5;
						break;
				}
				else if((NULL != strstr((const char *)usart2_rcv_buf,"ERROR")))   
				{
						res=-1;
						break;
				}
				else
				{
						;
				}
		}	
		//printf("%s\r\n","[ESP8266_CheckStatus]EXIT check status...");
		return res;	
}

/**
  * @brief  向平台上传LED、温湿度当前状态数据
**/
void ESP8266_SendDat(void)
{		
		int32 count=0;

		memset(usart2_rcv_buf,0,sizeof(usart2_rcv_buf));
		usart2_rcv_len=0;			
		//printf("%s\r\n","[ESP8266_SendDat]ENTER Senddata...");
		usart2_write(USART2,CIPSEND,strlen(CIPSEND));  //向ESP8266发送数据透传指令
		for(count=0;count<40;count++)
		{
				mDelay(100);
				if((NULL != strstr((const char *)usart2_rcv_buf,">")))
				{
						break;
				}
		}	
	
		GetSendBuf();		
		//send_pkg = PacketSavedataSimpleString(DEVICEID,send_buf);   
		usart2_write(USART2,send_pkg->_data,send_pkg->_write_pos);	//向平台上传数据点
		DeleteBuffer(&send_pkg);
		mDelay(500);

		usart2_write(USART2,"+++",3);  //向ESP8266发送+++结束透传，使ESP8266返回指令模式
		mDelay(200);
		//printf("%s\r\n","[ESP8266_SendDat]EXIT Senddata...");
}

/**
  * @brief  ESP8266在STA模式下
						检查有没有连接请求，如果有，发送config页面
						---目前只做一路连接，没将多路连接做入数组
**/


void ESP8266_Echo(void)
{
	int i;
					//printf("connect_id_now: %d\r\n",connect_id);
					if(connect_id != -1)
					{
							if(strlen((const char *)SSID)>0)
							{
								//打印获得的SSID和PSW
								printf("SSID: %s\r\n",(const char *)SSID);
								printf("PSW: %s\r\n",(const char *)PSW);
								printf("SSID_Len: %d\r\n",strlen((const char *)SSID));
								
								  //Hal_I2C_Init();
									mDelay(50);
									for(i = 0; i < 16; i++)
									{
											AT24CXX_WriteByte(i, SSID[i]);
											//printf("i:%d val=:%c\n", i, SSID[i]);
											
											if(SSID[i]=='\0')
											{
												break;
											}
											
											mDelay(100);
									}
									//Hal_I2C_Init();
									mDelay(50);
									for(i = 0; i < 16; i++)
									{
											AT24CXX_WriteByte(i+16, PSW[i]);
											//printf("i:%d val=:%c\n", i, PSW[i]);
											
											if(PSW[i]=='\0')
											{
												break;
											}
											
											mDelay(100);
									}
									mDelay(50);
									sprintf(CWJAP,"AT+CWJAP=\"%.16s\",\"%.16s\"\r\n",SSID,PSW);		//构建AT命令的Wifi连接字符串
									printf("%s\r\n",CWJAP);
									
									printf("%s\r\n","==============Reboot ESP8266==========");
									ESP8266_Init();    //ESP8266初始化
							}
							else
							{
									printf("%s\r\n",CIPSEND_CONNECT);
									if(SendCmd(CIPSEND_CONNECT,"OK",10))
									{ 			
										printf("%s\r\n","SUCCESS.");
									}
									else
									{
										printf("%s\r\n","FAIL.");
									}
									
									printf("%s\r\n",Send_HtmlData);
									if(SendCmd(Send_HtmlData,"SEND OK",30))
									{ 			
										printf("%s\r\n","SUCCESS.");
									}
									else
									{
										printf("%s\r\n","FAIL.");
									}
									
									printf("%s\r\n",CIPCLOSE);
									if(SendCmd(CIPCLOSE,"OK",10))
									{ 			
										printf("%s\r\n","SUCCESS.");
									}
									else
									{
										printf("%s\r\n","FAIL.");
									}
									
									printf("%s\r\n","=========Config_mode=======");
							}
							connect_id=-1;
							
					}

		
}








