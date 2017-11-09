# -*- coding:utf-8 -*-
from selenium import webdriver
import time

browser = webdriver.Chrome()
timeToWait = 20
try:
    browser.set_page_load_timeout(timeToWait)
    browser.get('https://www.ctguqmx.com/account/login/')
    browser.find_element_by_id('aw-login-user-name').send_keys('1094828998@qq.com')
    browser.find_element_by_id('aw-login-user-password').send_keys('******') 
    browser.find_element_by_id('login_submit').click()
    browser.get('https://www.ctguqmx.com/')
    time.sleep(1);
    browser.find_element_by_link_text('签到').click()
    browser.find_element_by_id('qd_button').click()
    print('res_successful')
    time.sleep(1)
    browser.close()
except Exception as e:
    print("page load timeout after waiting for", timeToWait ,"second")
    browser.close()
browser.quit()