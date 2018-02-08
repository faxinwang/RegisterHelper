# 签到助手

## App简介

前两天在一本python书上看到了一个非常强大的python第三方库selenium, 之前一直想做一个自动签到的工具来着, 然后发现了这个python库之后, 瞬间感觉方便了好多, 不需要去往服务器post登录数据, 也不需要想方设法处理cookies和跳转重定向等麻烦问题. 只需要很少的几行代码就可以搞定了.

用了一段时间后，感觉每次自动签到的时候都会自动打开浏览器，跳转到登录页面，然后跳转到签到页面。网速慢的时候还会在页面上停留一段时间，感觉挺碍眼的，回来就用python的request库进行签到了。

## 使用方法

不过, 这些都不是重点, 重点是我用C#做的这个定时执行自动签到的工具, 通过xml配置文件, 可以非常方便的添加, 删除签到项和设置各项签到任务的执行时间.除了可以定时执行python脚本外, 还可以执行java程序和exe程序, 只需要在config.xml文件中做相应的配置, 并把相应的可执行文件放到与RegisterHelper.exe的同级目录Registers目录下就可以了.需要注意的是，该签到工具并不会在后台运行，所以点击窗口上的关闭按钮程序就退出了。如果要掩藏窗口，只需要点击最小化按钮，程序掩藏主窗体并显示一个系统托盘图标，双击托盘图标就可以打开主窗体了。

同样，可以右键生成一个快捷方式链接然后把链接复制到系统启动目录来实现开机自启动。
有了这个自动签到工具, 妈妈再也不用担心我认真学习忘记签到了.

## 配置文件使用方法

```xml
<?xml version="1.0" encoding="utf-8"?>
<RegisterList>
<!-- 可在RegisterList元素下添加任意多个签到项，每个签到项以item标签包
围，并在name标签内写入文件名，在time标签内写入需要执行签到的时间。 -->
  <item>
    <!-- 脚本文件名,程序自动根据后缀名判断如何运行该程序
    python使用python3解释器 -->
    <name>DoRegist_qmx.py</name>
    <!-- 设置签到时间 -->
    <time>9:30</time>
  </item>

  <item>
    <!-- 同一个脚本可以在不同的时间多次执行 -->
    <name>DoRegist_qmx.py</name>
    <time>15:30</time>
  </item>

  <item>
    <name>DoRegist_qmx.py</name>
    <time>19:30</time>
  </item>

  <item>
    <name>DoRegist_nyoj.py</name>
    <time>18:46</time>
  </item>

  <item>
    <!-- 在一天结束或者刚开始的时间，将所有签到项重置为尚未签到 -->
    <name>ResetAll</name>
    <time>00:05</time>
  </item>
</RegisterList>
```

## 截图

![主界面](https://github.com/faxinwang/RegisterHelper/raw/master/screenshot.png "主界面")

* `黄色`的表示签到未成功或未进行签到(比如程序开始运行的时候签到时间已经过了,ResetAll比较特殊, 会一直处于黄色状态)
* `绿色`的表示签到成功
* `红色`的表示签到时间还未到。