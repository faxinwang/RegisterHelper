# -*- coding:utf-8 -*-
from selenium import webdriver
import time

browser = webdriver.Chrome()
try:
	browser.get('http://acm.nyist.net/JudgeOnline/login.php')
	browser.find_element_by_id('userid').send_keys('wfx123')
	browser.find_element_by_id('password').send_keys('MyPassword') 
	browser.find_element_by_name('btn_submit').click()
	time.sleep(1)
	browser.find_element_by_link_text('签到').click()
	time.sleep(1)
	print('res_successful')
	browser.close()
except Exception as e:
	print(e.messages)
	browser.close()
browser.quit()