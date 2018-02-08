# -*- coding:utf-8 -*-
import requests

login_page = "http://59.69.128.203/JudgeOnline/dologin.php?url=http%3A%2F%2Facm.nyist.net%2FJudgeOnline%2Fproblemset.php"
login_data = {"userid":"wfx123","password":"******"}
regist_page1 = "http://59.69.128.203/JudgeOnline/profile.php?flag=1"
try:
    Se = requests.Session()
    Se.post(login_page, login_data)
    Se.get(regist_page1)
    resp = Se.get(regist_page1)
    resp.encoding = resp.apparent_encoding
    if resp.text.count("今天已经签过了") >= 1: print("res_success")    
except Exception as e:
    print(e)

