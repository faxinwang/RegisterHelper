# -*- coding:utf-8 -*-
from selenium import webdriver
import time

browser = webdriver.Chrome()
timeToWait = 25
try:
    browser.set_page_load_timeout(timeToWait)
    browser.get('http://acm.nyist.net/JudgeOnline/login.php')
    browser.find_element_by_id('userid').send_keys('wfx123')
    browser.find_element_by_id('password').send_keys('******')
    browser.find_element_by_name('btn_submit').click()
    browser.find_element_by_link_text('签到').click()
    print('res_successful')
	time.sleep(1)
    browser.close()
except Exception as e:
    print("page load timeout after waiting for", timeToWait ,"second")
    browser.close()
browser.quit()
