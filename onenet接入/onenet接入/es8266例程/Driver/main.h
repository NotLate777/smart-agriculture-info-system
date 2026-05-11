#ifndef   MAIN_H_H
#define   MAIN_H_H

#include "stm32f10x.h"
#include "stdio.h"
#include "stdlib.h"
#include "string.h"
#include "usart1.h"
#include "usart2.h"
#include "edpkit.h"
#include "esp8266.h"
#include "utils.h"
#include "hal_i2c.h"
#include "at24c02.h"

extern		float temp, humi;    //SHT20温湿度
extern		int16_t adxlData[3];	//三轴数据
extern		uint16_t lx;	//光照度
extern		float temp_ds18b20; //ds18b20温度
extern		int pwm_1_value,pwm_2_value;	//PWM值

#endif
