# -*- coding:utf-8 -*-
import requests

login_page = "https://www.ctguqmx.com/account/ajax/login_process/"
login_data = {"user_name":"1094828998@qq.com","password":"******"}
regist_page1 = "https://www.ctguqmx.com/qiandao/"
regist_page2 = "http://172.25.5.133/index.php/Qiandao/doQd"
try:
    Se = requests.Session()
    resp = Se.post(login_page, login_data)
    resp = Se.get(regist_page1)
    resp = Se.get(regist_page2)
    if resp.text.count("签到成功") >= 1: print("res_success")    
except Exception as e:
    print(e)
