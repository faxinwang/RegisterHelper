# -*- coding:utf-8 -*-
from selenium import webdriver
import time

browser = webdriver.Chrome()
try:
	browser.get('https://www.ctguqmx.com/account/login/')
	time.sleep(1)
	elem = browser.find_element_by_id('aw-login-user-name').send_keys('1094828998@qq.com')
	browser.find_element_by_id('aw-login-user-password').send_keys('MyPassword') 
	browser.find_element_by_id('login_submit').click()
	time.sleep(1)
	browser.get('https://www.ctguqmx.com/')
	time.sleep(1)
	browser.find_element_by_link_text('签到').click()
	time.sleep(1)
	browser.find_element_by_id('qd_button').click()
	time.sleep(1)
	print('res_successful')
	browser.close()
except Exception as e:
	print(e.messages)
	browser.close()
browser.quit()