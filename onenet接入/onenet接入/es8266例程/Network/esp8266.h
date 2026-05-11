#ifndef   ESP8266_H_H
#define   ESP8266_H_H

#define   AT          "AT\r\n"	
#define   RST         "AT+RST\r\n"

#define   CIFSR       "AT+CIFSR\r\n"
#define   CIPSTART    "AT+CIPSTART=\"TCP\",\"183.230.40.39\",876\r\n"	//EDP连接
#define   CWMODE_1    "AT+CWMODE=1\r\n"
#define   CIPSEND     "AT+CIPSEND\r\n"
#define   CIPSTATUS   "AT+CIPSTATUS\r\n"
#define   CIPMODE     "AT+CIPMODE=1\r\n"			//透传模式

#define   CWMODE_2    "AT+CWMODE=2\r\n"
#define   CWSAP       "AT+CWSAP=\"OneNET-Config\",\"\",11,0\r\n"
#define   CIPMUX      "AT+CIPMUX=1\r\n"
#define   CIPSERVER   "AT+CIPSERVER=1,80\r\n"



#define   MAX_SEND_BUF_LEN  1024

extern    EdpPacket* send_pkg;
extern    char send_buf[MAX_SEND_BUF_LEN];

extern		char DEVICEID[10];
extern		char APIKEY[30];
extern		char CWJAP[100];

extern		char * Send_HtmlData;
extern		char CIPSEND_CONNECT[50]; //AT+CIPSEND=%d,%d\r\n
extern		char CIPCLOSE[50];				//AT+CIPCLOSE=%d\r\n

extern		int connect_id;
extern		char http_parameter[100];
extern		char SSID[16];
extern		char PSW[16];

extern  void ESP8266_Init(void);
extern  void GetSendBuf(void);
extern  unsigned short int SendCmd(char* cmd, char* result, int timeOut);
extern  void ESP8266_DevLink(const char* devid, const char* auth_key, int timeOut);
extern   int ESP8266_CheckStatus(int timeOut);
extern  void ESP8266_SendDat(void);

/**
  * @brief  发送一条AT指令，并显示结果
**/
extern	unsigned short int SendCmd_ShowResult(char* cmd,char* result);
/**
  * @brief  ESP8266在STA模式下
						检查有没有连接请求，如果有，发送config页面
						---目前只做一路连接，没将多路连接做入数组
**/
extern	void ESP8266_Echo(void);

#endif


